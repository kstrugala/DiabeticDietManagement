using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.RecommendedMealPlan
{
    public class UpdateRecommendedMealPlanHandler : ICommandHandler<UpdateRecommendedMealPlan>
    {
        private readonly IPatientService _patientService;
        public UpdateRecommendedMealPlanHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task HandleAsync(UpdateRecommendedMealPlan command)
        {
            await _patientService.UpdateRecommendedMealPlanAsync(command);
        }
    }
}
