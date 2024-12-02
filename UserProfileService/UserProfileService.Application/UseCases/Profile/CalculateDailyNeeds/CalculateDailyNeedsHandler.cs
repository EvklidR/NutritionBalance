using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Application.Interfaces;
using UserProfileService.Application.Models;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Enums;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class CalculateDailyNeedsHandler : IRequestHandler<CalculateDailyNeedsQuery, DailyNeedsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealPlanService _mealPlanService;

        public CalculateDailyNeedsHandler(IUnitOfWork unitOfWork, IMealPlanService mealPlanService)
        {
            _unitOfWork = unitOfWork;
            _mealPlanService = mealPlanService;
        }

        public async Task<DailyNeedsResponse> Handle(
            CalculateDailyNeedsQuery request,
            CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);

            if (profile == null)
            {
                throw new NotFoundException("Profile not found");
            }

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            DailyNeedsResponse response;

            if (profile.MealPlanId == null) 
            {
                response = CalculateDailyMacros(profile);
            } else
            {
                response = await _mealPlanService.GetDailyNeedsByMealPlanAsync(
                    (int)profile.MealPlanId,
                    profile.Weight,
                    CalculateDailyCalories(profile),
                    profile.DateOfStartPlan.ToString());
            }

            return response;

        }

        // Суточная норма калорий (рассчитывается через формулу Харриса-Бенедикта)
        private double CalculateBMR(Domain.Entities.Profile profile)
        {
            if (profile.Gender == Gender.Male)
            {
                return 88.36 + 
                    13.4 * profile.Weight + 
                    4.8 * profile.Height - 
                    5.7 * CalculateAge(profile.Birthday);
            }
            else
            {
                return 447.6 + 
                    9.2 * profile.Weight + 
                    3.1 * profile.Height - 
                    4.3 * CalculateAge(profile.Birthday);
            }
        }

        // Умножаем на коэффициент активности для получения суточной нормы калорий
        private double CalculateDailyCalories(Domain.Entities.Profile profile)
        {
            return Math.Round(CalculateBMR(profile) * profile.ActivityLevel, 2);
        }


        // Примерные пропорции КБЖУ (протеины, жиры, углеводы)
        private DailyNeedsResponse CalculateDailyMacros(Domain.Entities.Profile profile)
        {
            DailyNeedsResponse result = new DailyNeedsResponse();
            var calories = CalculateDailyCalories(profile);
            result.Calories = Math.Round(calories, 2);
            result.Proteins = Math.Round(0.3 * calories / 4, 2);
            result.Fats = Math.Round(0.3 * calories / 9, 2);
            result.Carbohydrates = Math.Round(0.4 * calories / 4, 2);
            return result;
        }


        public int CalculateAge(DateOnly birthday)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - birthday.Year;

            // Проверяем, если день рождения еще не наступил в этом году
            if (today < birthday.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}

