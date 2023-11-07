using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Model;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContextClass;
        public ProductService(DbContextClass dbContextClass)
        {
            this._dbContextClass = dbContextClass;     
        }

        public IEnumerable<Product> GetProductList()
        {
            return _dbContextClass.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            Product? productResult = new Product();
            var product=_dbContextClass.Products.AsNoTracking().FirstOrDefault(x => x.ProductID == id);
            if (product != null)
            {
                productResult = product;
            }
            return productResult;
        }
        public  Product AddProduct(Product product)
        {
            var result = _dbContextClass.Products.Add(product);
            _dbContextClass.SaveChanges();
            return result.Entity;
        }

        public Product UpdateProduct(Product product)
        {
            var result = _dbContextClass.Products.Update(product);
            _dbContextClass.SaveChanges();
            return result.Entity;
        }

        public bool DeleteProduct(int id)
        {
            var product = _dbContextClass.Products.FirstOrDefault(d => d.ProductID == id);
            if (product == null) { return false; }
            var result = _dbContextClass.Products.Remove(product);
            _dbContextClass.SaveChanges();
            return result != null ? true : false;
        }

       

    }
}
