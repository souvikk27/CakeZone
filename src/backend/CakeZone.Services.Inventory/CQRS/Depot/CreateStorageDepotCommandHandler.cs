using AutoMapper;
using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Repository.Depot;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot
{
    public class CreateStorageDepotCommandHandler : IRequestHandler<CreateStorageDepotCommand, StorageDepot>
    {
        private readonly IStorageDepotRepository _storageDepotRepository;
        private readonly IMapper _mapper;

        public CreateStorageDepotCommandHandler(IStorageDepotRepository storageDepotRepository, IMapper mapper)
        {
            _storageDepotRepository = storageDepotRepository;
            _mapper = mapper;
        }

        public async Task<StorageDepot> Handle(CreateStorageDepotCommand request, CancellationToken cancellationToken)
        {
            var storgeDepot = _mapper.Map<StorageDepot>(request.CreateStorageDepotDto);
            await _storageDepotRepository.AddAsync(storgeDepot);
            await _storageDepotRepository.SaveChangesAsync();
            return storgeDepot;
        }
    }
}
