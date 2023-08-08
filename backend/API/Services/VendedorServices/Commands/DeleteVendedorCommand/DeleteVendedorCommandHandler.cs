using API.Data;
using API.Dtos.VendedorDtos;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentValidation;
using MediatR;

namespace API.Services.VendedorServices.Commands.DeleteVendedorCommand
{
  public class DeleteVendedorCommandHandler : IRequestHandler<DeleteVendedorCommand, VendedorDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<DeleteVendedorCommand> _validator;

    public DeleteVendedorCommandHandler(LagranferiaminoristaContext context, IMapper mapper, IValidator<DeleteVendedorCommand> validator)
    {
      _context = context;
      _mapper = mapper;
      _validator = validator;
    }

    public async Task<VendedorDto> Handle(DeleteVendedorCommand request, CancellationToken cancellationToken)
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
          var VendedorToDelete = await _context.Vendedores.FindAsync(request.IdVendedor);

          if (VendedorToDelete == null)
          {
            var VendedorVacio = new VendedorDto();

            VendedorVacio.StatusCode = StatusCodes.Status404NotFound;
            VendedorVacio.ErrorMessage = "La categoría no existe";
            VendedorVacio.IsSuccess = false;

            return VendedorVacio;
          }
          else
          {
            _context.Vendedores.Remove(VendedorToDelete);
            await _context.SaveChangesAsync();

            var vendedorDto = _mapper.Map<VendedorDto>(VendedorToDelete);

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
