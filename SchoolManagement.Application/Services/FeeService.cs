using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.FeeCollection;
using SchoolManagement.Application.DTOs.FeeStructure;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.FeeModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Application.Services
{
    public class FeeService : IFeeService
    {
        private readonly IFeeStructureRepository _feeStructureRepository;
        private readonly IFeeCollectionRepository _feeCollectionRepository;
        private readonly IStudentRepository _studentRepository;

        public FeeService(
            IFeeStructureRepository feeStructureRepository,
            IFeeCollectionRepository feeCollectionRepository,
            IStudentRepository studentRepository)
        {
            _feeStructureRepository = feeStructureRepository;
            _feeCollectionRepository = feeCollectionRepository;
            _studentRepository = studentRepository;
        }

        public async Task CreateFeeStructureAsync(CreateFeeStructureRequest request)
        {
            var feeStructure =
                new FeeStructureEntity
                {
                    Id = Guid.NewGuid(),

                    ClassId = request.ClassId,

                    MonthlyFee = request.MonthlyFee,

                    EffectiveFrom = request.EffectiveFrom,

                    EffectiveTo = request.EffectiveTo,

                    CreatedAt = DateTime.UtcNow
                };

            await _feeStructureRepository
                .AddAsync(feeStructure);

            await _feeStructureRepository
                .SaveChangesAsync();
        }

        public async Task CollectFeeAsync(CreateFeeCollectionRequest request)
        {
            var alreadyCollected =
                await _feeCollectionRepository
                    .FeeAlreadyCollectedAsync(
                        request.StudentId,
                        request.Month,
                        request.Year);

            if (alreadyCollected)
            {
                throw new Exception(
                    "Fee already collected for this month.");
            }

            var feeCollection =
                new FeeCollectionEntity
                {
                    Id = Guid.NewGuid(),

                    StudentId = request.StudentId,

                    Month = request.Month,

                    Year = request.Year,

                    Amount = request.Amount,

                    PaymentDate = request.PaymentDate,

                    PaymentMode = request.PaymentMode,

                    ReceiptNo = GenerateReceiptNo(),

                    CreatedAt = DateTime.UtcNow
                };

            await _feeCollectionRepository
                .AddAsync(feeCollection);

            await _feeCollectionRepository
                .SaveChangesAsync();
        }

        public async Task<ApiResponse<StudentFeeHistoryResponse>> GetStudentFeeHistoryAsync(Guid studentId)
        {
            var student =
                await _studentRepository
                    .GetByIdAsync(studentId);

            if (student == null)
                throw new Exception("Student not found.");

            var fees =
                await _feeCollectionRepository
                    .GetByStudentIdAsync(studentId);

            var result = new StudentFeeHistoryResponse
            {
                StudentId = student.Id,

                StudentName =
                    $"{student.FirstName} {student.LastName}",

                FeeHistory = fees.Select(x =>
                    new StudentFeeHistoryItem
                    {
                        Month = x.Month,

                        Year = x.Year,

                        Amount = x.Amount,

                        PaymentDate = x.PaymentDate,

                        ReceiptNo = x.ReceiptNo
                    }).ToList()
            };

            return ApiResponse<StudentFeeHistoryResponse>.SuccessResponse(result);
        }

        public async Task<ApiResponse<FeeDueResponse>> GetStudentDueAsync(Guid studentId)
        {
            var student =
                await _studentRepository
                    .GetByIdAsync(studentId);

            if (student == null)
                throw new Exception("Student not found.");

            var feeStructure =
                await _feeStructureRepository
                    .GetByClassIdAsync(student.ClassId);

            if (feeStructure == null)
                throw new Exception(
                    "Fee structure not found.");

            var fees =
                await _feeCollectionRepository
                    .GetByStudentIdAsync(studentId);

            var currentMonth =
                DateTime.UtcNow.Month;

            var currentYear =
                DateTime.UtcNow.Year;

            var paidMonths =
                fees.Where(x => x.Year == currentYear)
                    .Select(x => x.Month)
                    .Distinct()
                    .Count();

            var pendingMonths =
                currentMonth - paidMonths;

            if (pendingMonths < 0)
                pendingMonths = 0;

            var totalDue =
                pendingMonths *
                feeStructure.MonthlyFee;

            var result = new FeeDueResponse
            {
                StudentId = student.Id,

                StudentName = $"{student.FirstName} {student.LastName}",

                MonthlyFee = feeStructure.MonthlyFee,

                PendingMonths = pendingMonths,

                TotalDue = totalDue
            };

            return ApiResponse<FeeDueResponse>.SuccessResponse(result);
        }

        #region Private Methods
        private string GenerateReceiptNo()
        {
            return $"RCPT-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
        #endregion
    }
}
