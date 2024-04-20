using APICatalogo.Domain.Entities;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.CategoriasRepository;
using APICatalogo.Infrastructure.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Infrastructure.Repositories.CategoriasRepository;

public class CategoriaRepository
    : Repository<Categoria>, ICategoriaRepository
{

    public CategoriaRepository(
        AppDbContext _context
    ) : base(_context)
    {

    }

    public async Task<PagedList<Categoria>> GetCategorias(
        CategoriasParameters categoriasParameters
    ){
        var categoriasOrdenadas = _context.Categorias.AsNoTracking().OrderBy(categoria => categoria.Id)
            .AsQueryable();

        var resultado = await categoriasOrdenadas.GetPaged<Categoria>(
            categoriasParameters.PageNumber,
            categoriasParameters.PageSize
        );

        return resultado;
    }

    public async Task<PagedList<Categoria>> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams)
    {

        IQueryable<Categoria> categorias = _context.Categorias
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(categoriasParams.Nome)) {
            categorias = categorias.Where(
                categoria => categoria.Nome.Contains(categoriasParams.Nome, StringComparison.InvariantCultureIgnoreCase)
            );
        }

        var categoriasFiltradas = await categorias.GetPaged<Categoria> (
            categoriasParams.PageNumber,
            categoriasParams.PageSize
        );

        return categoriasFiltradas;
    }
}
