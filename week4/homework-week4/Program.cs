var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Creating users
app.MapPost("/users/add", async (User user) =>
{
    var httpClient = new HttpClient();
    var response = await httpClient.PostAsJsonAsync("https://dummyjson.com/users/add", user);

    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest("Something went wrong");
    }
    var createdPost = response.Content.ReadFromJsonAsync<CreatedPost>();
    return Results.Ok(createdPost);
});

//Creating products
app.MapPost("/products/add", async (Product products) =>
{
    var httpClient = new HttpClient();
    var response = await httpClient.PostAsJsonAsync("https://dummyjson.com/products/add", products);

    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest("Something went wrong");
    }
    var createdProduct = response.Content.ReadFromJsonAsync<CreatedProduct>();
    return Results.Ok(createdProduct);
});

//Retrives user 
app.MapPost("/users", async (UserRequest req) =>
{
    return await GetUsersAsync(req.UserIds);
});

async Task<User> GetUserAsync(int id)
{
    var client = new HttpClient();
    var response = await client.GetAsync($"https://dummyjson.com/users/{id}");
    var result = await response.Content.ReadFromJsonAsync<User>();
    return result;
}
async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> UserIds)
{
    var getUserTasks = new List<Task<User>>();
    {
        foreach (int id in UserIds)
        {
            var getUserTask = GetUserAsync(id);
            getUserTasks.Add(getUserTask);
            System.Console.WriteLine($"This is the user {id}");
        }

    }
    return await Task.WhenAll(getUserTasks);
}

//Retrives product
app.MapPost("/products", async (ProductRequest req) =>
{
    return await GetProductsAsync(req.ProductIds);
});

async Task<Product> GetProductAsync(int productId)
{
    var client = new HttpClient();
    var response = await client.GetAsync($"https://dummyjson.com/products/{productId}");
    var result = await response.Content.ReadFromJsonAsync<Product>();
    return result;
}
async Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<int> ProductIds)
{
    var getProductTasks = new List<Task<Product>>();
    {
        foreach (int productId in ProductIds)
        {
            var getProductTask = GetProductAsync(productId);
            getProductTasks.Add(getProductTask);
            System.Console.WriteLine($"This is the product id {productId}");
        }
    }
    return await Task.WhenAll(getProductTasks);
}

//Get the user ids
app.MapGet("/users", async (int id) =>
{
    return await GetUserAsync(id);
});

//Get product ids
app.MapGet("/products", async (int productId) =>
{
    return await GetProductAsync(productId);
});

//Update the user ids 
app.MapPut("/users", async (int id) =>
{
    return await GetUserAsync(id);
});

//Update the product ids
app.MapPut("/products", async (int productId) =>
{
    return await GetProductAsync(productId);
});

//Delete the user ids
app.MapDelete("/users", async (int id) =>
{
    return await GetUserAsync(id);
});

//Delete the product ids
app.MapDelete("/products", async (int productId) =>
{
    return await GetProductAsync(productId);
});

app.Run();

record User(int Id, string FirstName, string LastName, int Age);
record Product(string Title, decimal Price);

record UserRequest(List<int> UserIds);
record ProductRequest(List<int> ProductIds);

record CreatedPost(int Id, string FirstName, string LastName, int Age);
record CreatedProduct(int id, string Title, decimal Price);



