using SchoolManagement.Application.DTOs.StaffAttendence;
using SchoolManagement.Application.DTOs.StudentAttendence;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.AttendanceModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IStudentAttendanceRepository _studentAttendanceRepository;
        private readonly IStaffAttendanceRepository _staffAttendanceRepository;

        public AttendanceService(IStudentAttendanceRepository studentAttendanceRepository, IStaffAttendanceRepository staffAttendanceRepository)
        {
            _studentAttendanceRepository = studentAttendanceRepository;
            _staffAttendanceRepository = staffAttendanceRepository;
        }

        public async Task CreateStudentAttendanceAsync(CreateStudentAttendanceRequest request)
        {
            var exists =
                await _studentAttendanceRepository
                    .ExistsAttendanceAsync(
                        request.StudentId,
                        request.AttendanceDate);

            if (exists)
                throw new Exception(
                    "Attendance already marked.");

            var attendance =
                new StudentAttendanceEntity
                {
                    Id = Guid.NewGuid(),

                    StudentId = request.StudentId,

                    AttendanceDate = request.AttendanceDate,

                    Status = request.Status,

                    CreatedAt = DateTime.UtcNow
                };

            await _studentAttendanceRepository
                .AddAsync(attendance);

            await _studentAttendanceRepository
                .SaveChangesAsync();
        }

        public async Task CreateBulkStudentAttendanceAsync(BulkStudentAttendanceRequest request)
        {
            var studentIds =
                request.Students
                    .Select(x => x.StudentId)
                    .ToList();

            var existingStudents =
                await _studentAttendanceRepository
                    .GetAlreadyMarkedStudentIdsAsync(
                        studentIds,
                        request.AttendanceDate.Date);

            foreach (var student in request.Students)
            {
                if (existingStudents.Contains(student.StudentId))
                    continue;

                await _studentAttendanceRepository.AddAsync(
                    new StudentAttendanceEntity
                    {
                        Id = Guid.NewGuid(),

                        StudentId = student.StudentId,

                        AttendanceDate = request.AttendanceDate.Date,

                        Status = student.Status,

                        CreatedAt = DateTime.UtcNow,

                        IsActive = true
                    });
            }

            await _studentAttendanceRepository.SaveChangesAsync();
        }

        public async Task CreateStaffAttendanceAsync(CreateStaffAttendanceRequest request)
        {
            var exists =
                await _staffAttendanceRepository
                    .ExistsAttendanceAsync(
                        request.StaffId,
                        request.AttendanceDate);

            if (exists)
                throw new Exception(
                    "Attendance already marked.");

            var attendance =
                new StaffAttendanceEntity
                {
                    Id = Guid.NewGuid(),

                    StaffId = request.StaffId,

                    AttendanceDate = request.AttendanceDate,

                    Status = request.Status,

                    CreatedAt = DateTime.UtcNow
                };

            await _staffAttendanceRepository
                .AddAsync(attendance);

            await _staffAttendanceRepository
                .SaveChangesAsync();
        }
    }
}
