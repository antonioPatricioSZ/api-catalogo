using APICatalogo.Application.Services.Token;
using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Entities;
using APICatalogo.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Application.UseCases.User.Login;

public class LoginUseCase : ILoginUseCase
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public LoginUseCase(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService
    )
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<ResponseLoginJson> Execute(RequestLoginJson loginDTO)
    {
        var user = await _userManager.FindByNameAsync(loginDTO.UserName!);

        var checkPassword = await _userManager
            .CheckPasswordAsync(user, loginDTO.Password);

        if (user is null || !checkPassword)
        {
            throw new InvalidLoginException();
        }

        var token = await _tokenService.GenerateAccessToken(user);

        await _userManager.UpdateAsync(user);

        return new ResponseLoginJson
        {
            Token = token,
        };
    }
}
