using APICatalogo.Domain.Repositories.ProdutosRepository;
using APICatalogo.Domain.Repositories.CategoriasRepository;

namespace APICatalogo.Domain.Repositories.UnitOfWork;

public interface IUnitOfWork {

    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    Task Commit();

}
