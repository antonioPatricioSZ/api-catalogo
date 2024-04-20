using APICatalogo.Communication.Responses;

namespace APICatalogo.Application.UseCases.Product.GetById;

public interface IGetByIdUseCase {

    Task<ProductResponseJson> Execute(int id);

}
