var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Task 1 - Temperature
app.MapGet("/temperature", () => {
var myDegree = new Temperature(10m);
System.Console.WriteLine($"{myDegree.Celcius} Celsius is {myDegree.Fahrenheit} Fahrenheit");
});

//Task 2 - Exchangerate
app.MapGet("/exchange", () => {
var amount = 160;
var exchangeRate = new ExchangeRate("EUR", "DKK");
exchangeRate.Rate = 7.5m;
Console.WriteLine($"The rate of {amount} {exchangeRate.FromCurrency} is {exchangeRate.Calculate(amount)} {exchangeRate.ToCurrency}");
});

//Task 3 - Bank account
app.MapGet("/account", () =>{
var amountBalance = new Account(100);
amountBalance.Deposit(100);
Console.WriteLine($"Account balance is {amountBalance.Balance}");
amountBalance.Withdraw(20);
System.Console.WriteLine($"Account balance is {amountBalance.Balance}");
// amountBalance.Withdraw(200); //Unhandled exception. System.Exception: Your balance is not enough
});

//Task 4 - Interface
app.MapGet("/interface", () => {
var myCat = new Cat();
MakeSound(myCat);

var myCow = new Cow();
MakeSound(myCow);


var myDog = new Dog();
MakeSound(myDog);


void MakeSound(IAnimal animal){
    System.Console.WriteLine($"{animal.Name} says {animal.Sound}");
}
});


app.Run();

//Temperature
public class Temperature{
    public decimal celDegree = -273.15m;
    public decimal Celcius { get; set; }

    public Temperature(decimal celciusDegree){
        Celcius = celciusDegree;
        if(Celcius < celDegree){
       throw new Exception();
        }
    } 
    public string Fahrenheit
    {
        get => ($"{((Celcius * 9) / 5) + 32}");
    }
    }


//Exchange rate
public class ExchangeRate
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public decimal Rate { get; set; }


    public ExchangeRate(string fromCurrency, string toCurrency)
    {
        FromCurrency = fromCurrency;
        ToCurrency = toCurrency;
       
    }
    public decimal Calculate(decimal amount)
    {
              if(Rate <= 0 || amount <= 0){
            throw new Exception("Must positiv number"); 
        }
        var result = Rate * amount;
        return result;
    }
}


//Bank account
public class Account
{
   public decimal Amount{ get; set; }
    public decimal Balance { get; set;}
  

    public Account(decimal balance)
    {
        Balance = balance ;

    }
         public decimal Deposit(decimal Amount)
    {
        {
            if (Balance > 0)
            {
                Balance += Amount;
            }
            return Balance;
        }
    }

     public decimal Withdraw(decimal Amount)
     {
     {
        if (Balance <= Amount)
        {
            throw new Exception("Your balance is not enough");
        }
            return Balance -= Amount;
        }
     }
}


//Task 4 - Interface
public interface IAnimal
{
    string Name { get; }
    string Sound { get; }

}

class Cat : IAnimal
{
    public string Name { get => "Cat"; }
    public string Sound { get => "Meow meow"; }

}
class Cow : IAnimal
{
    public string Name { get => "Cow"; }
    public string Sound { get =>  "Mooo moooo"; }
}
class Dog : IAnimal
{
    public string Name { get => "Dog"; }
    public string Sound { get => "Woof woof"; }
}
