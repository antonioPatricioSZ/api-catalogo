using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;

namespace APICatalogo.Application.UseCases.User.Login;

public interface ILoginUseCase
{

    Task<ResponseLoginJson> Execute(RequestLoginJson loginDTO);

}
