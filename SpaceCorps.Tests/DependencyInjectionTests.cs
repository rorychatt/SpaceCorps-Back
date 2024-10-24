using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SpaceCorps.Business.Engine;

namespace SpaceCorps.Tests;

public class DependencyInjectionTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public void GameService_IsInjected()
    {
        var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();
        
        using var scope = scopeFactory?.CreateScope();
        var gameService = scope?.ServiceProvider.GetService<IGame>();

        gameService.Should().NotBeNull();
    }
}