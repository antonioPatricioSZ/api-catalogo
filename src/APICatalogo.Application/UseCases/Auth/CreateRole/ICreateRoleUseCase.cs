using APICatalogo.Communication.Requests;

namespace APICatalogo.Application.UseCases.Auth.CreateRole;

public interface ICreateRoleUseCase {

    Task Execute(RequestCreateRoleJson request);

}
