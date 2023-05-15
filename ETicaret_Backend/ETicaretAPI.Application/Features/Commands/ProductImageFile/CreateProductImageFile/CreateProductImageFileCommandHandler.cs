using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.CreateProductImageFile
{
    public class CreateProductImageFileCommandHandler : IRequestHandler<CreateProductImageFileCommandRequest, CreateProductImageFileCommandResposne>
    {
        readonly IStorageServices _storageServices;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productwriteRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public CreateProductImageFileCommandHandler(IStorageServices storageServices,
            IProductReadRepository productreadRepository,
            IProductWriteRepository productwriteRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageServices = storageServices;
            _productReadRepository = productreadRepository;
            _productwriteRepository = productwriteRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<CreateProductImageFileCommandResposne> Handle(CreateProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _storageServices.UploadAsync("product-images", request.files);

            var product = await _productReadRepository.GetByIdAsync(request.Id);




            if (product is not null)
            {
                foreach (var i in data)
                {
                    await _productImageFileWriteRepository.AddAsync(new ETicareAPI.Domain.Entities.ProductImageFile()
                    {
                        Id = Guid.NewGuid(),
                        fileName = i.fileName,
                        path = i.pathOrContainerName,
                        Products = new List<Product>() { product }
                    });
                }
            }


            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
