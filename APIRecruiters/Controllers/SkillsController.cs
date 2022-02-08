namespace APIRecruiters.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using APIRecruiters.Services.Skill;

    
    [ApiController]
    public class SkillsController : Controller
    {
        private readonly ISkillService skills;

        public SkillsController(ISkillService skills)
        {
            this.skills = skills;
        }

        [Route("[controller]/{Id}")]
        [HttpGet]
        public ActionResult Get(int id) 
        {
            var skill = this.skills.GetById(id);
            if (skill == null)
            {
                return BadRequest("No Skill with given Id in database!");
            }
            return Ok(skill);
        }

        [Route("[controller]/[action]")] 
        [HttpGet]
        public ActionResult Active()
        {
            try
            {
                var skills = this.skills.AllActiveSkills();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }  
        }
    }
}
