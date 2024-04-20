using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;

namespace APICatalogo.Application.UseCases.Product.GetProdutosFilterPreco;

public interface IGetProdutosFilterPrecoUseCase {

    Task<IEnumerable<ProductResponseJson>> Execute(ProdutosFiltroPreco produtosParameters);

}
