using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabeticDietManagement.Infrastructure.Migrations
{
    public partial class MealPlanJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedMealPlanId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "RecommendedMealPlan",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedMealPlan",
                table: "Patients");

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedMealPlanId",
                table: "Patients",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
