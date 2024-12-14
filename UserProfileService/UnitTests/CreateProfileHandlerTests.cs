using Moq;
using UserProfileService.Application.UseCases.Profile;
using UserProfileService.Application.DTOs;
using UserProfileService.Application.Exceptions;
using UserProfileService.Application.Interfaces;
using UserProfileService.Domain.Interfaces;
using AutoMapper;

namespace UserProfileService.UnitTests
{
    public class CreateProfileHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserService> _mockUserService;
        private readonly IMapper _mapper;
        private readonly CreateProfileHandler _handler;

        public CreateProfileHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserService = new Mock<IUserService>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Application.Mappings.ProfileMappingProfile());
            });
            _mapper = new Mapper(configuration);

            _handler = new CreateProfileHandler(_mockUnitOfWork.Object, _mapper, _mockUserService.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateProfile_WhenValidData()
        {
            // Arrange
            var profileDto = new CreateProfileDTO { UserId = 1, Name = "John" };
            var command = new CreateProfileCommand(profileDto);

            _mockUserService.Setup(x => x.CheckUserByIdAsync(It.IsAny<int>())).ReturnsAsync(true);

            _mockUnitOfWork.Setup(x => x.ProfileRepository.GetAllAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Domain.Entities.Profile>());

            _mockUnitOfWork.Setup(x => x.ProfileRepository.Add(It.IsAny<Domain.Entities.Profile>())).Verifiable();
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(x => x.ProfileRepository.Add(It.IsAny<UserProfileService.Domain.Entities.Profile>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenUserNotFound()
        {
            // Arrange
            var profileDto = new CreateProfileDTO { UserId = 1, Name = "John" };
            var command = new CreateProfileCommand(profileDto);

            _mockUserService.Setup(x => x.CheckUserByIdAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowAlreadyExistsException_WhenProfileNameExists()
        {
            // Arrange
            var profileDto = new CreateProfileDTO { UserId = 1, Name = "John" };
            var command = new CreateProfileCommand(profileDto);

            var existingProfiles = new List<UserProfileService.Domain.Entities.Profile>
            {
                new UserProfileService.Domain.Entities.Profile { UserId = 1, Name = "John" }
            };

            _mockUserService.Setup(x => x.CheckUserByIdAsync(It.IsAny<int>())).ReturnsAsync(true);
            _mockUnitOfWork.Setup(x => x.ProfileRepository.GetAllAsync(It.IsAny<int>())).ReturnsAsync(existingProfiles);

            // Act & Assert
            await Assert.ThrowsAsync<AlreadyExistsException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
