using Microsoft.EntityFrameworkCore;
using SpaceCorps.Business.Db;

const string applicationTitle = "SpaceCorps WebApi";
const string version = "v0.0.1";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApiDocument(config => {
    config.DocumentName = applicationTitle;
    config.Title = applicationTitle;
    config.Version = version;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = applicationTitle;
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {}
