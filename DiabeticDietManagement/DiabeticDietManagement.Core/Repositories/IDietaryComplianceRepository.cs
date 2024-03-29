﻿using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Domain.Enums;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IDietaryComplianceRepository : IRepository
    {
        Task<DietaryCompliance> GetOneAsync(Guid id);
        Task<PagedResult<DietaryCompliance>> GetPagedAsync(Guid patientId, DietaryComplianceQuery query);
        Task<DietaryCompliance> GetAsync(Guid patientId, DateTime date, MealType mealType);
        Task<IEnumerable<DietaryCompliance>> GetAsync(Guid patientId);
        Task AddAsync(DietaryCompliance dietaryCompliance);
        Task UpdateAsync(DietaryCompliance dietaryCompliance);
        Task RemoveAsync(Guid id);
        Task RemovePatientDietaryComplianceAsync(Guid patientId);
    }
}
