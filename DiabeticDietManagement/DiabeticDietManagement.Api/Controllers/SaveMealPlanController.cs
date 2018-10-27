using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DiabeticDietManagement.Api.Controllers
{
    public class SaveMealPlanController : Controller
    {
        [Authorize(Policy = "doctor")]
        [HttpPost("api/savemealplan")]
        public IActionResult Index([FromBody] SaveRecommendedMealPlan mealPlan)
        {
            string jsonString = JsonConvert.SerializeObject(mealPlan, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
            return File(bytes, "application/json", $"plan-{DateTime.Now.ToString()}.json");
        }
    }
}