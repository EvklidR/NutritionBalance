using Moq;
using UserProfileService.Application.UseCases.Profile;
using UserProfileService.Application.Models;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Enums;
using UserProfileService.Application.Interfaces;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.UnitTests {
    public class CalculateDailyNeedsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMealPlanService> _mockMealPlanService;
        private readonly CalculateDailyNeedsHandler _handler;

        public CalculateDailyNeedsHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMealPlanService = new Mock<IMealPlanService>();

            _handler = new CalculateDailyNeedsHandler(_mockUnitOfWork.Object, _mockMealPlanService.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenProfileNotFound()
        {
            // Arrange
            var query = new CalculateDailyNeedsQuery(1, 1);

            _mockUnitOfWork.Setup(x => x.ProfileRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Profile)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenUserIsNotOwner()
        {
            // Arrange
            var query = new CalculateDailyNeedsQuery(1, 2); // userId = 2, profile.UserId = 1

            var profile = new Profile { UserId = 1 };

            _mockUnitOfWork.Setup(x => x.ProfileRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(profile);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnDailyNeeds_WhenNoMealPlanId()
        {
            // Arrange
            var query = new CalculateDailyNeedsQuery(1, 1);
            var profile = new Profile
            {
                UserId = 1,
                Weight = 70,
                Height = 175,
                Gender = Gender.Male,
                Birthday = new DateOnly(1990, 1, 1),
                ActivityLevel = 1.5
            };

            _mockUnitOfWork.Setup(x => x.ProfileRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(profile);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Calories > 0);
            Assert.True(result.Proteins > 0);
            Assert.True(result.Fats > 0);
            Assert.True(result.Carbohydrates > 0);
        }

        [Fact]
        public async Task Handle_ShouldReturnDailyNeeds_WhenHasMealPlanId()
        {
            // Arrange
            var query = new CalculateDailyNeedsQuery(1, 1);
            var profile = new Profile
            {
                UserId = 1,
                Weight = 70,
                Height = 175,
                Gender = Gender.Male,
                Birthday = new DateOnly(1990, 1, 1),
                ActivityLevel = 1.5,
                MealPlanId = 1,
                DateOfStartPlan = DateOnly.FromDateTime(DateTime.Now)
            };

            var dailyNeedsResponse = new DailyNeedsResponse
            {
                Calories = 2000,
                Proteins = 150,
                Fats = 80,
                Carbohydrates = 250
            };

            _mockUnitOfWork.Setup(x => x.ProfileRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(profile);
            _mockMealPlanService.Setup(x => x.GetDailyNeedsByMealPlanAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()))
                .ReturnsAsync(dailyNeedsResponse);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dailyNeedsResponse.Calories, result.Calories);
            Assert.Equal(dailyNeedsResponse.Proteins, result.Proteins);
            Assert.Equal(dailyNeedsResponse.Fats, result.Fats);
            Assert.Equal(dailyNeedsResponse.Carbohydrates, result.Carbohydrates);
        }
    }

}
