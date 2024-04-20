using APICatalogo.Communication.Requests;
using APICatalogo.Domain.Entities;
using APICatalogo.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Application.UseCases.Auth.AddUserToRole;

public class AddUserToRoleUseCase : IAddUserToRoleUseCase {

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AddUserToRoleUseCase(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
    ){
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Execute(RequestAddUserToRoleJson request) {

        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new ForbidenException("Usuário não possui permissão."); ;

        var roleExist = await _roleManager
            .RoleExistsAsync(request.RoleName);

        if (!roleExist)
            throw new NotFoundException("Role não existe.");

        var userHaveRole = await _userManager
            .IsInRoleAsync(user, request.RoleName);

        if (userHaveRole) {
            throw new ValidationErrorException(
                ["Usuário já possui esta role."]    
            );
        }

        await _userManager.AddToRoleAsync(user, request.RoleName); 

    }

}
