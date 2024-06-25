using AutoMapper;
using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Repository.StockIssue;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues
{
    public class CreateStockIssueCommandHandler : IRequestHandler<CreateStockIssueCommand, Model.StockIssue>
    {
        private readonly IStockIssuesRepository _stockIssuesRepository;
        private readonly IMapper _mapper;

        public CreateStockIssueCommandHandler(IStockIssuesRepository stockIssuesRepository, IMapper mapper)
        {
            _stockIssuesRepository = stockIssuesRepository;
            _mapper = mapper;
        }

        public async Task<StockIssue> Handle(CreateStockIssueCommand request, CancellationToken cancellationToken)
        {
            var stockIssues =
                await _stockIssuesRepository.AddAsync(_mapper.Map<StockIssue>(request.CreateStockReceiptDto));
            return stockIssues;
        }
    }
}
