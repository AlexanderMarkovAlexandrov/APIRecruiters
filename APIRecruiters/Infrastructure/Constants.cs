namespace APIRecruiters.Infrastructure
{
    public class Constants
    {
        public const string HomePage = "Welcome to my API project! Project Name: APIRecruiters." +
            "\r\n  - Endpoints: "+
            "\r\n	GET:http://localhost:8080/candidates/" +
            "\r\n	POST:http://localhost:8080/candidates/" +
            "\r\n	GET:http://localhost:8080/candidates/{id}"+
            "\r\n	PUT: http://localhost:8080/candidates/{id}"+
            "\r\n	DELETE:http://localhost:8080/candidates/{id}" +
            "\r\n" +
            "\r\n	GET:http://localhost:8080/skills/{id}" +
            "\r\n	GET:http://localhost:8080/skills/active" +
            "\r\n" +
            "\r\n	GET:http://localhost:8080/recruiter/{id}" +
            "\r\n	GET:http://localhost:8080/recruiters?level={level}" +
            "\r\n" +
            "\r\n	POST:http://localhost:8080/jobs/" +
            "\r\n	GET:http://localhost:8080/jobs?skill={skillName}" +
            "\r\n	DELETE:http://localhost:8080/jobs/{id}" +
            "\r\n" +
            "\r\n	GET:http://localhost:8080/interviews";
    }
}
