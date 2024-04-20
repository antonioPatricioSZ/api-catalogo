using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;

namespace APICatalogo.Application.UseCases.Category.Create;

public interface ICreateCategoryUseCase {

    Task<CategoriaResponseJson> Execute(CategoriaRequestJson request);

}
