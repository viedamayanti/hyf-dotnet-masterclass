using MySql.Data.MySqlClient;
using Dapper;

namespace HackYourFuture.Week6
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> PostUser(User userPost);
        Task<User> UpdateUser(User updateUser);
        Task<int> DeleteUser(int id);
    }
    public class UserRepository : IUserRepository
    {
        private string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using var connection = new MySqlConnection(connectionString);

            var users = await connection.QueryAsync<User>("SELECT id, name, age FROM dapper.users");
            return users;
        }

        public async Task<User> PostUser(User user)
        {
            using var connection = new MySqlConnection(connectionString);
            var userPost = await connection.ExecuteAsync(@"INSERT INTO dapper.users ( name, age) VALUES (@name, @age);", user);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            using var connection = new MySqlConnection(connectionString);
            var updateUser = await connection.ExecuteAsync("UPDATE dapper.users SET name=@name, age=@age WHERE id=@id", user);
            return user;
        }
        public async Task<int> DeleteUser(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            var deleteUser = await connection.ExecuteAsync($"DELETE FROM dapper.users WHERE id=@id", new { id });
            return deleteUser;
        }
    }
    public record User(int id, string name, int age);
}
