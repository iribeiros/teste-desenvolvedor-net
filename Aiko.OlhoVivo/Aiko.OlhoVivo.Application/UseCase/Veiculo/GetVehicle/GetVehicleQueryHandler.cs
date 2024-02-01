﻿using Aiko.OlhoVivo.Application.Models;
using Aiko.OlhoVivo.Domain.Interfaces.Repository;
using Aiko.OlhoVivo.Infrastructure.Useful;
using AutoMapper;
using MediatR;

namespace Aiko.OlhoVivo.Application.UseCase.Veiculo.GetVehicle;

/// <summary>
/// Handler responsável por listar uma ou mais veículos castrados e ativos.
/// </summary>
public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, Result<IEnumerable<VehicleModel>>>
{
    private readonly IMapper _mapper;
    private readonly IVehicleRepository _vehicleRepository;

    public GetVehicleQueryHandler(IMapper mapper, IVehicleRepository vehicleRepository)
    {
        _mapper = mapper;
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Result<IEnumerable<VehicleModel>>> Handle(GetVehicleQuery query, CancellationToken cancellationToken)
    {
        var vehicle = _mapper.Map<IEnumerable<VehicleModel>>
            (await _vehicleRepository.ListAsync(query.Id));

        return new()
        {
            Retorno = vehicle,
            Sucesso = vehicle.Any()
        };
    }
}