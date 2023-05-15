using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.ChooseImageFile
{
    public class ChooseImageFileCommandHandler : IRequestHandler<ChooseImageFileCommandRequest, ChooseImageFileCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;  
        readonly IProductWriteRepository _productWriteRepository;

        public ChooseImageFileCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<ChooseImageFileCommandResponse> Handle(ChooseImageFileCommandRequest request, CancellationToken cancellationToken)
        {

         Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.productId));

            if(product != null)
            {
              var productImageFilesTrue =  product.ProductImageFiles?.Where(x => x.showCase == true);
                if (productImageFilesTrue.Any())
                    foreach (var productImageFile in productImageFilesTrue)
                        productImageFile.showCase = false;

           ETicareAPI.Domain.Entities.ProductImageFile? image = product.ProductImageFiles?.FirstOrDefault(pI => pI.Id == Guid.Parse(request.imageId));
           
                if(image != null)
                    image.showCase = true;
               await _productWriteRepository.SaveAsync();
            }

            return new();
        }
    }
}
