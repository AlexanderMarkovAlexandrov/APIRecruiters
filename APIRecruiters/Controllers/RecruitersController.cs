namespace APIRecruiters.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using APIRecruiters.Services.Recruiter;

    [Route("[controller]")]
    [ApiController]
    public class RecruitersController : Controller
    {
        private readonly IRecruiterService recruiter;

        public RecruitersController(IRecruiterService recruiter)
        {
            this.recruiter = recruiter;
        }

        [HttpGet]
        public ActionResult Get([FromQuery] string level)
        {
            bool hasLevel = int.TryParse(level, out int input);
            if (input < 0)
            {
                return BadRequest("Invalid level!");
            }
            input = hasLevel ? input : -1;
            try
            {
                var recruiters = this.recruiter.AllRecruiters(input);
                return Ok(recruiters);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
