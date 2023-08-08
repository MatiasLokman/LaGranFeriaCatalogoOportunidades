using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using FluentValidation;

using API.Services.CategoriaServices.Commands.CreateCategoriaCommand;
using API.Services.CategoriaServices.Commands.UpdateCategoriaCommand;
using API.Services.CategoriaServices.Commands.DeleteCategoriaCommand;

using API.Services.ProductoServices.Queries.GetProductosByCategoryQuery;
using API.Services.ProductoServices.Commands.CreateProductoCommand;
using API.Services.ProductoServices.Commands.UpdateProductoCommand;
using API.Services.ProductoServices.Commands.DeleteProductoCommand;

using API.Services.UsuarioServices.Commands.LoginUsuarioCommand;

using API.Services.EnvioServices.Commands.UpdateCostoEnvioCommand;

using API.Services.VendedorServices.Commands.CreateVendedorCommand;
using API.Services.VendedorServices.Commands.UpdateVendedorCommand;
using API.Services.VendedorServices.Commands.DeleteVendedorCommand;

using API.Services.PedidoServices.Commands.CreatePedidoCommand;
using API.Services.PedidoServices.Commands.UpdatePedidoCommand;
using API.Services.PedidoServices.Commands.DeletePedidoCommand;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
// Add services to the container.
builder.Services.AddDbContext<LagranferiaminoristaContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var _authkey = builder.Configuration.GetValue<string>("JwtSettings:securitykey");
builder.Services.AddAuthentication(item =>
{
  item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
  item.RequireHttpsMetadata = true;
  item.SaveToken = true;
  item.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
    ValidateIssuer = false,
    ValidateAudience = false,
    ClockSkew = TimeSpan.Zero
  };
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(opt =>
{
  opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

// Validaciones para CRUD (Categor�as)
builder.Services.AddScoped<IValidator<CreateCategoriaCommand>, CreateCategoriaCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateCategoriaCommand>, UpdateCategoriaCommandValidator>();
builder.Services.AddScoped<IValidator<DeleteCategoriaCommand>, DeleteCategoriaCommandValidator>();

// Validaciones para CRUD (Productos)
builder.Services.AddScoped<IValidator<CreateProductoCommand>, CreateProductoCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateProductoCommand>, UpdateProductoCommandValidator>();
builder.Services.AddScoped<IValidator<DeleteProductoCommand>, DeleteProductoCommandValidator>();
builder.Services.AddScoped<IValidator<GetProductosByCategoryQuery>, GetProductosByCategoryQueryValidator>();

// Validacion para Login (Usuarios)
builder.Services.AddScoped<IValidator<LoginUsuarioCommand>, LoginUsuarioCommandValidator>();

// Validacion para Update de envio (Envio)
builder.Services.AddScoped<IValidator<UpdateCostoEnvioCommand>, UpdateCostoEnvioCommandValidator>();

// Validaciones para CRUD (Vendedores)
builder.Services.AddScoped<IValidator<CreateVendedorCommand>, CreateVendedorCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateVendedorCommand>, UpdateVendedorCommandValidator>();
builder.Services.AddScoped<IValidator<DeleteVendedorCommand>, DeleteVendedorCommandValidator>();

// Validaciones para CRUD (Pedidos)
builder.Services.AddScoped<IValidator<CreatePedidoCommand>, CreatePedidoCommandValidator>();
builder.Services.AddScoped<IValidator<UpdatePedidoCommand>, UpdatePedidoCommandValidator>();
builder.Services.AddScoped<IValidator<DeletePedidoCommand>, DeletePedidoCommandValidator>();

builder.Services.AddControllers();

var _jwtsettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSetings>(_jwtsettings);

var app = builder.Build();

app.UseCors(c =>
{
  c.AllowAnyHeader();
  c.AllowAnyMethod();
  c.WithOrigins("https://lagranferiaoportunidades.vercel.app");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
