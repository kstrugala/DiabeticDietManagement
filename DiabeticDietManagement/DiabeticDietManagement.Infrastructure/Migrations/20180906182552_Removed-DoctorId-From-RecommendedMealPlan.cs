using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabeticDietManagement.Infrastructure.Migrations
{
    public partial class RemovedDoctorIdFromRecommendedMealPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "RecommendedMealPlans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "RecommendedMealPlans",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
