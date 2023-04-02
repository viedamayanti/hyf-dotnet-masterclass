using MySql.Data.MySqlClient;
using Dapper;

namespace HackYourFuture.Week6
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductsById(int id);
        Task<Product> PostProduct(Product productPost);
        Task<Product> UpdateProduct(Product updateProduct);
        Task<int> DeleteProduct(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private string connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            using var connection = new MySqlConnection(connectionString);
            var products = await connection.QueryAsync<Product>("SELECT id, name, price, description FROM dapper.products");
            return products;
        }

        public async Task<Product> GetProductsById(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            var singleProduct = await connection.QuerySingleAsync<Product>($"SELECT id, name, price, description FROM dapper.products WHERE id = {id}");
            return singleProduct;
        }

        public async Task<Product> PostProduct(Product product)
        {
            using var connection = new MySqlConnection(connectionString);
            var productPost = await connection.ExecuteAsync(@"INSERT INTO dapper.products (name, price, description) VALUES (@name, @price, @description);", product);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            using var connection = new MySqlConnection(connectionString);
            var updateProduct = await connection.ExecuteAsync("UPDATE dapper.products SET name=@name, price=@price, description=@description WHERE id=@id", product);
            return product;
        }

        public async Task<int> DeleteProduct(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            var deleteProduct = await connection.ExecuteAsync($"DELETE FROM dapper.products WHERE id=@id", new { id });
            return deleteProduct;
        }
    }
    public record Product(int Id, string Name, decimal Price, string description);
}
