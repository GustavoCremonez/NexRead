using FluentAssertions;
using NSubstitute;
using NexRead.Application.AppServices;
using NexRead.Application.Services;
using NexRead.Domain.Entities;
using NexRead.Domain.Repositories;
using NexRead.Domain.ValueObjects;

namespace NexRead.Tests.AppServices;

public class UserAppServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly UserAppService _userAppService;

    public UserAppServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _authenticationService = Substitute.For<IAuthenticationService>();

        _userAppService = new UserAppService(
            _userRepository,
            _authenticationService
        );
    }

    [Fact]
    public async Task GetProfileAsync_WithExistingUser_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var email = Email.Create("john@example.com");
        var user = User.Create("John Doe", email, "hashed_password");

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(user);

        // Act
        var result = await _userAppService.GetProfileAsync(userId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be("John Doe");
        result.Value.Email.Should().Be("john@example.com");

        await _userRepository.Received(1).GetByIdAsync(
            userId,
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetProfileAsync_WithNonExistingUser_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act
        var result = await _userAppService.GetProfileAsync(userId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("User not found");

        await _userRepository.Received(1).GetByIdAsync(
            userId,
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetMyProfileAsync_WhenAuthenticated_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var email = Email.Create("john@example.com");
        var user = User.Create("John Doe", email, "hashed_password");

        _authenticationService.GetCurrentUserId()
            .Returns(userId);

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(user);

        // Act
        var result = await _userAppService.GetMyProfileAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be("John Doe");
        result.Value.Email.Should().Be("john@example.com");

        _authenticationService.Received(1).GetCurrentUserId();
        await _userRepository.Received(1).GetByIdAsync(
            userId,
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetMyProfileAsync_WhenNotAuthenticated_ShouldFail()
    {
        // Arrange
        _authenticationService.GetCurrentUserId()
            .Returns((Guid?)null);

        // Act
        var result = await _userAppService.GetMyProfileAsync();

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("User not authenticated");

        _authenticationService.Received(1).GetCurrentUserId();
        await _userRepository.DidNotReceive().GetByIdAsync(
            Arg.Any<Guid>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetMyProfileAsync_WhenAuthenticatedButUserNotFound_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _authenticationService.GetCurrentUserId()
            .Returns(userId);

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act
        var result = await _userAppService.GetMyProfileAsync();

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("User not found");

        _authenticationService.Received(1).GetCurrentUserId();
        await _userRepository.Received(1).GetByIdAsync(
            userId,
            Arg.Any<CancellationToken>()
        );
    }
}
