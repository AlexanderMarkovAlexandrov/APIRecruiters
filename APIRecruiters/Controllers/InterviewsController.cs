namespace APIRecruiters.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using APIRecruiters.Data;

    [Route("[controller]")]
    [ApiController]
    public class InterviewsController : Controller
    {
        private readonly MyProjectDbContext data;

        // Here I use the other way to write code without service, I work directly with the database in the controller.
        public InterviewsController(MyProjectDbContext data)
        {
            this.data = data;
        }

        public ActionResult Get()
        {
            return Ok(this.data.Interviews);
        }
    }
}
