namespace APIRecruiters.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ViewRecruiter
    {
        public string LastName { get; init; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
    }
}
