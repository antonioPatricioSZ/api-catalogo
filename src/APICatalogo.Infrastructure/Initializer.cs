using APICatalogo.Domain.Entities;
using APICatalogo.Domain.Repositories.CategoriasRepository;
using APICatalogo.Domain.Repositories.ProdutosRepository;
using APICatalogo.Domain.Repositories.Repository;
using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Infrastructure.Repositories.CategoriasRepository;
using APICatalogo.Infrastructure.Repositories.ProdutosRepository;
using APICatalogo.Infrastructure.Repositories.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APICatalogo.Infrastructure;

public static class Initializer {

    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    ){
        AddContext(services, configuration);
        AddIdentity(services);
        AddRepositories(services);
        AddUnitOfWork(services);
    }

    public static void AddIdentity(IServiceCollection services) {
        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    }

    
    public static void AddContext(
        IServiceCollection services,
        IConfiguration configuration
    ){

        var mySqlConnection = configuration
            .GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                mySqlConnection,
                ServerVersion.AutoDetect(mySqlConnection),
                mySqlOptions => {
                    mySqlOptions.EnableStringComparisonTranslations();
                }   
            )
        );

    }

    public static void AddUnitOfWork(IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddRepositories(IServiceCollection services) {
        services.AddScoped<ICategoriaRepository, CategoriaRepository>()
            .AddScoped<IProdutoRepository, ProdutoRepository>()
            .AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            
    }

}
