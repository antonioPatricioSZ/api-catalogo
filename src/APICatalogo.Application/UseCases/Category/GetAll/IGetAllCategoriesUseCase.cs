using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;

namespace APICatalogo.Application.UseCases.Category.GetAll;

public interface IGetAllCategoriesUseCase {

    Task<IEnumerable<CategoriaResponseJson>> Execute(CategoriasParameters categoriasParameters);

}
