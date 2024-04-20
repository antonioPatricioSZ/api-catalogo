using APICatalogo.Communication.Requests;

namespace APICatalogo.Application.UseCases.Product.Update;

public interface IUpdateProductUseCase {

    Task Execute(int id, ProductRequestJson requestDTO);

}
