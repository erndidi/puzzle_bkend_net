using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Word_Puzzle.Model
{
    public class Definition
    {
        [Key]
        public Guid ID { get; set; }

        public Word Word { get; set; }

        [Column("Def", TypeName = "nvarchar")]
        [MaxLength(1000)]
        [Required]

        public string Def { get; set; }
    }
}
   