﻿using CakeZone.Services.Product.Model.Exception;
using CakeZone.Services.Product.Repository.Product;
using MediatR;
namespace CakeZone.Services.Product.CQRS.Product
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Model.Product>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Model.Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.RemoveProductTransactionalAsync(request.Id);
            return product;
        }
    }
}
