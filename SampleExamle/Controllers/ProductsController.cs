using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleExamle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext context;

        //public static List<Product> products = new List<Product>
        //{
        //   new Product{Id = 1,CategoryId=1,ProductName="Notebook",UnitPrice=1200,UnitsInStock=14},
     
        //};

        public ProductsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found");     
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok(await context.Products.ToListAsync());
            
        }

        [HttpPut]
        public async Task<ActionResult<List<Product>>> UpdateProduct(Product req)
        {
            var dbProduct = await context.Products.FindAsync(req.Id);
            if (dbProduct == null)
                return BadRequest("Product not uptade");
            dbProduct.ProductName = req.ProductName;
            dbProduct.CategoryId = req.CategoryId;
            dbProduct.UnitPrice = req.UnitPrice;
            dbProduct.UnitsInStock = req.UnitsInStock;
            await context.SaveChangesAsync();

            return Ok(await context.Products.ToListAsync());

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> Delete(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found");
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return Ok(await context.Products.ToListAsync());
        }
    }
}
