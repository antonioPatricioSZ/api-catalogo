using APICatalogo.Communication.Requests;
using APICatalogo.Domain.Entities;
using APICatalogo.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Application.UseCases.User.Register;

public class RegisterUseCase : IRegisterUseCase {

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<RegisterModelJson> _validator;

    public RegisterUseCase(UserManager<ApplicationUser> userManager, IValidator<RegisterModelJson> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<(string Id, string username)> Execute(RegisterModelJson model) {

        _validator.ValidateAndThrow(model);

        var userExists = await _userManager.FindByNameAsync(model.UserName!);

        if (userExists is not null) {

            throw new ValidationErrorException(
                ["E-mail já registrado."]
            );
        }

        ApplicationUser user = new() {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName,
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        return (user.Id, user.UserName);
    }
}
