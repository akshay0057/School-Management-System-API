using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Menu
{
    public sealed class MenuTreeResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Route { get; set; }

        public string? Icon { get; set; }

        public List<MenuTreeResponse>
            Children
        { get; set; } = [];
    }
}
