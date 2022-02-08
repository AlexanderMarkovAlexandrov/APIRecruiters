namespace APIRecruiters.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ViewCandidate
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string BirthDate { get; set; }

        [Required]
        public string Bio { get; set; }

        public ViewRecruiter Recruiter { get; set; }

        public IEnumerable<ViewSkill> Skills { get; set; }
    }
}
