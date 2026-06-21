using SchoolManagement.Application.Common.Models;

namespace SchoolManagement.Application.DTOs.Student
{
    public class GetStudentListRequest : PaginationRequest
    {
        public Guid? ClassId { get; set; }

        public Guid? SectionId { get; set; }

        public Guid? SessionId { get; set; }
    }
}
