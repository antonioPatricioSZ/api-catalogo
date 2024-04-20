using APICatalogo.Communication.Requests;

namespace APICatalogo.Application.UseCases.User.Register;

public interface IRegisterUseCase {

    Task<(string Id, string username)> Execute(RegisterModelJson model);

}
