namespace APIRecruiters.Data
{
    using Microsoft.EntityFrameworkCore;
    using APIRecruiters.Data.Models;

    public class MyProjectDbContext : DbContext
    {
        public MyProjectDbContext(DbContextOptions<MyProjectDbContext> options)
            : base(options)
        { }

        public DbSet<Job> Jobs { get; init; }
        public DbSet<Skill> Skills { get; init; }
        public DbSet<Candidate> Candidates { get; init; }
        public DbSet<Interview> Interviews { get; init; }
        public DbSet<Recruiter> Recruiters { get; init; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Interview>()
                .HasKey(c => new { c.CandidateId, c.JobId });

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Recruiter)
                .WithMany(c => c.Candidates)
                .HasForeignKey(c => c.RecruiterId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
