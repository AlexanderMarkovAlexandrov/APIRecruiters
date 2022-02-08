namespace APIRecruiters.Services.Skill
{
    using System.Collections.Generic;
    using System.Linq;
    using APIRecruiters.Data;
    using APIRecruiters.Data.Models;

    public class SkillService : ISkillService
    {
        private readonly MyProjectDbContext data;

        public SkillService(MyProjectDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<Skill> AllActiveSkills()
        {
            return this.data.Skills.Where(s => s.Candidates.Count() > 0).ToArray();
        }

        public Skill GetById(int id)
        {
            return this.data.Skills.Where(s => s.Id == id).FirstOrDefault();
        }
    }
}
