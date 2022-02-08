namespace APIRecruiters.Services.Recruiter
{
    using System.Collections.Generic;
    using APIRecruiters.ViewModels;

    public interface IRecruiterService
    {
        public IEnumerable<ViewRecruiter> AllRecruiters(int level);
    }
}
