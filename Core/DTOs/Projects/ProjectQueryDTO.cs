using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Projects
{

    public class ProjectQueryDTO
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
    }

}
