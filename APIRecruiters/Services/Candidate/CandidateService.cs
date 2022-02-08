namespace APIRecruiters.Services.Candidate
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APIRecruiters.Data;
    using APIRecruiters.Data.Models;
    using APIRecruiters.ViewModels;

    public class CandidateService : ICandidateService
    {
        private readonly MyProjectDbContext data;
        public CandidateService(MyProjectDbContext data)
        {
            this.data = data;
        }
        public IEnumerable<ViewCandidate> AllCandidates()
        {
            return this.data.Candidates
                .Include(r => r.Recruiter)
                .Include(s => s.Skills)
                .Select(c=> CreateViewCandidate(c));
        }

        public int Create(ViewCandidate candidate)
        {
            if (candidate.Recruiter == null)
            {
                throw new ArgumentException("The Recruiter can't be null!");
            }
            if (candidate.Skills.Count() == 0)
            {
                throw new ArgumentException("The Skills can't be null!");
            }
            if (this.data.Candidates.Any(c => c.Email == candidate.Email))
            {
                throw new ArgumentException("The Candidate with this email already exist!");
            }

            var skills = this.data.Skills.ToList().Where(s => candidate.Skills.Any(t => t.Name == s.Name)).ToList();
            var newSkills = new List<Skill>();
            if (skills.Count >0)
            {
                 newSkills = candidate.Skills.Where(s => skills.All(t => t.Name != s.Name))
                                     .Select(s => new Skill { Name = s.Name }).ToList();
            }
            else
            {
                newSkills = candidate.Skills.Select(s => new Skill { Name = s.Name }).ToList();
            }

            skills.AddRange(newSkills);

            var recruiter = this.data.Recruiters.Where(r => r.Email == candidate.Recruiter.Email).FirstOrDefault();

            if (recruiter == null)
            {
                recruiter = new Recruiter
                {
                    LastName = candidate.Recruiter.LastName,
                    Email = candidate.Recruiter.Email,
                    Country = candidate.Recruiter.Country,
                    Level = 1,
                };
                this.data.Recruiters.Add(recruiter);
                this.data.SaveChanges();
            }
            else
            {
                recruiter.Level += 1;
            }
            
            var newCandidate = new Candidate 
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                BirthDate = candidate.BirthDate,
                Bio = candidate.Bio,
                RecruiterId = recruiter.Id,
                Recruiter = recruiter,
                Skills = skills
            };
            this.data.Candidates.Add(newCandidate);
            this.data.SaveChanges();

            return newCandidate.Id;
        }

        public bool Delete(int Id)
        {
            var candidate = this.data.Candidates.FirstOrDefault(c => c.Id == Id);
            if (candidate == null)
            {
                return false;
            }
            else
            {
                var interviews = this.data.Interviews.Where(i => i.CandidateId == candidate.Id);
                var recruiter = this.data.Recruiters.Where(r => r.Id == candidate.RecruiterId).FirstOrDefault();
                recruiter.Level -= 1;
                this.data.Interviews.RemoveRange(interviews);
                this.data.Candidates.Remove(candidate);
                this.data.SaveChanges();
                return true;
            }
        }

        public ViewCandidate GetById(int Id)
        {
            var candidate = this.data.Candidates
                .Where(c => c.Id == Id)
                .Include(o => o.Recruiter)
                .Include(s => s.Skills)
                .Select(c => CreateViewCandidate(c) )
                .FirstOrDefault();
              
            return candidate;
        }

        public ViewCandidate Update(ViewCandidate candidate)
        {
            var currCandidate = this.data.Candidates.FirstOrDefault(c => c.Id == candidate.Id);
            var skills = this.data.Skills.ToList().Where(s => candidate.Skills.Any(t => t.Name == s.Name)).ToList();
            var newSkills = new List<Skill>();
            if (skills.Count > 0)
            {
                newSkills = candidate.Skills.Where(s => skills.All(t => t.Name != s.Name))
                                    .Select(s => new Skill { Name = s.Name }).ToList();
            }
            else
            {
                newSkills = candidate.Skills.Select(s => new Skill { Name = s.Name }).ToList();
            }

            skills.AddRange(newSkills);

            var recruiter = this.data.Recruiters.Where(r => r.Email == candidate.Recruiter.Email).FirstOrDefault();

            if (recruiter == null)
            {
                recruiter = new Recruiter
                {
                    LastName = candidate.Recruiter.LastName,
                    Email = candidate.Recruiter.Email,
                    Country = candidate.Recruiter.Country,
                };
                this.data.Recruiters.Add(recruiter);
                this.data.SaveChanges();
            }
            if (currCandidate.RecruiterId != recruiter.Id)
            {
                currCandidate.Recruiter.Level -= 1;
                recruiter.Level += 1;
                this.data.SaveChanges();
            }
            currCandidate.LastName = candidate.LastName;
            currCandidate.Email = candidate.Email;
            currCandidate.Bio = candidate.Bio;
            currCandidate.RecruiterId = recruiter.Id;
            currCandidate.Recruiter = recruiter;
            currCandidate.Skills = skills;
            this.data.SaveChanges();
            return this.GetById(candidate.Id);
        }

        public static ViewCandidate CreateViewCandidate(Candidate c)
        {
            return new ViewCandidate
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Bio = c.Bio,
                BirthDate = c.BirthDate,
                Skills = c.Skills.Select(c => new ViewSkill { Name = c.Name }).ToArray(),
                Recruiter = new ViewRecruiter
                {
                    LastName = c.Recruiter.LastName,
                    Email = c.Recruiter.Email,
                    Country = c.Recruiter.Country
                }
            };
        }
    }
}
