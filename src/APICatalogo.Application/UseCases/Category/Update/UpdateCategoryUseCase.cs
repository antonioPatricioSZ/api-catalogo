using APICatalogo.Communication.Requests;
using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Exceptions;
using AutoMapper;

namespace APICatalogo.Application.UseCases.Category.Update;

public class UpdateCategoryUseCase : IUpdateCategoryUseCase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(int id, CategoriaRequestJson requestDTO) {

        var categoria = await _unitOfWork.CategoriaRepository.GetUpdate(
            p => p.Id == id
        ) ?? throw new NotFoundException("Categoria não encontrada.");

        _mapper.Map(requestDTO, categoria);

        _unitOfWork.CategoriaRepository.Update(categoria);
        await _unitOfWork.Commit();

    }

}
