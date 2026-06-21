using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Exceptions;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.StudentModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ApiResponse<StudentResponse>> CreateAsync(AddStudentRequest request)
        {
            var admissionExist =await _studentRepository.ExistsAsync(x => x.AdmissionNo == request.AdmissionNo);

            if (admissionExist)
                throw new BadRequestException("Admission number already exists.");

            var rollExist = await _studentRepository.ExistsAsync(x => x.RollNo == request.RollNo);

            if (rollExist)
                throw new BadRequestException("Roll Number already exists.");

            var student = new StudentEntity
            {
                AdmissionNo = request.AdmissionNo,
                RollNo = request.RollNo,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                AdmissionDate = request.AdmissionDate,
                ClassId = request.ClassId,
                SectionId = request.SectionId,
                SessionId = request.SessionId,
                ParentName = request.ParentName,
                ParentMobile = request.ParentMobile,
                Address = request.Address
            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            var response = new StudentResponse
            {
                Id = student.Id,
                AdmissionNo = student.AdmissionNo,
                RollNo = student.RollNo,
                FullName = $"{student.FirstName} {student.LastName}"
            };

            return ApiResponse<StudentResponse>.SuccessResponse(
                    response,
                    "Student created successfully.");
        }

        public async Task<ApiResponse<StudentResponse?>> GetByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                throw new NotFoundException("Student not found.");

            var response = new StudentResponse
            {
                Id = student.Id,
                AdmissionNo = student.AdmissionNo,
                RollNo = student.RollNo,
                FullName = $"{student.FirstName} {student.LastName}",
                ClassName = student.Class?.ClassName ?? "",
                SectionName = student.Section?.SectionName ?? "",
                ParentName = student.ParentName ?? "",
                ParentMobile = student.ParentMobile ?? ""
            };

            return ApiResponse<StudentResponse?>.SuccessResponse(
                    response,
                    "Retrieved student successfully.");
        }

        public async Task<ApiResponse<PaginationResponse<StudentResponse>>>GetAllAsync(GetStudentListRequest request)
        {
            var query = _studentRepository.GetQueryable();

            #region Search

            if (!string.IsNullOrWhiteSpace(
                request.SearchText))
            {
                var search =
                    request.SearchText.Trim().ToLower();

                query = query.Where(x =>
                    x.FirstName.ToLower().Contains(search)
                    || (x.LastName != null &&
                        x.LastName.ToLower().Contains(search))
                    || x.AdmissionNo.ToLower().Contains(search)
                    || x.RollNo.ToLower().Contains(search)
                    || (x.ParentName != null &&
                        x.ParentName.ToLower().Contains(search))
                    || (x.ParentMobile != null &&
                        x.ParentMobile.Contains(search)));
            }

            #endregion

            #region Filters

            if (request.ClassId.HasValue)
            {
                query = query.Where(
                    x => x.ClassId ==
                    request.ClassId.Value);
            }

            if (request.SectionId.HasValue)
            {
                query = query.Where(
                    x => x.SectionId ==
                    request.SectionId.Value);
            }

            if (request.SessionId.HasValue)
            {
                query = query.Where(
                    x => x.SessionId ==
                    request.SessionId.Value);
            }

            #endregion

            var totalRecords =
                await query.CountAsync();

            var students = await query
                .OrderBy(x => x.FirstName)
                .Skip(
                    (request.PageNumber - 1)
                    * request.PageSize)
                .Take(request.PageSize)
                .Select(student =>
                    new StudentResponse
                    {
                        Id = student.Id,
                        AdmissionNo =
                            student.AdmissionNo,

                        RollNo =
                            student.RollNo,

                        FullName =
                            student.FirstName + " "
                            + student.LastName,

                        ClassName =
                            student.Class!.ClassName,

                        SectionName =
                            student.Section!.SectionName,

                        ParentName =
                            student.ParentName ?? "",

                        ParentMobile =
                            student.ParentMobile ?? ""
                    })
                .ToListAsync();

            return ApiResponse<
                PaginationResponse<StudentResponse>>
                .SuccessResponse(
                    new PaginationResponse<
                        StudentResponse>
                    {
                        TotalRecords =
                            totalRecords,

                        Data = students
                    },
                    "Retrieved students successfully.");
        }

        public async Task<ApiResponse<StudentResponse>> UpdateAsync(UpdateStudentRequest request)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);

            if (student == null)
                throw new NotFoundException("Student not found.");

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Gender = request.Gender;
            student.DateOfBirth = request.DateOfBirth;
            student.ClassId = request.ClassId;
            student.SectionId = request.SectionId;
            student.SessionId = request.SessionId;
            student.ParentName = request.ParentName;
            student.ParentMobile = request.ParentMobile;
            student.Address = request.Address;

            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();

            var response = new StudentResponse
            {
                Id = student.Id,
                AdmissionNo = student.AdmissionNo,
                RollNo = student.RollNo,
                FullName = $"{student.FirstName} {student.LastName}"
            };

            return ApiResponse<StudentResponse>.SuccessResponse(
                    response,
                    "Student updated successfully.");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                throw new NotFoundException("Student not found.");

            _studentRepository.Delete(student);
            await _studentRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(
                true,
                "Student deleted successfully.");
        }
    }
}
