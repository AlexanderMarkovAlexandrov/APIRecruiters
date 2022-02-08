namespace APIRecruiters.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ViewSkill
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; init; }
    }
}
