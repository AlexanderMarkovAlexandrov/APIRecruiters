namespace APIRecruiters.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using APIRecruiters.Data.Models;

    public class ViewJob
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Title { get; init; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Salary { get; set; }

        public IEnumerable<ViewSkill> Skills { get; set; }
    }
}
