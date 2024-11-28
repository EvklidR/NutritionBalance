using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Application.Models;
using UserProfileService.Domain.Enums;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class CalculateDailyNeedsHandler : IRequestHandler<CalculateDailyNeedsQuery, DailyNeedsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalculateDailyNeedsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            DailyNeedsResponse response;

            if (profile.MealPlanId == null) 
            {
                response = CalculateDailyMacros(profile);
            } else
            {
                response = new DailyNeedsResponse();
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
            return CalculateBMR(profile) * profile.ActivityLevel;
        }

        // Примерные пропорции КБЖУ (протеины, жиры, углеводы)
        private DailyNeedsResponse CalculateDailyMacros(Domain.Entities.Profile profile)
        {
            DailyNeedsResponse result = new DailyNeedsResponse();
            var calories = CalculateDailyCalories(profile);
            result.Calories = calories;
            result.Proteins = 0.3 * calories / 4; // 30% белков, 1 грамм белка = 4 ккал
            result.Fats = 0.3 * calories / 9; // 30% жиров, 1 грамм жира = 9 ккал
            result.Carbohydrates = 0.4 * calories / 4; // 40% углеводов, 1 грамм углеводов = 4 ккал
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

