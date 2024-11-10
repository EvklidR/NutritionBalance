using Microsoft.Extensions.DependencyInjection;
using UserProfileService.Application.Validators;
using System.Reflection;
using FluentValidation;
using UserProfileService.Application.Interfaces.EventsUseCases;
using UserProfileService.Application.Interfaces.ParticipantsUseCases;
using UserProfileService.Application.UseCases.EventsUseCases;
using UserProfileService.Application.UseCases.ParticipantsUseCases;

namespace UserProfileService.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {


            services.AddValidatorsFromAssemblyContaining<CreateEventDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateProfileDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateEventDtoValidator>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICreateEvent, CreateEvent>();
            services.AddScoped<IGetEventById, GetEventById>();
            services.AddScoped<IGetEventByName, GetEventByName>();
            services.AddScoped<IUpdateEvent, UpdateEvent>();
            services.AddScoped<IDeleteEvent, DeleteEvent>();
            services.AddScoped<IGetFilteredEvents, GetFilteredEvents>();

            services.AddScoped<IRegisterUserForEvent, RegisterUserForEvent>();
            services.AddScoped<IUnregisterUserFromEvent, UnregisterUserFromEvent>();
            services.AddScoped<IGetUserEvents, GetUserEvents>();
            services.AddScoped<IGetParticipantById, GetParticipantById>();
            services.AddScoped<IGetEventParticipants, GetEventParticipants>();
            services.AddScoped<INotifyParticipants, NotifyParticipants>();

            services.AddScoped<EventsUseCasesFacade>();
            services.AddScoped<ParticipantsUseCasesFacade>();

            return services;

        }
    }
}
