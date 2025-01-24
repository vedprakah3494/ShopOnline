using Product_MVC.Models;

namespace Product_MVC.Services
{
    public interface IProductService
    {
        Task<Product?> CreateProductAsync(Product product);
        Task DeleteProduct(Product product);
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
        Task UpdateProduct(Product product);
    }
}