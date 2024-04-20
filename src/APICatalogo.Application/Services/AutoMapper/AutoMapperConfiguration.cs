using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Entities;
using AutoMapper;

namespace APICatalogo.Communication.DTOs.AutoMapper;

public class AutoMapperConfiguration : Profile
{

    public AutoMapperConfiguration()
    {
        CreateMap<ProductRequestJson, Produto>();
        CreateMap<Produto, ProductResponseJson>();
        CreateMap<CategoriaRequestJson, Categoria>();
        CreateMap<Categoria, CategoriaResponseJson>()
            .ForMember(
                destino => destino.CategoriaId,
                config => config.MapFrom(origem => origem.Id)
            );
    }

}
