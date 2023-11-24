using FluentValidation;
using POS.Application.Dtos.Request;

namespace POS.Application.Validators.Category
{
    public class CategoryValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Nombre no puede ser vacío");

            RuleFor(c => c.Description).NotEmpty();

        }
    }
}
