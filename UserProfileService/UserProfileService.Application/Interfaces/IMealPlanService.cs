using System;
using UserProfileService.Application.Models;

namespace UserProfileService.Application.Interfaces
{
    public interface IMealPlanService
    {
        Task<DailyNeedsResponse> GetDailyNeedsByMealPlanAsync(
            int mealPlanId,
            double bodyWeight,
            double dailyKcal,
            string startDate);
    }
}
