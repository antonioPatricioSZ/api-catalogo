using APICatalogo.Domain.Entities;
using APICatalogo.Domain.Pagination;
using APICatalogo.Domain.Repositories.Repository;

namespace APICatalogo.Domain.Repositories.ProdutosRepository;

public interface IProdutoRepository
    : IRepository<Produto> {

    Task<PagedList<Produto>> GetProdutosPaginados(ProdutosParameters produtosParameters);

    Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);

    Task<IEnumerable<Produto>> GetProdutosPorCategoria(int categoryId);

}
