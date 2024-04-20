using APICatalogo.Domain.Entities;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.Repository;

namespace APICatalogo.Domain.Repositories.CategoriasRepository;

public interface ICategoriaRepository
    : IRepository<Categoria> {

    Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
    Task<PagedList<Categoria>> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams);
}
