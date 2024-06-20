using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Repository.Depot;
using CakeZone.Services.Inventory.Services;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot
{
    public class GetStorageDepotsQueryHandler : IRequestHandler<GetStorageDepotsQuery, IEnumerable<StorageDepot>>
    {
        private readonly IStorageDepotRepository _storageDepotRepository;

        public GetStorageDepotsQueryHandler(IStorageDepotRepository storageDepotRepository)
        {
            _storageDepotRepository = storageDepotRepository;
        }

        public async Task<IEnumerable<StorageDepot>> Handle(GetStorageDepotsQuery request, CancellationToken cancellationToken)
        {
            var storageDepots = await _storageDepotRepository.ListAllAsync();

            var filteredStorageDepots = storageDepots.Where(attribute =>
                (request.Parameter.AddedOn == DateTime.MinValue || request.Parameter.AddedOn == attribute.CreatedAt) &&
                (string.IsNullOrEmpty(request.Parameter.Name) || request.Parameter.Name == attribute.Name) &&
                (string.IsNullOrEmpty(request.Parameter.Address) || request.Parameter.Address == attribute.Address)
            ).ToList();

            var metadata = new MetaData().Initialize(request.Parameter.PageNumber,
                request.Parameter.PageSize,
                filteredStorageDepots.Count);

            return PagedList<StorageDepot>.ToPagedList(filteredStorageDepots,
                request.Parameter.PageNumber,
                request.Parameter.PageSize);
        }
    }
}