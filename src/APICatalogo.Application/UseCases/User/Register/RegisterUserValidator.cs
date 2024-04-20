using APICatalogo.Communication.Requests;
using FluentValidation;

namespace APICatalogo.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterModelJson> {

    public RegisterUserValidator() {

        RuleFor(request => request.UserName).NotEmpty()
            .WithMessage("Error username is empty");

        RuleFor(request => request.Email).NotEmpty().WithMessage("Error");

        When(request => !string.IsNullOrWhiteSpace(request.Email), () => {
            RuleFor(request => request.Email)
                .EmailAddress().WithMessage("Email inválido");
        });

    }

}
