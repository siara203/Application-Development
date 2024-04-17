using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TrainingFPTCo.DataDBContext
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Column("Name", TypeName = "Varchar(100)"), Required]
        public required string NameCategory { get; set; }

        [Column("Description", TypeName = "Varchar(200)"), AllowNull]
        public string? Description { get; set; }

        [Column("PosterImage", TypeName = "Varchar(100)"), Required]
        public required string PosterImage { get; set; }

        [Column("ParentId", TypeName = "Integer"), Required]
        public int  ParentId { get; set; }

        [Column("Status", TypeName = "Varchar(20)"), Required]
        public required string Status { get; set; }
        [AllowNull]
        public DateTime? CreateAt { get; set; }

        [AllowNull]
        public DateTime? UpdateAt { get; set; }

        [AllowNull]
        public DateTime? DeleteAt { get; set; }

    }
}
