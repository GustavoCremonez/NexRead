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
    /// <param name="createAuthorRequest">Dados do autor a ser criado</param>
    /// <returns>Dados do autor criado</returns>
    /// <response code="201">Autor criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="409">Já existe um autor com esse nome</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorRequest createAuthorRequest)
    {
        var response = await _authorService.CreateAuthorAsync(createAuthorRequest);

        return response.ToCreatedActionResult(response.Value.Id.ToString());
    }

    /// <summary>
    ///     Atualiza os dados de um autor existente
    /// </summary>
    /// <param name="updateAuthorRequest">Dados atualizados do autor</param>
    /// <returns>Dados do autor atualizado</returns>
    /// <response code="200">Autor atualizado com sucesso</response>
    /// <response code="400">Dados inválidos ou nome já em uso</response>
    /// <response code="404">Restaurante não encontrado</response>
    /// <response code="409">Já existe um autor com esse nome</response>
    [HttpPut]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateAuthorRequest updateAuthorRequest)
    {
        var response = await _authorService.UpdateAuthorAsync(updateAuthorRequest);

        return response.ToOkActionResult();
    }

    /// <summary>
    ///     Obtém um autor pelo Id
    /// </summary>
    /// <param name="id">Id do autor</param>
    /// <returns>Dados do autor solicitado</returns>
    /// <response code="200">Retorna os dados do autor</response>
    /// <response code="404">Autor não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAuthorAsync(Guid id)
    {
        var response = await _authorService.GetAuthorAsync(id);

        return response.ToOkActionResult();
    }

    /// <summary>
    ///     Remove um autor pelo seu Id
    /// </summary>
    /// <param name="id">Id do autor a ser removido</param>
    /// <returns>Confirmação da remoção</returns>
    /// <response code="204">Autor removido com sucesso</response>
    /// <response code="404">Autor não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _authorService.DeleteAuthorAsync(id);

        return response.ToNoContentActionResult();
    }
}
