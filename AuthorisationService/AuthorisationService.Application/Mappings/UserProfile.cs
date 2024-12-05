using AutoMapper;
using AuthorisationService.Application.DTOs;
using AuthorisationService.Domain.Entities;


namespace AuthorisationService.Application.Mappings
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => HashPassword(src.Password)))
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }

}