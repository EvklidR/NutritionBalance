using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class GetIngredientsQuery : IRequest<IEnumerable<Domain.Entities.Ingredient>?>
    {
        public int  ProfileId { get; set; }

        public GetIngredientsQuery(int profileId)
        {
            ProfileId = profileId;
        }
    }
}
