using API.SEG.Persistence;
using API.SEG.Infraestructure;
using API.SEG.Aplicacion.Features;
using SEG.WebApi.Modules.Authentication;
using SEG.WebApi.Modules.Feature;
using SEG.WebApi.Modules.Injection;
using SEG.WebApi.Modules.Mapper;
using SEG.WebApi.Modules.Swagger;
using SEG.WebApi.Modules.Validator;
using SEG.WebApi.Modules.Redis;
using SEG.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddMapper();
builder.Services.AddControllers();
builder.Services.AddFeature(configuration);
builder.Services.AddPersistenceServices();
builder.Services.AddInfraestructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddInjection(configuration);
builder.Services.AddAuthentication(configuration);
builder.Services.AddValidator();
builder.Services.AddSwagger();
builder.Services.AddRedisCache(builder.Configuration);

// Se registra el ActionFilterAttribute en el Contenedor de Dependencias
builder.Services.AddScoped<AuthorizationRequestAttribute>();
builder.Services.AddScoped<ValidateRequestAttribute>();
builder.Services.AddScoped<ValidateTokenRequestAttribute>();
builder.Services.AddScoped<UpdateTokenRequestAttribute>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Seguridad v1"));
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyHeader();
    x.AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { };