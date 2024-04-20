using APICatalogo.Communication.Responses;

namespace APICatalogo.Application.UseCases.Category.GetById;

public interface IGetCategoryByIdUseCase {

    Task<CategoriaResponseJson> Execute(int id);

}
