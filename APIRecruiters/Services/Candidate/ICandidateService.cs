namespace APIRecruiters.Services.Candidate
{
    using System.Collections.Generic;
    using APIRecruiters.ViewModels;

    public interface ICandidateService
    {
        public IEnumerable<ViewCandidate> AllCandidates();

        public ViewCandidate GetById(int Id);

        public bool Delete(int Id);

        public int Create(ViewCandidate candidate);

        public ViewCandidate Update(ViewCandidate candidate);
    }
}
