using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.API.Mapper;
using EnterpriseWarehouse.API.Services;
using EnterpriseWarehouse.Domain.Context;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<WarehouseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.UseNetTopologySuite()));
builder.Services.AddScoped<DataProvider>();
builder.Services.AddScoped<IEntityRepository<Organization>, OrganizationRepository>();
builder.Services.AddScoped<IEntityRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IEntityRepository<Cell>, CellRepository>();
builder.Services.AddScoped<IEntityRepository<Supply>, SupplyRepository>();

builder.Services.AddScoped<IEntityService<OrganizationDTO, OrganizationCreateDTO>, OrganizationService>();
builder.Services.AddScoped<IEntityService<ProductDTO, ProductCreateDTO>, ProductService>();
builder.Services.AddScoped<IEntityService<CellDTO, CellCreateDTO>, CellService>();
builder.Services.AddScoped<IEntityService<SupplyDTO, SupplyCreateDTO>, SupplyService>();
builder.Services.AddScoped<IQueryService, QueryService>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => { policy.AllowAnyOrigin(); policy.AllowAnyMethod(); policy.AllowAnyHeader(); }));

builder.Services.AddControllers();

var app = builder.Build();
app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseCors();
app.MapControllers();
app.Run();
