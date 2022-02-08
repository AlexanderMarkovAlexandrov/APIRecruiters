namespace APIRecruiters.Services.Job
{
    using System.Collections.Generic;
    using APIRecruiters.ViewModels;

    public interface IJobService
    {
        public IEnumerable<ViewJob> AllJobsBySkill(string skill);
        public int Create(ViewJob job);
        public bool Delete(int id);
        public ViewJob JobById(int id);
    }
}
