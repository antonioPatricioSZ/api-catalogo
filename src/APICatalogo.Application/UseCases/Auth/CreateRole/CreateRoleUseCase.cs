using APICatalogo.Communication.Requests;
using APICatalogo.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Application.UseCases.Auth.CreateRole;

public class CreateRoleUseCase : ICreateRoleUseCase {

    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateRoleUseCase(
        RoleManager<IdentityRole> roleManager
    ){
        _roleManager = roleManager;
    }

    public async Task Execute(RequestCreateRoleJson request) {

        if(string.IsNullOrWhiteSpace(request.RoleName)) {
            throw new ValidationErrorException(
                ["Informe uma role."]
            );
        }

        var roleExist = await _roleManager.RoleExistsAsync(request.RoleName);


        if (roleExist) {
            throw new ValidationErrorException(
                ["Role já existe."]
            );
        }

        await _roleManager.CreateAsync(
            new IdentityRole(request.RoleName)
        );

    }

}
