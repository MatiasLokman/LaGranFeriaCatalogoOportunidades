using AutoMapper;
using API.Models;

using API.Dtos.CategoriaDtos;
using API.Services.CategoriaServices.Commands.CreateCategoriaCommand;
using API.Services.CategoriaServices.Commands.UpdateCategoriaCommand;
using API.Services.CategoriaServices.Commands.DeleteCategoriaCommand;

using API.Dtos.ProductoDtos;
using API.Services.ProductoServices.Commands.CreateProductoCommand;
using API.Services.ProductoServices.Commands.UpdateProductoCommand;
using API.Services.ProductoServices.Commands.DeleteProductoCommand;

using API.Dtos.EnvioDto;
using API.Services.EnvioServices.Commands.UpdateCostoEnvioCommand;

using API.Dtos.VendedorDtos;
using API.Services.VendedorServices.Commands.CreateVendedorCommand;
using API.Services.VendedorServices.Commands.UpdateVendedorCommand;
using API.Services.VendedorServices.Commands.DeleteVendedorCommand;

using API.Dtos.PedidoDtos;
using API.Services.PedidoServices.Commands.CreatePedidoCommand;
using API.Services.PedidoServices.Commands.UpdatePedidoCommand;
using API.Services.PedidoServices.Commands.DeletePedidoCommand;

namespace API.Mapper
{
  public class MapperConfig : Profile
  {
    public MapperConfig()
    {
      // Mappers para Categorías
      CreateMap<CategoriaDto, Categoria>().ReverseMap();
      CreateMap<Categoria, CreateCategoriaCommand>().ReverseMap();
      CreateMap<Categoria, UpdateCategoriaCommand>().ReverseMap();
      CreateMap<Categoria, DeleteCategoriaCommand>().ReverseMap();

      //// Mappers para Productos
      CreateMap<Producto, ProductoDto>()
          .ForMember(dest => dest.NombreCategoria, opt => opt.MapFrom(src => src.IdCategoriaNavigation.Nombre));
      CreateMap<Producto, CreateProductoCommand>().ReverseMap();
      CreateMap<Producto, UpdateProductoCommand>().ReverseMap();
      CreateMap<Producto, DeleteProductoCommand>().ReverseMap();

      // Mappers para Envios
      CreateMap<EnvioDto, Envio>().ReverseMap();
      CreateMap<Envio, UpdateCostoEnvioCommand>().ReverseMap();

      // Mappers para Vendedores
      CreateMap<VendedorDto, Vendedor>().ReverseMap();
      CreateMap<Vendedor, CreateVendedorCommand>().ReverseMap();
      CreateMap<Vendedor, UpdateVendedorCommand>().ReverseMap();
      CreateMap<Vendedor, DeleteVendedorCommand>().ReverseMap();

      // Mappers para Pedidos
      CreateMap<PedidoDto, Pedido>().ReverseMap();
      CreateMap<Pedido, CreatePedidoCommand>().ReverseMap();
      CreateMap<Pedido, UpdatePedidoCommand>().ReverseMap();
      CreateMap<Pedido, DeletePedidoCommand>().ReverseMap();
    }
  }
}
