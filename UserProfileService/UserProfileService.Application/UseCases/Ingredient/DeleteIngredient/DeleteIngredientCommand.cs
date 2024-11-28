using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class DeleteIngredientCommand : IRequest
    {
        public int IngredientId { get; set; }

        public DeleteIngredientCommand(int ingredientId)
        {
            IngredientId = ingredientId;
        }
    }
}
