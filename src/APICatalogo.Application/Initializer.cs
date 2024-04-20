using System.Reflection;
using APICatalogo.Application.Services.Token;
using APICatalogo.Application.UseCases.Product.Create;
using APICatalogo.Application.UseCases.Product.GetAll;
using APICatalogo.Application.UseCases.Product.GetById;
using APICatalogo.Application.UseCases.User.Login;
using APICatalogo.Application.UseCases.User.Register;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using APICatalogo.Application.UseCases.Product.GetProdutosCategoria;
using APICatalogo.Application.UseCases.Product.GetProdutosFilterPreco;
using APICatalogo.Application.UseCases.Product.Update;
using APICatalogo.Application.UseCases.Product.Delete;
using APICatalogo.Application.UseCases.Category.Create;
using APICatalogo.Application.UseCases.Category.GetAll;
using APICatalogo.Application.UseCases.Category.GetById;
using APICatalogo.Application.UseCases.Category.GetProdutosFilterPreco;
using APICatalogo.Application.UseCases.Category.Delete;
using APICatalogo.Application.UseCases.Category.Update;
using APICatalogo.Application.UseCases.Auth.CreateRole;
using APICatalogo.Application.UseCases.Auth.AddUserToRole;

namespace APICatalogo.Application;

public static class Initializer {

    public static void AddApplication(
        this IServiceCollection services
    ){
        AddUseCases(services);
    }

    public static void AddUseCases(IServiceCollection services) {

        services.AddScoped<ITokenService, TokenService>()
            .AddScoped<ILoginUseCase, LoginUseCase>()
            .AddScoped<IRegisterUseCase, RegisterUseCase>()
            .AddScoped<ICreateUseCase, CreateUseCase>()
            .AddScoped<IGetByIdUseCase, GetByIdUseCase>()
            .AddScoped<IGetAllUseCase, GetAllUseCase>()
            .AddScoped<IGetProdutosFilterPrecoUseCase, GetProdutosFilterPrecoUseCase>()
            .AddScoped<IGetProdutosCategoriaUseCase, GetProdutosCategoriaUseCase>()
            .AddScoped<IUpdateProductUseCase, UpdateProductUseCase>()
            .AddScoped<IDeleteProductUseCase, DeleteProductUseCase>()
            .AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>()
            .AddScoped<IGetCategoryByIdUseCase, GetCategoryByIdUseCase>()
            .AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>()
            .AddScoped<IGetCategoriesFilterName, GetCategoriesFilterName>()
            .AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>()
            .AddScoped<ICreateRoleUseCase, CreateRoleUseCase>()          
            .AddScoped<IAddUserToRoleUseCase, AddUserToRoleUseCase>()          
            .AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();          

        services.AddValidatorsFromAssembly(Assembly.Load("APICatalogo.Application"));

    }

}
