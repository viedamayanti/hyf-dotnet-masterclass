using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IMealSharing, MyMealSharing>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.MapGet("/", () => "Meal Sharing");

var mealTest = app.Services.GetService<IMealSharing>();
mealTest.AddMealSharing(new MealSharing());

//On Postman - Swagger
{
    "header": "Pizzaria",
        "image": "url.pizza/inthehouse",
        "body": "The best pizza ever",
        "location": "Frederiksberg",
        "price": 89
    }

app.MapGet("/meals", ([FromServices] IMealSharing mealSharingService) => mealSharingService.ListAllMealSharings());
app.MapPost("/meals", (MealSharing mealSharing, [FromServices] IMealSharing mealSharingService) => mealSharingService.AddMealSharing(mealSharing));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

public class MealSharing
{
    public string Header { get; set; }
    public string Image { get; set; }
    public string Body { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
}
public interface IMealSharing
{
    void AddMealSharing(MealSharing post);
    IEnumerable<MealSharing> ListAllMealSharings();
}
public class MyMealSharing : IMealSharing
{
    private List<MealSharing> _mealsharing = new List<MealSharing>();

    public IEnumerable<MealSharing> ListAllMealSharings()
    {
        return _mealsharing;
    }
    public void AddMealSharing(MealSharing post)
    {
        _mealsharing.Add(post);
    }
}


