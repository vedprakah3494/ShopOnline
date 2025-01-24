using Product_MVC.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace Product_MVC.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            using var client = _httpClientFactory.CreateClient("AppClient");
            var requestContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("product", requestContent);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Product>(responseContent, JsonOptions);
        }

        public async Task DeleteProduct(Product product)
        {
            using var client = _httpClientFactory.CreateClient("AppClient");
            var response = await client.DeleteAsync($"product/{product.Id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Product?> GetProductById(int id)
        {
            using var client = _httpClientFactory.CreateClient("AppClient");
            var response = await client.GetAsync($"product/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Product>(responseContent, JsonOptions);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            using var client = _httpClientFactory.CreateClient("AppClient");
            var response = await client.GetAsync("product");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(responseContent, JsonOptions);
            return products ?? Array.Empty<Product>();
        }

        public async Task UpdateProduct(Product product)
        {
            using var client = _httpClientFactory.CreateClient("AppClient");
            var requestContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"product/{product.Id}", requestContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
