namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class DailyPlanDto
    {
        public uint Day { get; set; }
        public MealDto Breakfast { get; set; }
        public MealDto Snap { get; set; }
        public MealDto Lunch { get; set; }
        public MealDto Dinner { get; set; }
        public MealDto Supper { get; set; }
    }
}