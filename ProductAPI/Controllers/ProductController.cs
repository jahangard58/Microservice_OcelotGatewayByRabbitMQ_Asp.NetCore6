using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model;
using ProductAPI.RabbitMQ;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRabbitMQProducer _rabbitmqProducer;
        public ProductController(IProductService productService, IRabbitMQProducer rabbitmqProducer)
        {
            this._productService = productService;
            this._rabbitmqProducer = rabbitmqProducer;
        }

        [HttpGet]
        public IEnumerable<Product> ProductList()
        {
            return _productService.GetProductList();
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost]
        public Product AddProduct(Product product)
        {
            var ResultProduct = new Product();
            try
            {
                ResultProduct = _productService.AddProduct(product);

                //send the inserted product data to the queue and consumer will listening this data from queue
                _rabbitmqProducer.SendProductMessage(ResultProduct);
                _rabbitmqProducer.SendProductMessageIsCorrect("InsertTrue");
            }
            catch 
            {

                _rabbitmqProducer.SendProductMessageIsCorrect("InsertError");
            }
            
            return ResultProduct;
        }

        [HttpPut]
        public Product UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public bool DeleteProduct(int id)
        {
            return _productService.DeleteProduct(id);
        }

    }
}
