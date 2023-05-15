using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.ChooseImageFile
{
    public class ChooseImageFileCommandRequest : IRequest<ChooseImageFileCommandResponse>
    {
        public string productId { get; set; }
        public string imageId { get; set; }
    }
}
