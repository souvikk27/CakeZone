using AutoMapper;
using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Model.Exceptions;
using CakeZone.Services.Inventory.Repository.Depot;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot;

public class UpdateStorageDepotCommandHandler : IRequestHandler<UpdateStorageDepotCommand, StorageDepot>
{
    private readonly IStorageDepotRepository _storageDepotRepository;
    private readonly IMapper _mapper;

    public UpdateStorageDepotCommandHandler(IStorageDepotRepository storageDepotRepository, IMapper mapper)
    {
        _storageDepotRepository = storageDepotRepository;
        _mapper = mapper;
    }

    public async Task<StorageDepot> Handle(UpdateStorageDepotCommand request, CancellationToken cancellationToken)
    {
        var existStorageDepot = await _storageDepotRepository.GetByIdAsync(request.UpdateStorageDepotDto.Id);

        if (existStorageDepot == null)
        {
            throw new NotFoundApiException(
                "Requested storage depot is not not found, either validate parameters or contact support");
        }

        var storageDepot = _mapper.Map<StorageDepot>(request.UpdateStorageDepotDto);
        await _storageDepotRepository.UpdateAsync(storageDepot);
        await _storageDepotRepository.SaveChangesAsync();

        return storageDepot;
    }
}