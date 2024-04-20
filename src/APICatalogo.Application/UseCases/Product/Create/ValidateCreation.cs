using APICatalogo.Communication.Requests;
using APICatalogo.Exceptions;
using FluentValidation;

namespace APICatalogo.Application.UseCases.Product.Create;

public class ValidateCreateProduct : AbstractValidator<ProductRequestJson> {

    public ValidateCreateProduct() {

        RuleFor(request => request.Nome)
           .NotEmpty()
           .WithMessage(ResourceErrorMessages.NAME_PRODUCT_EMPTY);

        RuleFor(request => request.Descricao)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.DESCRIPTION_EMPTY);

        RuleFor(request => request.Preco)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.PRICE_EMPTY)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.PRICE_MIN);

        RuleFor(request => request.ImagemUrl)
            .NotEmpty().WithMessage(
                ResourceErrorMessages.URL_IMAGE_PRODUCT_EMPTY
            )
            .MaximumLength(255).WithMessage(
                ResourceErrorMessages.URL_IMAGE_PRODUCT_MAX
            );

        RuleFor(request => request.Estoque)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.STOCK_EMPTY)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.STOCK_MIN);

    }

}
