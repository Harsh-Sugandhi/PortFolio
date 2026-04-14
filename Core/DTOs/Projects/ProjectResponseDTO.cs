using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Projects
{

    public class ProjectResponseDTO
    {
        public Guid? ID { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? TechStack { get; set; }

        public string? GitHubUrl { get; set; }

        public string? LiveUrl { get; set; }

        public DateTime? CreatedAt { get; set; }
    }

}
