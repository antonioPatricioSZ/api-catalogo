using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Entities;
using APICatalogo.Domain.Repositories.UnitOfWork;
using AutoMapper;
using FluentValidation;

namespace APICatalogo.Application.UseCases.Category.Create;

public class CreateCategoryUseCase : ICreateCategoryUseCase {

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CategoriaRequestJson> _validator;

    public CreateCategoryUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<CategoriaRequestJson> validator
    ){
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<CategoriaResponseJson> Execute(
        CategoriaRequestJson request
    ){

        _validator.ValidateAndThrow(request);

        var categoria = _mapper.Map<Categoria>(request);

        var novaCategoria = _unitOfWork.CategoriaRepository.Create(categoria);
        await _unitOfWork.Commit();

        var CategoriaDto = _mapper.Map<CategoriaResponseJson>(novaCategoria);

        return CategoriaDto;

    }

}
