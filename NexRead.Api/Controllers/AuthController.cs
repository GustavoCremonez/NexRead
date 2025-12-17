using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexRead.Api.Extensions;
using NexRead.Application.AppServices;
using NexRead.Application.DTOs.Auth.Requests;
using NexRead.Application.DTOs.Auth.Responses;

namespace NexRead.Api.Controllers;

/// <summary>
/// Endpoints de autenticação
/// </summary>
[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthAppService _authAppService;

    public AuthController(IAuthAppService authAppService)
    {
        _authAppService = authAppService;
    }

    /// <summary>
    /// Registra um novo usuário no sistema
    /// </summary>
    /// <param name="request">Dados do novo usuário</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados do usuário criado</returns>
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos ou email já cadastrado</response>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/auth/register
    ///     {
    ///        "name": "João Silva",
    ///        "email": "joao@exemplo.com",
    ///        "password": "Senha@123"
    ///     }
    ///
    /// Requisitos da senha:
    /// - Mínimo 8 caracteres, máximo 40
    /// - Pelo menos 1 letra maiúscula
    /// - Pelo menos 1 letra minúscula
    /// - Pelo menos 1 número
    /// - Pelo menos 1 caractere especial
    /// </remarks>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authAppService.RegisterAsync(request, cancellationToken);
        return result.ToActionResult();
    }

    /// <summary>
    /// Autentica um usuário no sistema
    /// </summary>
    /// <param name="request">Credenciais de login</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados do usuário autenticado e refresh token</returns>
    /// <response code="200">Login realizado com sucesso</response>
    /// <response code="400">Credenciais inválidas</response>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/auth/login
    ///     {
    ///        "email": "joao@exemplo.com",
    ///        "password": "Senha@123"
    ///     }
    ///
    /// Após o login bem-sucedido:
    /// - Um cookie de autenticação é criado (HttpOnly, Secure, SameSite=Strict)
    /// - Um refresh token é retornado na resposta
    /// - Ambos têm validade de 7 dias
    /// </remarks>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authAppService.LoginAsync(request, cancellationToken);
        return result.ToActionResult();
    }

    /// <summary>
    /// Renova a sessão do usuário usando um refresh token
    /// </summary>
    /// <param name="request">Refresh token atual</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Novo refresh token</returns>
    /// <response code="200">Token renovado com sucesso</response>
    /// <response code="400">Refresh token inválido ou expirado</response>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/auth/refresh-token
    ///     {
    ///        "refreshToken": "token-base64-anterior"
    ///     }
    ///
    /// Este endpoint:
    /// - Valida o refresh token fornecido
    /// - Revoga o token anterior
    /// - Gera um novo refresh token
    /// - Renova o cookie de autenticação
    /// </remarks>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await _authAppService.RefreshTokenAsync(request, cancellationToken);
        return result.ToActionResult();
    }

    /// <summary>
    /// Encerra a sessão do usuário autenticado
    /// </summary>
    /// <param name="request">Refresh token opcional para revogar</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>No Content</returns>
    /// <response code="204">Logout realizado com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/auth/logout
    ///     {
    ///        "refreshToken": "token-base64" // opcional
    ///     }
    ///
    /// Este endpoint:
    /// - Remove o cookie de autenticação
    /// - Revoga todos os refresh tokens do usuário
    /// - Se um refresh token específico for fornecido, também o revoga
    /// </remarks>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest? request, CancellationToken cancellationToken)
    {
        var result = await _authAppService.LogoutAsync(request?.RefreshToken, cancellationToken);
        return result.ToNoContentActionResult();
    }
}

/// <summary>
/// Request para logout
/// </summary>
/// <param name="RefreshToken">Refresh token opcional para revogar</param>
public record LogoutRequest(string? RefreshToken);
