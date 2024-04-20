using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Exceptions;

namespace APICatalogo.Application.UseCases.Category.Delete;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase {

    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryUseCase(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id) {

        var categoria = await _unitOfWork.CategoriaRepository.GetUpdate(
            c => c.Id == id
        ) ?? throw new NotFoundException("Categoria não encontrada.");

        _unitOfWork.CategoriaRepository.Delete(categoria);
        await _unitOfWork.Commit();

    }

}
