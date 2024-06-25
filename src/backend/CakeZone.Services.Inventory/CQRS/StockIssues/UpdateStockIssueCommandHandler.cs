using AutoMapper;
using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Model.Exceptions;
using CakeZone.Services.Inventory.Repository.StockIssue;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues
{
    public class UpdateStockIssueCommandHandler : IRequestHandler<UpdateStockIssueCommand, Model.StockIssue>
    {
        private readonly IStockIssuesRepository _stockIssuesRepository;
        private readonly IMapper _mapper;

        public UpdateStockIssueCommandHandler(IStockIssuesRepository stockIssuesRepository, IMapper mapper)
        {
            _stockIssuesRepository = stockIssuesRepository;
            _mapper = mapper;
        }

        public async Task<StockIssue> Handle(UpdateStockIssueCommand request, CancellationToken cancellationToken)
        {
            var checkExists = await _stockIssuesRepository.CheckExistsAsync(x => x.Id == request.UpdateStockIssueDto.Id);
            if (!checkExists)
            {
                throw new NotFoundApiException(
                    "Requested stock issue not found either validate parameters or contact support");
            }

            var stockIssues = _mapper.Map<StockIssue>(request.UpdateStockIssueDto);

            await _stockIssuesRepository.UpdateAsync(stockIssues);
            await _stockIssuesRepository.SaveChangesAsync();
            return stockIssues;
        }
    }
}