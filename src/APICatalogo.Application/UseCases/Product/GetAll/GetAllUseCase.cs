using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace APICatalogo.Application.UseCases.Product.GetAll;

public class GetAllUseCase : IGetAllUseCase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<ProductResponseJson>> Execute(ProdutosParameters produtosParameters) {
        
        var produtos = await _unitOfWork.ProdutoRepository.GetProdutosPaginados(produtosParameters);

        var metadata = new {
            produtos.ItemsCount,
            produtos.PageSize,
            produtos.Page,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        _contextAccessor?.HttpContext?.Response.Headers.Append(
            "X-Pagination",
            JsonConvert.SerializeObject(metadata)
        );

        var produtosMap = _mapper.Map<IEnumerable<ProductResponseJson>>(produtos.Items);

        return produtosMap;
    }
}
