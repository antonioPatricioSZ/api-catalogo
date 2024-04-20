using APICatalogo.Application.UseCases.Category.Create;
using APICatalogo.Application.UseCases.Category.Delete;
using APICatalogo.Application.UseCases.Category.GetAll;
using APICatalogo.Application.UseCases.Category.GetById;
using APICatalogo.Application.UseCases.Category.GetProdutosFilterPreco;
using APICatalogo.Application.UseCases.Category.Update;
using APICatalogo.Communication.Requests;
using APICatalogo.Communication.Responses;
using APICatalogo.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase {


    [ProducesResponseType(typeof(IEnumerable<CategoriaResponseJson>), StatusCodes.Status200OK)]
    [HttpGet("pagination")]
    public async Task<IActionResult> GetCategorias(
        [FromQuery] CategoriasParameters categoriasParameters,
        IGetAllCategoriesUseCase useCase
    ){

        var response = await useCase.Execute(categoriasParameters);

        return Ok(response);

    }


    [ProducesResponseType(typeof(IEnumerable<CategoriaResponseJson>), StatusCodes.Status200OK)]
    [HttpGet("filter/nome/pagination")]
    public async Task<IActionResult> GetCategoriasFiltradas(
        [FromQuery] CategoriasFiltroNome categoriasFiltro,
        IGetCategoriesFilterName useCase
    ){

        var response = await useCase.Execute(categoriasFiltro);

        return Ok(response);
        
    }


    [ProducesResponseType(typeof(CategoriaResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status404NotFound)]
    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> GetById(
        int id,
        IGetCategoryByIdUseCase useCase
    ){

        var response = await useCase.Execute(id);

        return Ok(response);
       
    }


    [ProducesResponseType(typeof(CategoriaResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "UserSuperAdminOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Post(
        CategoriaRequestJson categoriaRequestDTO,
        ICreateCategoryUseCase useCase
    ){

        var response = await useCase.Execute(categoriaRequestDTO);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.CategoriaId },
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
        CategoriaRequestJson categoriaRequestDTO,
        IUpdateCategoryUseCase useCase
    ){

        await useCase.Execute(id, categoriaRequestDTO);

        return NoContent();

    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorReponseJson), StatusCodes.Status404NotFound)]
    [Authorize(Policy = "UserSuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id,
        IDeleteCategoryUseCase useCase
    ){

        await useCase.Execute(id);

        return NoContent();

    }
}
