using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Exceptions;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Staff;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.StaffModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<ApiResponse<StaffResponse>>AddStaffAsync(AddStaffRequest request)
        {
            var employeeExists =
                await _staffRepository
                .GetByEmployeeCodeAsync(
                    request.EmployeeCode);

            if (employeeExists != null)
                throw new BadRequestException(
                    "Employee code already exists.");

            var emailExists =
                await _staffRepository
                .GetByEmailAsync(
                    request.Email);

            if (emailExists != null)
                throw new BadRequestException(
                    "Email already exists.");

            var staff = new StaffEntity
            {
                EmployeeCode = request.EmployeeCode,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Mobile = request.Mobile,
                Email = request.Email,
                Designation = request.Designation,
                JoiningDate = request.JoiningDate,
                BasicSalary = request.BasicSalary
            };

            await _staffRepository.AddAsync(staff);

            await _staffRepository.SaveChangesAsync();

            return ApiResponse<StaffResponse>
                .SuccessResponse(
                    MapToResponse(staff),
                    "Staff added successfully.");
        }

        public async Task<ApiResponse<StaffResponse>>UpdateStaffAsync(UpdateStaffRequest request)
        {
            var staff =
                await _staffRepository
                .GetByIdAsync(request.Id);

            if (staff == null)
                throw new NotFoundException(
                    "Staff not found.");

            staff.FirstName = request.FirstName;
            staff.LastName = request.LastName;
            staff.Gender = request.Gender;
            staff.Mobile = request.Mobile;
            staff.Email = request.Email;
            staff.Designation = request.Designation;
            staff.JoiningDate = request.JoiningDate;
            staff.BasicSalary = request.BasicSalary;

            _staffRepository.Update(staff);

            await _staffRepository.SaveChangesAsync();

            return ApiResponse<StaffResponse>
                .SuccessResponse(
                    MapToResponse(staff),
                    "Staff updated successfully.");
        }

        public async Task<ApiResponse<StaffResponse>>GetByIdAsync(Guid id)
        {
            var staff =
                await _staffRepository.GetByIdAsync(id);

            if (staff == null)
                throw new NotFoundException(
                    "Staff not found.");

            return ApiResponse<StaffResponse>
                .SuccessResponse(
                    MapToResponse(staff));
        }

        public async Task<ApiResponse<bool>>DeleteAsync(Guid id)
        {
            var staff =
                await _staffRepository.GetByIdAsync(id);

            if (staff == null)
                throw new NotFoundException(
                    "Staff not found.");

            _staffRepository.Delete(staff);

            await _staffRepository.SaveChangesAsync();

            return ApiResponse<bool>
                .SuccessResponse(
                    true,
                    "Staff deleted successfully.");
        }

        public async Task<ApiResponse<PaginationResponse<StaffResponse>>>GetListAsync(GetStaffListRequest request)
        {
            var query = _staffRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(
                request.SearchText))
            {
                var search =
                    request.SearchText.Trim();

                query = query.Where(x =>
                    x.FirstName.Contains(search) ||
                    x.LastName!.Contains(search) ||
                    x.EmployeeCode.Contains(search) ||
                    x.Mobile.Contains(search));
            }

            var totalRecords =
                await query.CountAsync();

            var data = await query
                .OrderBy(x => x.FirstName)
                .Skip((request.PageNumber - 1)
                    * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new StaffResponse
                {
                    Id = x.Id,
                    EmployeeCode = x.EmployeeCode,
                    FullName =
                        x.FirstName + " " + x.LastName,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    Designation = x.Designation,
                    JoiningDate = x.JoiningDate,
                    BasicSalary = x.BasicSalary
                })
                .ToListAsync();

            return ApiResponse<
                PaginationResponse<StaffResponse>>
                .SuccessResponse(
                    new PaginationResponse<
                        StaffResponse>
                    {
                        TotalRecords = totalRecords,
                        Data = data
                    });
        }

        #region Private Methods
        private static StaffResponse MapToResponse(StaffEntity staff)
        {
            return new StaffResponse
            {
                Id = staff.Id,
                EmployeeCode = staff.EmployeeCode,
                FullName =
                    $"{staff.FirstName} {staff.LastName}",
                Mobile = staff.Mobile,
                Email = staff.Email,
                Designation = staff.Designation,
                JoiningDate = staff.JoiningDate,
                BasicSalary = staff.BasicSalary
            };
        }
        #endregion
    }
}
