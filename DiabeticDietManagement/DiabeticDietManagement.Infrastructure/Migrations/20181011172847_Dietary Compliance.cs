using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabeticDietManagement.Infrastructure.Migrations
{
    public partial class DietaryCompliance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietaryCompliances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    MealType = table.Column<int>(nullable: false),
                    WasComplied = table.Column<bool>(nullable: false),
                    EatenProducts = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryCompliances", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaryCompliances");
        }
    }
}
