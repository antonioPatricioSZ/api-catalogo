using APICatalogo.Communication.Responses;

namespace APICatalogo.Application.UseCases.Product.GetProdutosCategoria;

public interface IGetProdutosCategoriaUseCase {

    Task<IEnumerable<ProductResponseJson>> Execute(int categoryId);

}
