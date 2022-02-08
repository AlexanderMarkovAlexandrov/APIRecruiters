namespace APIRecruiters.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Job
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Title { get; init; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Salary { get; set; }

        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Interview> Interviews { get; set; }
    }
}
