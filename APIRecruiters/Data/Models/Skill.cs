namespace APIRecruiters.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Skill
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Candidate> Candidates { get; set; }
    }
}
