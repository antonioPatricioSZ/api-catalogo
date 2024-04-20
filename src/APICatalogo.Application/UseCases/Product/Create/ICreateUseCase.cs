using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;

namespace APICatalogo.Application.UseCases.Product.Create;

public interface ICreateUseCase {

    Task<ProductResponseJson> Execute(ProductRequestJson request);

}
