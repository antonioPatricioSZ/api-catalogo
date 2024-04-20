using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace APICatalogo.Application.UseCases.Category.GetAll;

public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllCategoriesUseCase(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<CategoriaResponseJson>> Execute(CategoriasParameters categoriasParameters) {
        
        var categorias = await _unitOfWork.CategoriaRepository.GetCategorias(categoriasParameters);

        var metadata = new {
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

        var categoriasMap = _mapper.Map<IEnumerable<CategoriaResponseJson>>(categorias.Items);

        return categoriasMap;
    }
}
