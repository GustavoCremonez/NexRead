using Microsoft.AspNetCore.Mvc;
using NexRead.Api.Extensions;
using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Dto.Author.Request;
using NexRead.Dto.Author.Response;

namespace NexRead.Api.Controllers;

/// <summary>
///     Controlador para gerenciamento de autores
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    /// <summary>
    ///     Adiciona um novo autor
    /// </summary>
    /// <param name="authorDto">Dados do autor a ser criado</param>
    /// <returns>Dados do autor criado</returns>
    /// <response code="200">Autor criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorRequest createAuthorDto)
    {
        var response = await _authorService.CreateAuthorAsync(createAuthorDto);

        return ResultExtensions.ToCreatedActionResult(response, response.Value.Id.ToString());
    }
}
