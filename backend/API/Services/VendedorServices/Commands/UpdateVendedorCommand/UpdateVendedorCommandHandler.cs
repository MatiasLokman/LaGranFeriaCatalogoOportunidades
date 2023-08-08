using API.Data;
using API.Dtos.VendedorDtos;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace API.Services.VendedorServices.Commands.UpdateVendedorCommand
{
  public class UpdateVendedorCommandHandler : IRequestHandler<UpdateVendedorCommand, VendedorDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateVendedorCommand> _validator;

    public UpdateVendedorCommandHandler(LagranferiaminoristaContext context, IMapper mapper, IValidator<UpdateVendedorCommand> validator)
    {
      _context = context;
      _mapper = mapper;
      _validator = validator;
    }

    public async Task<VendedorDto> Handle(UpdateVendedorCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
          var VendedorVacio = new VendedorDto();

          VendedorVacio.StatusCode = StatusCodes.Status400BadRequest;
          VendedorVacio.ErrorMessage = string.Join(". ", validationResult.Errors.Select(e => e.ErrorMessage));
          VendedorVacio.IsSuccess = false;

          return VendedorVacio;
        }
        else
        {
          var VendedorToUpdate = await _context.Vendedores.FindAsync(request.IdVendedor);

          if (VendedorToUpdate == null)
          {
            var VendedorVacio = new VendedorDto();

            VendedorVacio.StatusCode = StatusCodes.Status404NotFound;
            VendedorVacio.ErrorMessage = "La categoría no existe";
            VendedorVacio.IsSuccess = false;

            return VendedorVacio;
          }
          else
          {
            VendedorToUpdate.Nombre = request.Nombre;
            VendedorToUpdate.Ocultar = request.Ocultar;

            await _context.SaveChangesAsync();

            var vendedorDto = _mapper.Map<VendedorDto>(VendedorToUpdate);

            vendedorDto.StatusCode = StatusCodes.Status200OK;
            vendedorDto.IsSuccess = true;
            vendedorDto.ErrorMessage = "";

            return vendedorDto;
          }
        }
      }
      catch (Exception ex)
      {
        var VendedorVacio = new VendedorDto();

        VendedorVacio.StatusCode = StatusCodes.Status400BadRequest;
        VendedorVacio.ErrorMessage = ex.Message;
        VendedorVacio.IsSuccess = false;

        return VendedorVacio;
      }
    }

  }
}
