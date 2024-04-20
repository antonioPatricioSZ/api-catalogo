using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Entities;
using APICatalogo.Domain.Repositories.UnitOfWork;
using AutoMapper;
using FluentValidation;

namespace APICatalogo.Application.UseCases.Product.Create;

public class CreateUseCase : ICreateUseCase
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ProductRequestJson> _validator;

    public CreateUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<ProductRequestJson> validator
    ){
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<ProductResponseJson> Execute(
        ProductRequestJson request
    ){

        _validator.ValidateAndThrow(request);

        var produto = _mapper.Map<Produto>(request);

        var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
        await _unitOfWork.Commit();

        var produtoDto = _mapper.Map<ProductResponseJson>(novoProduto);

        return produtoDto;

    }

}
