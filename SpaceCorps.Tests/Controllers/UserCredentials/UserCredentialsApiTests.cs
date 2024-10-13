using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
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

    [Fact]
    public async Task VerifyPassword_ShouldReturn_NotFound()
    {
        const string email = "iamdefinetlynotindb@test3.com";
        const string password = "password";

        var request = new VerifyPasswordRequest(email, password);

        var response = await httpClient.PostAsJsonAsync("api/UserCredentials/verify", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task VerifyPassword_ShouldReturn_Unauthorized()
    {
        const string email = "iamunauthorized@test4.com";
        const string password = "password";
        const string wrongPassword = "wrongpassword";

        var createUserRequest = new CreateUserRequest(email, password);
        var seedUser = await httpClient.PostAsJsonAsync("api/UserCredentials/create", createUserRequest);
        seedUser.StatusCode.Should().Be(HttpStatusCode.Created);

        var request = new VerifyPasswordRequest(email, wrongPassword);
        var verifyPasswordResponse = await httpClient.PostAsJsonAsync("api/UserCredentials/verify", request);

        verifyPasswordResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task VerifyPassword_ShouldReturn_Ok()
    {
        const string email = "iamauthorized@test5.com";
        const string password = "password";

        var createUserRequest = new CreateUserRequest(email, password);
        var seedUser = await httpClient.PostAsJsonAsync("api/UserCredentials/create", createUserRequest);
        seedUser.StatusCode.Should().Be(HttpStatusCode.Created);

        var request = new VerifyPasswordRequest(email, password);
        var verifyPasswordResponse = await httpClient.PostAsJsonAsync("api/UserCredentials/verify", request);
        verifyPasswordResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task VerifyPassword_ShouldReturn_Email()
    {
        const string email = "anotheremailteSt@test4.com";
        const string password = "password";

        var createUserRequest = new CreateUserRequest(email, password);

        var seedUser = await httpClient.PostAsJsonAsync("api/UserCredentials/create", createUserRequest);
        seedUser.StatusCode.Should().Be(HttpStatusCode.Created);

        var request = new VerifyPasswordRequest(email, password);
        var verifyPasswordResponse = await httpClient.PostAsJsonAsync("api/UserCredentials/verify", request);

        var response = await verifyPasswordResponse.Content.ReadFromJsonAsync<VerifyPasswordResponse>();

        response!.Email.Should().Be(email);
    }

    [Fact]
    public async Task GetUser_ShouldReturn_NotFound()
    {
        var response = await httpClient.GetAsync("api/UserCredentials/0");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetUser_ShouldReturn_Ok()
    {
        const string email = "iamok@test7.com";
        const string password = "password";

        var createUserRequest = new CreateUserRequest(email, password);

        var seedUser = await httpClient.PostAsJsonAsync("api/UserCredentials/create", createUserRequest);

        seedUser.StatusCode.Should().Be(HttpStatusCode.Created);

        var response = await httpClient.GetAsync("api/UserCredentials/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}