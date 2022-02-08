namespace APIRecruiters.Services.Skill
{
    using System.Collections.Generic;
    using APIRecruiters.Data.Models;
    public interface ISkillService
    {
        public Skill GetById(int id);
        public IEnumerable<Skill> AllActiveSkills();
    }
}
