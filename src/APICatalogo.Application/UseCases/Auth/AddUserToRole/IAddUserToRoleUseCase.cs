using APICatalogo.Communication.Requests;

namespace APICatalogo.Application.UseCases.Auth.AddUserToRole;

public interface IAddUserToRoleUseCase {

    Task Execute(RequestAddUserToRoleJson request);

}
