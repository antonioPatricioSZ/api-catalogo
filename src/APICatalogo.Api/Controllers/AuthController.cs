using APICatalogo.Application.UseCases.Auth.AddUserToRole;
using APICatalogo.Application.UseCases.Auth.CreateRole;
using APICatalogo.Application.UseCases.User.Login;
using APICatalogo.Application.UseCases.User.Register;
using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Api.Controllers;
[Route("[controller]")]
[ApiController]

public class AuthController : ControllerBase {

    [HttpPost("create-role")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status400BadRequest)]
    //[Authorize(Policy = "UserSuperAdmin")]
    public async Task<IActionResult> CreateRole(
        [FromBody] RequestCreateRoleJson request,
        ICreateRoleUseCase useCase
    ){

        await useCase.Execute(request);

        return NoContent();

    }


    [HttpPost("add-user-role")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status400BadRequest)]
    //[Authorize(Policy = "UserSuperAdmin")]
    public async Task<IActionResult> AddUserToRole(
        RequestAddUserToRoleJson request,
        IAddUserToRoleUseCase useCase   
    ){

        await useCase.Execute(request);

        return NoContent();

    }
  

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        RequestLoginJson model,
        ILoginUseCase useCase
    ){

        var result = await useCase.Execute(model);

        return Ok(result);

    }


    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        RegisterModelJson model,
        IRegisterUseCase useCase
    ){

        var (id, username) = await useCase.Execute(model);

        return Created(string.Empty, new { id, username });

    }

}
