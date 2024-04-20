using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Repositories.UnitOfWork;
using AutoMapper;

namespace APICatalogo.Application.UseCases.Product.GetProdutosCategoria;

public class GetProdutosCategoriaUseCase : IGetProdutosCategoriaUseCase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 

    public GetProdutosCategoriaUseCase(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseJson>> Execute(int categoryId) {

        var produtos = await _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(categoryId);

        var produtosDto = _mapper.Map<IEnumerable<ProductResponseJson>>(produtos);

        return produtosDto;
    }
    
}
