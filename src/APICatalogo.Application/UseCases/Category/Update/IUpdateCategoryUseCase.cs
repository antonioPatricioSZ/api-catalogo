using APICatalogo.Communication.Requests;

namespace APICatalogo.Application.UseCases.Category.Update;

public interface IUpdateCategoryUseCase {

    Task Execute(int id, CategoriaRequestJson requestDTO);

}
