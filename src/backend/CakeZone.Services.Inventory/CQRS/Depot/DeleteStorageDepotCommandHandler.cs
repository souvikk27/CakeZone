using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Model.Exceptions;
using CakeZone.Services.Inventory.Repository.Depot;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot;

public class DeleteStorageDepotCommandHandler : IRequestHandler<DeleteStorageDepotCommand, StorageDepot>
{
    private readonly IStorageDepotRepository _storageDepotRepository;

    public DeleteStorageDepotCommandHandler(IStorageDepotRepository storageDepotRepository)
    {
        _storageDepotRepository = storageDepotRepository;
    }

    public async Task<StorageDepot> Handle(DeleteStorageDepotCommand request, CancellationToken cancellationToken)
    {
        var storageDepot = await _storageDepotRepository.GetByIdAsync(request.Id);
        if (storageDepot == null)
        {
            throw new NotFoundApiException("Storage depot not found either validate parameters or contact support");
        }
        await _storageDepotRepository.DeleteAsync(storageDepot);
        await _storageDepotRepository.SaveChangesAsync();
        return storageDepot;
    }
}