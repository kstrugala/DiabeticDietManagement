using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabeticDietManagement.Infrastructure.Migrations
{
    public partial class RemoveRecommendedMealPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyMealPlan");

            migrationBuilder.DropTable(
                name: "Portion");

            migrationBuilder.DropTable(
                name: "RecommendedMealPlans");

            migrationBuilder.DropTable(
                name: "Meal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedMealPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedMealPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MealId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portion_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyMealPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BreakfastId = table.Column<Guid>(nullable: true),
                    Day = table.Column<long>(nullable: false),
                    DinnerId = table.Column<Guid>(nullable: true),
                    LunchId = table.Column<Guid>(nullable: true),
                    RecommendedMealPlanId = table.Column<Guid>(nullable: true),
                    SnapId = table.Column<Guid>(nullable: true),
                    SupperId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMealPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMealPlan_Meal_BreakfastId",
                        column: x => x.BreakfastId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyMealPlan_Meal_DinnerId",
                        column: x => x.DinnerId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyMealPlan_Meal_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyMealPlan_RecommendedMealPlans_RecommendedMealPlanId",
                        column: x => x.RecommendedMealPlanId,
                        principalTable: "RecommendedMealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyMealPlan_Meal_SnapId",
                        column: x => x.SnapId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyMealPlan_Meal_SupperId",
                        column: x => x.SupperId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyMealPlan_BreakfastId",
                table: "DailyMealPlan",
                column: "BreakfastId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMealPlan_DinnerId",
                table: "DailyMealPlan",
                column: "DinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMealPlan_LunchId",
                table: "DailyMealPlan",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMealPlan_RecommendedMealPlanId",
                table: "DailyMealPlan",
                column: "RecommendedMealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMealPlan_SnapId",
                table: "DailyMealPlan",
                column: "SnapId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMealPlan_SupperId",
                table: "DailyMealPlan",
                column: "SupperId");

            migrationBuilder.CreateIndex(
                name: "IX_Portion_MealId",
                table: "Portion",
                column: "MealId");
        }
    }
}
