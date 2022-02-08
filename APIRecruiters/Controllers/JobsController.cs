namespace APIRecruiters.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using APIRecruiters.Services.Job;
    using APIRecruiters.ViewModels;

    [Route("[controller]")]
    [ApiController]
    public class JobsController : Controller
    {
        private readonly IJobService jobs;

        public JobsController(IJobService jobs)
        {
            this.jobs = jobs;
        }

        [HttpGet]
        public ActionResult Get([FromQuery] string skill)
        {
            return Ok(this.jobs.AllJobsBySkill(skill));
        }

        [HttpPost]
        public ActionResult Post([FromBody] ViewJob job)
        {
            try
            {
                var newId = this.jobs.Create(job);
                return Ok(this.jobs.JobById(newId));
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = this.jobs.Delete(id);
            if (!result)
            {
                return BadRequest("No Job with the given Id in database!");
            }
            return Content("The Job deleted successfully!");
        }
    }
}
