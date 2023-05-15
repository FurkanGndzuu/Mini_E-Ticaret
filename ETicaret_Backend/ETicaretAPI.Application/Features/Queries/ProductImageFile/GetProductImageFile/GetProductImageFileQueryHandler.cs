using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFile
{
    public class GetProductImageFileQueryHandler : IRequestHandler<GetProductImageFileQueryRequest, List<GetProductImageFileQueryResponse>>
    {

        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImageFileQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImageFileQueryResponse>> Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.productId));
            return  product?.ProductImageFiles.Select(p => new GetProductImageFileQueryResponse
            {
                Path = $"{_configuration["StorageLink:Azure"]}/{p.path}",
                FileName = p.fileName,
                showCase = p.showCase,
                id = p.Id.ToString()
                
            }).ToList();
        }
    }
}
