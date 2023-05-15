using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {

        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
           

            await _productWriteRepository.AddAsync(new()
            {
                Name = request.name,
                Stock = request.stock,
                Price = request.price
            });

      

        

           
            await _productWriteRepository.SaveAsync();
           await _productHubService.ProductAddedMessage($"{request.name} , added.");

            return new();
        }
    }
}
