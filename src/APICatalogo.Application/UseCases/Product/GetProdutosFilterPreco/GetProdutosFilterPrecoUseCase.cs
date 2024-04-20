using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace APICatalogo.Application.UseCases.Product.GetProdutosFilterPreco;

public class GetProdutosFilterPrecoUseCase : IGetProdutosFilterPrecoUseCase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetProdutosFilterPrecoUseCase(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor contextAccessor
    ){
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<ProductResponseJson>> Execute(ProdutosFiltroPreco produtosFiltroPreco) {

        var produtos = await _unitOfWork.ProdutoRepository.GetProdutosFiltroPreco(produtosFiltroPreco);

        var metadata = new
        {
            produtos.ItemsCount,
            produtos.PageSize,
            produtos.Page,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        _contextAccessor.HttpContext.Response.Headers.Append(
            "X-Pagination",
            JsonConvert.SerializeObject(metadata)
        );

        var produtosDto = _mapper.Map<IEnumerable<ProductResponseJson>>(produtos.Items);

        return produtosDto;

    }

}
