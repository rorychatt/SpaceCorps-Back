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

    [Fact]
    public async Task CreateUserAsync_UserAlreadyExists_ReturnsConflict()
    {
        const string email = "iamnottobedublicated@test2.com";
        const string password = "password";

        var request = new CreateUserRequest(email, password);
        var response = await httpClient.PostAsJsonAsync("api/UserCredentials/create", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var response2 = await httpClient.PostAsJsonAsync("api/UserCredentials/create", request);

        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}