using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories.CustomerRepositories;
using ETicaretAPI.Application.Repositories.OrderRepositories;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;


using ETicaretAPI.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI.Application.Abstractions.Storage;
using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Application.Features.Queries.GetAllProductsQueries;
using MediatR;
using ETicaretAPI.Application.Features.Commands.Products.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Products.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.Products.DeleteProduct;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.CreateProductImageFile;
using ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFile;
using Microsoft.AspNetCore.Authorization;
using ETicareAPI.Domain.Enums;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : Controller
    {
      
        readonly IProductReadRepository _productReadRepository;
        readonly IMediator _mediatr;

        public ProductController(IProductReadRepository productReadRepository, IMediator mediatr)
        {         
            _productReadRepository = productReadRepository;
            _mediatr = mediatr;
        }




        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetAllProductsQueryRequest request)
        {
       
           GetAllProductQueryResponse response = await _mediatr.Send(request);
            return Ok(response);
           
        }
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpGet("{id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Product By Id", Menu = nameof(MenuTypes.Products))]
        public async Task<IActionResult> Get(string Id)
        {
            Product product =await _productReadRepository.GetByIdAsync(Id);

            return Ok(product);
        }
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Product", Menu = nameof(MenuTypes.Products))]
        public async Task<IActionResult> Post(CreateProductCommandRequest  request)
        {
            await _mediatr.Send(request);

            return Ok();
        }
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Product", Menu = nameof(MenuTypes.Products))]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
        {
            await _mediatr.Send(request);

            return Ok();
        }
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Product", Menu = nameof(MenuTypes.Products))]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            await _mediatr.Send(request);

            return Ok();
        }
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpPost("[action]")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Add Product Image File", Menu = nameof(MenuTypes.Products))]
        public async Task<IActionResult> Upload([FromQuery] CreateProductImageFileCommandRequest request)
        {
            request.files = Request.Form.Files;
          CreateProductImageFileCommandResposne response =  await _mediatr.Send(request);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpGet("[action]/{productId}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Product Image Files", Menu = nameof(MenuTypes.Products))]
        public async Task<IActionResult> getProductImages([FromRoute] GetProductImageFileQueryRequest request)
        {
            List<GetProductImageFileQueryResponse> response = await _mediatr.Send(request);
            return Ok(response);

        }
    }
}
