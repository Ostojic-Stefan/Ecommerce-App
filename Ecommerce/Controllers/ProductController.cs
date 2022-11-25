using AutoMapper;
using Ecommerce.Data;
using Ecommerce.DTOs.Product;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ProductController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            return Ok(await context.Products.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<ProductDTO>>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product != null)
                return Ok(mapper.Map<ProductDTO>(product));

            return NotFound("Product with the given id does not exist");
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO productDto)
        {
            var product = mapper.Map<Product>(productDto);

            context.Products.Add(product);

            await context.SaveChangesAsync();

            return Ok(mapper.Map<ProductDTO>(product));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> EditProduct(int id, ProductDTO productDto)
        {
            var product = await context.Products.FirstOrDefaultAsync(prod => prod.Id == id);

            if (product == null)
                return NotFound();

            mapper.Map(productDto, product);

            await context.SaveChangesAsync();

            return Ok(mapper.Map<ProductDTO>(product));
        }
    }
}
