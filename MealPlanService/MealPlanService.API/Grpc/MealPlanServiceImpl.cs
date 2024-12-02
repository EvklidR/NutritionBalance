using Grpc.Core;
using MealPlanService.Grpc;
using MealPlanService.Application.UseCases;
using MediatR;

public class MealPlanServiceImpl : MealPlanService.Grpc.MealPlanService.MealPlanServiceBase
{
    private readonly IMediator _mediator;

    public MealPlanServiceImpl(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetKcalAndMacrosResponse> CalculateKcalAndMacros(GetKcalAndMacrosRequest request, ServerCallContext context)
    {
        var query = new GetKcalAndMacrosQuery(
            request.MealPlanId,
            request.BodyWeight,
            request.DailyKcal,
            DateOnly.Parse(request.StartDate)
        );

        var result = await _mediator.Send(query);

        return new GetKcalAndMacrosResponse
        {
            Calories = result.Calories,
            Proteins = result.Proteins,
            Fats = result.Fats,
            Carbohydrates = result.Carbohydrates
        };
    }
}
