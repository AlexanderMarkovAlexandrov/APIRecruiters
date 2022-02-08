namespace APIRecruiters.Data.Models
{
    public class Interview
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
