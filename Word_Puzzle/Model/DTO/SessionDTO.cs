using System.ComponentModel.DataAnnotations;

namespace Puzzle_API.Model.DTO
{
    public class SessionDTO
    {
        [Required]
        public string? SessionId { get; set; }
        [Required]
        public string? UserName { get; set; }
    }
}
