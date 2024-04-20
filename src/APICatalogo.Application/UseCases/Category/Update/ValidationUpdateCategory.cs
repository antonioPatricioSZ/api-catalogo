using APICatalogo.Communication.Requests;
using APICatalogo.Exceptions;
using FluentValidation;

namespace APICatalogo.Application.UseCases.Category.Update;

public class ValidationUpdateCategory 
    : AbstractValidator<CategoriaUpdateRequestJson> {

    public ValidationUpdateCategory() {

        RuleFor(request => request.Nome)
            .NotEmpty().WithMessage(
                ResourceErrorMessages.NAME_CATEGORY_EMPTY
            );

        RuleFor(request => request.ImagemUrl)
            .NotEmpty().WithMessage(
                ResourceErrorMessages.URL_IMAGE_CATEGORY_EMPTY
            )
            .MaximumLength(255).WithMessage(
                ResourceErrorMessages.URL_IMAGE_CATEGORY_MAX
            );

    }

}
