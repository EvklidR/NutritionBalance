using Microsoft.Extensions.DependencyInjection;
using MealPlanService.Application.Validators;
using System.Reflection;
using FluentValidation;
using MealPlanService.Application.Interfaces.EventsUseCases;
using MealPlanService.Application.Interfaces.ParticipantsUseCases;
using MealPlanService.Application.UseCases.EventsUseCases;
using MealPlanService.Application.UseCases.ParticipantsUseCases;

namespace MealPlanService.Application.DependencyInjection
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
