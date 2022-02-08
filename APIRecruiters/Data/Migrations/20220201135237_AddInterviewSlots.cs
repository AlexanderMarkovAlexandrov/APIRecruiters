using Microsoft.EntityFrameworkCore.Migrations;

namespace APIRecruiters.Migrations
{
    public partial class AddInterviewSlots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterviewSlots",
                table: "Recruiters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterviewSlots",
                table: "Recruiters");
        }
    }
}
