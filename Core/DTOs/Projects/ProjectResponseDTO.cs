using Core.Entities;

namespace Core.DTOs.Projects
{

    public class ProjectResponseDTO
    {
        public Guid ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string TechStack { get; set; } = string.Empty;

        public string GitHubUrl { get; set; } = string.Empty;

        public string LiveUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }


        public static ProjectResponseDTO ToResponse(Project project)
        {
            return new ProjectResponseDTO
            {
                ID = project.ID,
                Title = project.Title,
                Description = project.Description,
                TechStack = project.TechStack,
                GitHubUrl = project.GitHubUrl,
                LiveUrl = project.LiveUrl,
                CreatedAt = project.CreatedAt
            };
        }

        public static void ValidateProject(string title, string? gitHubUrl, string? liveUrl)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required");

            if (title.Length > 100)
                throw new ArgumentException("Title too long");

            ValidateUrl(gitHubUrl, "GitHub URL");
            ValidateUrl(liveUrl, "Live URL");
        }

        public static void ValidateUrl(string? url, string fieldName)
        {
            if (!string.IsNullOrWhiteSpace(url) && !Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new ArgumentException($"Invalid {fieldName}");
        }

    }

}
