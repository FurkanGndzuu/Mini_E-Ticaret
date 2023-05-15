using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.CreateProductImageFile
{
    public class CreateProductImageFileCommandRequest : IRequest<CreateProductImageFileCommandResposne>
    {
        public string Id { get; set; }
        public IFormFileCollection? files { get; set; }
    }
}
