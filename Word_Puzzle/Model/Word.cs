using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Word_Puzzle.Model
{
    [Table("Word")]
    public class Word
    {
        [Key]
        public Guid ID { get; set; }

        [Column("Name", TypeName = "nvarchar")]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }
        List<Definition> Definitions { get; set; } = new List<Definition>();
    }
}
