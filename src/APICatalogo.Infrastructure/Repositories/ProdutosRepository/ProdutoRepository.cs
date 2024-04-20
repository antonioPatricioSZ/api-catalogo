using APICatalogo.Domain.Entities;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.ProdutosRepository;
using APICatalogo.Infrastructure.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Infrastructure.Repositories.ProdutosRepository;

public class ProdutoRepository
    : Repository<Produto>, IProdutoRepository
{

    public ProdutoRepository(
        AppDbContext _context
    ) : base(_context)
    { }

    public async Task<PagedList<Produto>> GetProdutosPaginados(ProdutosParameters produtosParameters) {

        var produtosOrdenados = _context.Produtos.AsNoTracking().OrderBy(produto => produto.Id)
            .AsQueryable();

        var resultado = await produtosOrdenados.GetPaged<Produto>(
            produtosParameters.PageNumber,
            produtosParameters.PageSize
        );

        return resultado;
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPreco(
        ProdutosFiltroPreco produtosFiltroParams
    )
    {

        IQueryable<Produto> produtos = _context.Produtos.AsNoTracking(); 

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrWhiteSpace(produtosFiltroParams.PrecoCriterio))
        {

            if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.InvariantCultureIgnoreCase))
            {
                produtos = produtos.Where(
                    produto => produto.Preco > produtosFiltroParams.Preco.Value
                ).OrderBy(produto => produto.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.InvariantCultureIgnoreCase))
            {
                produtos = produtos.Where(
                    produto => produto.Preco < produtosFiltroParams.Preco.Value
                ).OrderBy(produto => produto.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.InvariantCultureIgnoreCase))
            {
                produtos = produtos.Where(
                    produto => produto.Preco == produtosFiltroParams.Preco.Value
                ).OrderBy(produto => produto.Preco);
            }
        }

        if (!string.IsNullOrWhiteSpace(produtosFiltroParams.Nome))
        {
            produtos = produtos.Where(
                produto => produto.Nome.Contains(produtosFiltroParams.Nome.Trim(), StringComparison.InvariantCultureIgnoreCase)
            );
        }

        var produtosFiltrados = await produtos.GetPaged<Produto>(
            produtosFiltroParams.PageNumber,
            produtosFiltroParams.PageSize
        );

        return produtosFiltrados;

    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int categoryId)
    {
        var produtos = await GetAll();

        var produtosCategoria = produtos
            .Where(produto => produto.CategoriaId == categoryId);

        return produtosCategoria;
    }

}


