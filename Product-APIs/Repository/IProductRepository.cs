using Product_APIs.Model;

namespace Product_APIs.Repository
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
        Product? GetProductById(int id);
        IEnumerable<Product> GetProducts();
        void UpdateProduct(Product product);
    }
}