namespace APIRecruiters.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using APIRecruiters.Services.Candidate;
    using APIRecruiters.ViewModels;

    [Route("[controller]")]
    [ApiController]
    public class CandidatesController : Controller
    {
        private readonly ICandidateService candidates;

        public CandidatesController(ICandidateService candidates)
        {
            this.candidates = candidates;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(this.candidates.AllCandidates());  
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var candidate = this.candidates.GetById(id);
            if (candidate == null)
            {
                return BadRequest("No Candidate with given Id in database!");
            }
            else
            {
                return Ok(candidate);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] ViewCandidate candidate)
        {
            try
            {
                var newId = this.candidates.Create(candidate);
                return Ok(this.candidates.GetById(newId));
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ViewCandidate candidate)
        {
            var currCandidate = this.candidates.GetById(id);
            if (currCandidate == null)
            {
                return BadRequest("No Candidate with the given Id in database!");
            }
            candidate.Id = id;
            try
            {
                var updatedCandidate = this.candidates.Update(candidate);
                return Ok(updatedCandidate);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = this.candidates.Delete(id);
            if (!result)
            {
                return BadRequest("No Candidate with the given Id in database!");
            }
            return Content("The Candidate deleted successfully!");
        }
    }
}
