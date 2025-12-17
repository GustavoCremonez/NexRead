using FluentAssertions;
using NSubstitute;
using NexRead.Application.AppServices;
using NexRead.Dto.Auth.Request;
using NexRead.Application.Services;
using NexRead.Domain.Entities;
using NexRead.Domain.Repositories;
using NexRead.Domain.ValueObjects;

namespace NexRead.Tests.AppServices;

public class AuthAppServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationService _authenticationService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly AuthAppService _authAppService;

    public AuthAppServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _authenticationService = Substitute.For<IAuthenticationService>();
        _refreshTokenService = Substitute.For<IRefreshTokenService>();

        _authAppService = new AuthAppService(
            _userRepository,
            _passwordHasher,
            _authenticationService,
            _refreshTokenService
        );
    }

    [Fact]
    public async Task RegisterAsync_WithValidData_ShouldSucceed()
    {
        // Arrange
        var request = new RegisterRequest(
            Name: "John Doe",
            Email: "john@example.com",
            Password: "Password123!"
        );

        _userRepository.ExistsByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _passwordHasher.HashPassword(Arg.Any<string>())
            .Returns("hashed_password");

        // Act
        var result = await _authAppService.RegisterAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be("John Doe");
        result.Value.Email.Should().Be("john@example.com");

        await _userRepository.Received(1).AddAsync(
            Arg.Is<User>(u => u.Name == "John Doe" && u.Email == "john@example.com"),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task RegisterAsync_WithExistingEmail_ShouldFail()
    {
        // Arrange
        var request = new RegisterRequest(
            Name: "John Doe",
            Email: "john@example.com",
            Password: "Password123!"
        );

        _userRepository.ExistsByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _authAppService.RegisterAsync(request);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Email already registered");

        await _userRepository.DidNotReceive().AddAsync(
            Arg.Any<User>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task RegisterAsync_WithInvalidEmail_ShouldFail()
    {
        // Arrange
        var request = new RegisterRequest(
            Name: "John Doe",
            Email: "invalid-email",
            Password: "Password123!"
        );

        // Act
        var act = async () => await _authAppService.RegisterAsync(request);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ShouldSucceed()
    {
        // Arrange
        var email = Email.Create("john@example.com");
        var user = User.Create("John Doe", email, "hashed_password");

        var request = new LoginRequest(
            Email: "john@example.com",
            Password: "Password123!"
        );

        _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(true);

        _refreshTokenService.GenerateRefreshToken()
            .Returns("refresh_token_123");

        // Act
        var result = await _authAppService.LoginAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be("John Doe");
        result.Value.Email.Should().Be("john@example.com");
        result.Value.RefreshToken.Should().Be("refresh_token_123");

        await _authenticationService.Received(1).SignInAsync(
            user.Id,
            user.Name,
            user.Email,
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task LoginAsync_WithInvalidEmail_ShouldFail()
    {
        // Arrange
        var request = new LoginRequest(
            Email: "john@example.com",
            Password: "Password123!"
        );

        _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act
        var result = await _authAppService.LoginAsync(request);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Invalid credentials");

        await _authenticationService.DidNotReceive().SignInAsync(
            Arg.Any<Guid>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task LoginAsync_WithInvalidPassword_ShouldFail()
    {
        // Arrange
        var email = Email.Create("john@example.com");
        var user = User.Create("John Doe", email, "hashed_password");

        var request = new LoginRequest(
            Email: "john@example.com",
            Password: "WrongPassword123!"
        );

        _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(false);

        // Act
        var result = await _authAppService.LoginAsync(request);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Invalid credentials");

        await _authenticationService.DidNotReceive().SignInAsync(
            Arg.Any<Guid>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task LogoutAsync_WithValidRefreshToken_ShouldSucceed()
    {
        // Arrange
        var refreshToken = "valid_refresh_token";

        _refreshTokenService.RevokeRefreshTokenAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _authAppService.LogoutAsync(refreshToken);

        // Assert
        result.IsSuccess.Should().BeTrue();

        await _authenticationService.Received(1).SignOutAsync(Arg.Any<CancellationToken>());
        await _refreshTokenService.Received(1).RevokeRefreshTokenAsync(
            refreshToken,
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task LogoutAsync_WithoutRefreshToken_ShouldSucceed()
    {
        // Arrange & Act
        var result = await _authAppService.LogoutAsync(null);

        // Assert
        result.IsSuccess.Should().BeTrue();

        await _authenticationService.Received(1).SignOutAsync(Arg.Any<CancellationToken>());
        await _refreshTokenService.DidNotReceive().RevokeRefreshTokenAsync(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>()
        );
    }
}
