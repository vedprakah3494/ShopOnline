using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product_APIs.DTOs;
using Product_APIs.Model;
using Product_APIs.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository productRepository, IMapper mapper, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            _logger.LogInformation("Getting all products");
            var products = _productRepository.GetProducts();
            var result = _mapper.Map<IEnumerable<ProductDTO>>(products);
            _logger.LogInformation("Returning all products");
            return Ok(result);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GeProductById(int id)
        {
            _logger.LogInformation($"Getting product by id {id}");
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ProductDTO>(product);
            _logger.LogInformation($"Returning product by id {id}");
            return Ok(result);
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult<ProductDTO> Post([FromBody] ProductDTO product)
        {
            _logger.LogInformation("Creating new product");
            var productEntity = _mapper.Map<Product>(product);
            _productRepository.CreateProduct(productEntity);
            var productToReturn = _mapper.Map<ProductDTO>(productEntity);
            _logger.LogInformation("New product created");
            return CreatedAtAction(nameof(GeProductById), new { id = productToReturn.Id }, productToReturn);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductDTO product)
        {
            _logger.LogInformation($"Updating product by id {id}");
            var productToUpdate = _productRepository.GetProductById(id);
            if (productToUpdate == null)
            {
                return;
            }
            _mapper.Map(product, productToUpdate);
            _productRepository.UpdateProduct(productToUpdate);
            _logger.LogInformation($"Product updated by id {id}");
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"Deleting product by id {id}");
            var productToDelete = _productRepository.GetProductById(id);
            if (productToDelete == null)
            {
                return;
            }
            _productRepository.DeleteProduct(productToDelete);
            _logger.LogInformation($"Product deleted by id {id}");
        }
    }
}
