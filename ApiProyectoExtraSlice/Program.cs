using ApiProyectoExtraSlice.Data;
using ApiProyectoExtraSlice.Helpers;
using ApiProyectoExtraSlice.Repository;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient(builder.Configuration.GetSection("KeyVault"));
});

//DEBEMOS RECUPERAR, DE FORMA EXPLICITA EL SECRETCLIENT INYECTADO
SecretClient secretClient = builder.Services.BuildServiceProvider().GetService<SecretClient>();
KeyVaultSecret keyVaultSecret = await secretClient.GetSecretAsync("connectionstring");
// Add services to the container.
string connectionString = keyVaultSecret.Value;


builder.Services.AddDbContext<RestauranteContext>(options => options.UseSqlServer(connectionString));




builder.Services.AddTransient<RepositoryRestaurante>();
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(optons =>
{
    optons.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Crud Extra Slice",
        Description = "Api de la bbdd ExtraSliceNr",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json",
        name: "Api ExtraSlice");
    options.RoutePrefix = "";
});
//if (app.Environment.IsDevelopment())
//{
   
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
