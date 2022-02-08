namespace APIRecruiters
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using APIRecruiters.Data;
    using APIRecruiters.Infrastructure;
    using APIRecruiters.Services.Candidate;
    using APIRecruiters.Services.Job;
    using APIRecruiters.Services.Recruiter;
    using APIRecruiters.Services.Skill;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration{get;}

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<MyProjectDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ICandidateService, CandidateService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IRecruiterService, RecruiterService>();
            services.AddTransient<ISkillService, SkillService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Constants.HomePage);
                });
            });
        }
    }
}
