using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace APICatalogo.Application.UseCases.Category.GetProdutosFilterPreco;

public class GetCategoriesFilterName : IGetCategoriesFilterName
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetCategoriesFilterName(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor contextAccessor
    ){
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<CategoriaResponseJson>> Execute(CategoriasFiltroNome categoriasFiltroNome) {

        var categorias = await _unitOfWork.CategoriaRepository.GetCategoriasFiltroNome(categoriasFiltroNome);

        var metadata = new
        {
            categorias.ItemsCount,
            categorias.PageSize,
            categorias.Page,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        _contextAccessor.HttpContext.Response.Headers.Append(
            "X-Pagination",
            JsonConvert.SerializeObject(metadata)
        );

        var categoriasDto = _mapper.Map<IEnumerable<CategoriaResponseJson>>(categorias.Items);

        return categoriasDto;

    }

}
