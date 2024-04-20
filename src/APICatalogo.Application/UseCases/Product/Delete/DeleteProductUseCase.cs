using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Exceptions;

namespace APICatalogo.Application.UseCases.Product.Delete;

public class DeleteProductUseCase : IDeleteProductUseCase
{

    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductUseCase(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id) {

        var produto = await _unitOfWork.ProdutoRepository.GetUpdate(
            produto => produto.Id == id
        ) ?? throw new NotFoundException("Produto não encontrado.");

        _unitOfWork.ProdutoRepository.Delete(produto);
        await _unitOfWork.Commit();

    }

}
