using APICatalogo.Application.UseCases.Product.Create;
using APICatalogo.Application.UseCases.Product.Delete;
using APICatalogo.Application.UseCases.Product.GetAll;
using APICatalogo.Application.UseCases.Product.GetById;
using APICatalogo.Application.UseCases.Product.GetProdutosCategoria;
using APICatalogo.Application.UseCases.Product.GetProdutosFilterPreco;
using APICatalogo.Application.UseCases.Product.Update;
using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase {


    [ProducesResponseType(typeof(IEnumerable<ProductResponseJson>), StatusCodes.Status200OK)]
    [HttpGet("produtos/{id}")]
    public async Task<IActionResult> GetProdutosCategoria(
        int id,
        IGetProdutosCategoriaUseCase useCase
    ) {

        var response = await useCase.Execute(id);

        return Ok(response);

    }


    [ProducesResponseType(typeof(IEnumerable<ProductResponseJson>), StatusCodes.Status200OK)]
    [HttpGet("pagination")]
    public async Task<IActionResult> GetProdutos(
        [FromQuery] ProdutosParameters produtosParameters,
        IGetAllUseCase useCase
    ) {

        var produtos = await useCase.Execute(produtosParameters);

        return Ok(produtos);

    }

    [ProducesResponseType(typeof(IEnumerable<ProductResponseJson>), StatusCodes.Status200OK)]
    [HttpGet("filter/preco/pagination")]
    public async Task<IActionResult> GetProdutosFilterPreco(
        [FromQuery] ProdutosFiltroPreco produtosFiltroParameters,
        IGetProdutosFilterPrecoUseCase useCase
    ) {

        var response = await useCase.Execute(produtosFiltroParameters);

        return Ok(response);

    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        int id,
        IGetByIdUseCase useCase
    ) {

        var response = await useCase.Execute(id);

        return Ok(response);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "UserSuperAdminOrAdmin")]
    public async Task<IActionResult> Post(
        ProductRequestJson request,
        ICreateUseCase useCase
    ){

        var response = await useCase.Execute(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response
        );

    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status404NotFound)]
    [Authorize(Policy = "UserSuperAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(
        int id,
        ProductRequestJson produtoRequest,
        IUpdateProductUseCase useCase
    ){

        await useCase.Execute(id, produtoRequest);

        return NoContent();

    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    [Authorize(Policy = "UserSuperAdmin")]
    public async Task<IActionResult> Delete(
        int id,
        IDeleteProductUseCase useCase
    ){

        await useCase.Execute(id);

        return NoContent();

    }

}
