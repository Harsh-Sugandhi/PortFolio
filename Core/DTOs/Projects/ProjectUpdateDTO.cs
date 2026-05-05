using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Projects
{
    public class ProjectUpdateDTO
    {

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(200)]
        public string? TechStack { get; set; }

        [Url]
        [MaxLength(200)]
        public string? GitHubUrl { get; set; }

        [Url]
        [MaxLength(200)]
        public string? LiveUrl { get; set; }

    }

}
