using API.Data;
using API.Dtos.VendedorDtos;
using API.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace API.Services.VendedorServices.Commands.CreateVendedorCommand
{
  public class CreateVendedorCommandHandler : IRequestHandler<CreateVendedorCommand, VendedorDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateVendedorCommand> _validator;

    public CreateVendedorCommandHandler(LagranferiaminoristaContext context, IMapper mapper, IValidator<CreateVendedorCommand> validator)
    {
      _context = context;
      _mapper = mapper;
      _validator = validator;
    }

    public async Task<VendedorDto> Handle(CreateVendedorCommand request, CancellationToken cancellationToken)
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
          var vendedorToCreate = _mapper.Map<Vendedor>(request);
          await _context.AddAsync(vendedorToCreate);
          await _context.SaveChangesAsync();

          var vendedorDto = _mapper.Map<VendedorDto>(vendedorToCreate);

          vendedorDto.StatusCode = StatusCodes.Status200OK;
          vendedorDto.IsSuccess = true;
          vendedorDto.ErrorMessage = string.Empty;

          return vendedorDto;
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
