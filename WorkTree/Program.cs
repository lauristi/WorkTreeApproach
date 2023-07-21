using AutoMapper;
using WorkTree.AutoMapper;
using WorkTree.Business.Interface;
using WorkTree.Business;
using WorkTree.Repositories.Interface;
using WorkTree.Repositories;
using WorkTree.Database.Dapper.Interface;
using WorkTree.Database.Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Adicionando o serviço do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); // Resolve conflito de nomes de endpoits no swagger
});

//--------------------------------------------------------------------------------------------------
//Auto Mappper
//--------------------------------------------------------------------------------------------------

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(c =>
{
    c.AddProfile(new MappingProfiles());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//--------------------------------------------------------------------------------------------------
//Injeção de para o connectionString provider
//--------------------------------------------------------------------------------------------------

builder.Services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();

//--------------------------------------------------------------------------------------------------
//Injeção de dependencias
//--------------------------------------------------------------------------------------------------

builder.Services.AddScoped<IBaseItemRepository, BaseItemRepository>();
builder.Services.AddScoped<IBaseItemBLL, BaseItemBLL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();