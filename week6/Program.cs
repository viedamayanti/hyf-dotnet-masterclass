using HackYourFuture.Week6;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

app.MapGet("/", () => "Week 6 homework");

//Users.cs
app.MapGet("/users", async (IUserRepository userRepository) =>
{
    return await userRepository.GetUsers();
});

app.MapPost("/users", async (IUserRepository userRepository, User user) =>
{
    return await userRepository.PostUser(user);
});


app.MapPut("/users", async (IUserRepository userRepository, User user) =>
{
    return await userRepository.UpdateUser(user);
});

app.MapDelete("/users/{id}", async (IUserRepository userRepository, int id) =>
{
    return await userRepository.DeleteUser(id);
});

//Product.cs
app.MapGet("/products", async (IProductRepository productRepository) =>
{
    return await productRepository.GetProducts();
});

app.MapGet("/products/{id}", async (IProductRepository productRepository, int id) =>
{
    if (id <= 0)
    {
        Results.BadRequest("Please input a number");
    }
    return await productRepository.GetProductsById(id);
});

app.MapPost("/products", async (IProductRepository productRepository, Product product) =>
{
    return await productRepository.PostProduct(product);
});

app.MapPut("/products", async (IProductRepository productRepository, Product product) =>
{
    return await productRepository.UpdateProduct(product);
});

app.MapDelete("/products/{id}", async (IProductRepository productRepository, int id) =>
{
    return await productRepository.DeleteProduct(id);
});

app.Run();



