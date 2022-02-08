namespace APIRecruiters.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Candidate
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; init; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string BirthDate { get; init; }

        [Required]
        public string Bio { get; set; }

        public int RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }

        public virtual IEnumerable<Skill> Skills { get; set; }

        public IEnumerable<Interview> Interviews { get; set; }
    }
}
