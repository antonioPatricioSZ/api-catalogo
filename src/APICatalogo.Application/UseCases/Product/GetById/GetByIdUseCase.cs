using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Exceptions;
using AutoMapper;

namespace APICatalogo.Application.UseCases.Product.GetById;

public class GetByIdUseCase : IGetByIdUseCase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductResponseJson> Execute(int id) {

        var produto = await _unitOfWork.ProdutoRepository.Get(
            produto => produto.Id == id
        ) ?? throw new NotFoundException("Produto não encontrado.");

        var response = _mapper.Map<ProductResponseJson>(produto);

        return response;

    }
}
