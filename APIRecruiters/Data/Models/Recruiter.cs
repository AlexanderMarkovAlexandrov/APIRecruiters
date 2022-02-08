namespace APIRecruiters.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recruiter
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; init; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        public int Level { get; set; }

        public int InterviewSlots { get; set; }
        public IEnumerable<Candidate> Candidates { get; set; }
    }
}
