using ApiProyectoExtraSlice.Data;
using ApiProyectoExtraSlice.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectioString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryRestaurante>();
builder.Services.AddDbContext<RestauranteContext>(options => options.UseSqlServer(connectioString));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
