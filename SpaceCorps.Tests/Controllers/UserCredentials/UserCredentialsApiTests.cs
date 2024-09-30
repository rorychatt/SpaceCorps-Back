using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using SpaceCorps.Business.Dto.Authorization;

namespace SpaceCorps.Tests.Controllers.UserCredentials;

public class UserCredentialsApiTesdts(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient httpClient = factory.CreateClient();

    [Fact]
    public async Task CreateUserAsync_UserDoesNotExist_ReturnsCreated()
    {
        var request = new CreateUserRequest(Guid.NewGuid().ToString() + "@tester.com", "password");

        var response = await httpClient.PostAsJsonAsync("api/UserCredentials/create", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

    }
}