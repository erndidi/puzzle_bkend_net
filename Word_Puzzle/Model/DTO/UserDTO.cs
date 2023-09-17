using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Puzzle_API.Model.DTO
{
    public class PlayerDTO
    {
        public Guid? Id { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? Lastname { get; set; }
        [Required]
        public string? Password { get; set; }


        public string? GoogleId { get; set; }

        public string? FacebookId { get; set; }

        public string? SessionId { get; set; }   
    
        public string? UserName { get; set; }

        public string? UsedWordId { get; set; } 
                
        public int Score { get; set; }

        public bool UserFound { get; set; }




    }
}
