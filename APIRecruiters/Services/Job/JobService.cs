namespace APIRecruiters.Services.Job
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APIRecruiters.Data;
    using APIRecruiters.Data.Models;
    using APIRecruiters.ViewModels;

    public class JobService : IJobService
    {
        private readonly MyProjectDbContext data;

        public JobService(MyProjectDbContext data)
        {
            this.data = data;
        }

        public int Create(ViewJob job)
        {
            if (job.Skills.Count() == 0)
            {
                throw new ArgumentException("The Skills can't be null!");
            }

            var skills = this.data.Skills.ToList().Where(s => job.Skills.Any(t => t.Name == s.Name)).ToList();
            var newSkills = new List<Skill>();
            if (skills.Count > 0)
            {
                newSkills = job.Skills.Where(s => skills.All(t => t.Name != s.Name))
                                                      .Select(s => new Skill { Name = s.Name }).ToList();
            }
            else
            {
                newSkills = job.Skills.Select(s => new Skill { Name = s.Name }).ToList();
            }
             
            skills.AddRange(newSkills);

            var candidates = this.data.Candidates
                                   .Include(c => c.Skills)
                                   .Where(c => c.Recruiter.InterviewSlots < 5)
                                   .ToList();

            var selectedCandidates = candidates.Where(c => c.Skills.Intersect(skills).Count() > 0)
                                                .OrderByDescending(c => c.Skills.Intersect(skills).Count())
                                                .ToArray(); 
            var recruiters = this.data.Recruiters.ToList().Where(r => selectedCandidates.Any(c => c.RecruiterId == r.Id)).ToHashSet();

            var newJob = new Job
            {
                Title = job.Title,
                Description = job.Description,
                Salary = job.Salary,
                Skills = skills
            };
            this.data.Jobs.Add(newJob);
            this.data.SaveChanges();

            var interviews = new List<Interview>();
            foreach (var candidate in selectedCandidates)
            {
                var recruiter = recruiters.First(r => r.Id == candidate.RecruiterId);
                if (recruiter.InterviewSlots < 5)
                {
                    interviews.Add(new Interview { CandidateId = candidate.Id, JobId = newJob.Id }); 
                    recruiter.InterviewSlots += 1;
                }                   
            }
            this.data.Interviews.AddRange(interviews);
            this.data.SaveChanges();

            return newJob.Id;
        }

        public bool Delete(int id)
        {
            var job = this.data.Jobs.Where(j => j.Id == id).FirstOrDefault();
            if (job == null)
            {
                return false;
            }

            var interviews = this.data.Interviews.Where(i => i.JobId == job.Id);

            foreach (var interview in interviews)
            {
                var recruter = this.data.Recruiters.First(r => r.Candidates.Any(c => c.Id == interview.CandidateId));
                recruter.InterviewSlots -= 1;
            }
            this.data.Interviews.RemoveRange(interviews);
            this.data.Jobs.Remove(job);
            this.data.SaveChanges();
            return true;
        }

        public IEnumerable<ViewJob> AllJobsBySkill(string skill)
        {
            return this.data.Jobs.Include(s => s.Skills)
                                .Where(j => j.Skills.Any(s => s.Name == skill))
                                .Select(j => CreateViewJob(j))
                                .ToArray();
        }
        public ViewJob JobById(int id)
        {
            return this.data.Jobs.Where(j => j.Id == id)
                                .Select(j => CreateViewJob(j))
                                .FirstOrDefault();
        }

        public static ViewJob CreateViewJob(Job job)
        {
            return new ViewJob
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Salary = job.Salary,
                Skills = job.Skills.Select(s => new ViewSkill { Name = s.Name }).ToArray()
            };
        }
    }
}
