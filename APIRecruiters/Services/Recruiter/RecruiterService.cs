namespace APIRecruiters.Services.Recruiter
{
    using System.Collections.Generic;
    using System.Linq;
    using APIRecruiters.Data;
    using APIRecruiters.ViewModels;

    public class RecruiterService : IRecruiterService
    {
        private readonly MyProjectDbContext data;

        public RecruiterService(MyProjectDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<ViewRecruiter> AllRecruiters(int level)
        {

            return this.data.Recruiters.Where(r => level < 0 ? r.Level > 0 : r.Level == level)
                                        .Select(r => new ViewRecruiter 
                                        {
                                            LastName = r.LastName,
                                            Email = r.Email,
                                            Country = r.Country
                                        }).ToArray();
        }
    }
}
