using FluentValidation;
using UserProfileService.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace UserProfileService.Application.Validators
{
    public class CreateDishDTOValidator : AbstractValidator<CreateDishDTO>
    {
        public CreateDishDTOValidator()
        {
            RuleFor(d => d.ProfileId)
                .GreaterThan(0).WithMessage("ProfileId must be greater than zero.");

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(d => d.Image)
                .Must(BeValidImage).When(d => d.Image != null)
                .WithMessage("Invalid image file. Supported formats: JPEG, PNG.");

            RuleFor(d => d.AmountOfPortions)
                .GreaterThan(0);

            RuleForEach(d => d.Ingredients)
                .SetValidator(new IngredientOfDishDTOValidator());
        }

        private bool BeValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }
}
