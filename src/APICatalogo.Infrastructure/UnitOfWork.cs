using APICatalogo.Domain.Repositories.CategoriasRepository;
using APICatalogo.Domain.Repositories.ProdutosRepository;
using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Infrastructure.Repositories.CategoriasRepository;
using APICatalogo.Infrastructure.Repositories.ProdutosRepository;

namespace APICatalogo.Infrastructure;

public class UnitOfWork : IUnitOfWork, IDisposable {

    public AppDbContext _context;
    private IProdutoRepository _produtoRepo;
    private ICategoriaRepository _categoriaRepo;
    private bool _disposed;

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
        }
    }
    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

}
