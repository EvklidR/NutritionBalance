using Grpc.Net.Client;
using MealPlanService.Grpc;
using UserProfileService.Application.Interfaces;
using UserProfileService.Application.Models;

namespace UserProfileService.Infrastructure.Grpc
{
    public class MealPlanServiceClient : IMealPlanService
    {
        private readonly MealPlanService.Grpc.MealPlanService.MealPlanServiceClient _client;

        public MealPlanServiceClient(string address)
        {
            var channel = GrpcChannel.ForAddress(address);
            _client = new MealPlanService.Grpc.MealPlanService.MealPlanServiceClient(channel);
        }

        public async Task<DailyNeedsResponse> GetDailyNeedsByMealPlanAsync(int mealPlanId, double bodyWeight, double dailyKcal, string startDate)
        {
            var request = new GetKcalAndMacrosRequest
            {
                MealPlanId = mealPlanId,
                BodyWeight = bodyWeight,
                DailyKcal = dailyKcal,
                StartDate = startDate
            };

            var response = await _client.CalculateKcalAndMacrosAsync(request);
            return new DailyNeedsResponse(response.Calories, response.Proteins, response.Fats, response.Carbohydrates);
        }
    }
}