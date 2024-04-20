using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;

namespace APICatalogo.Application.UseCases.Product.GetAll;

public interface IGetAllUseCase {

    Task<IEnumerable<ProductResponseJson>> Execute(ProdutosParameters produtosParameters);

}
