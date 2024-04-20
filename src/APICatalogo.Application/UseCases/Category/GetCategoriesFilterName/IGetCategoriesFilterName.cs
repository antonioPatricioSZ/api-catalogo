using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;

namespace APICatalogo.Application.UseCases.Category.GetProdutosFilterPreco;

public interface IGetCategoriesFilterName {

    Task<IEnumerable<CategoriaResponseJson>> Execute(CategoriasFiltroNome categoriasFiltroNome);

}
