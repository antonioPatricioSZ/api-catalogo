using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Exceptions;
using AutoMapper;

namespace APICatalogo.Application.UseCases.Category.GetById;

public class GetCategoryByIdUseCase : IGetCategoryByIdUseCase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoriaResponseJson> Execute(int id) {

        var categoria = await _unitOfWork.CategoriaRepository.Get(
            c => c.Id == id
        ) ?? throw new NotFoundException("Categoria não encontrada.");

        var response = _mapper.Map<CategoriaResponseJson>(categoria);

        return response;

    }
}
