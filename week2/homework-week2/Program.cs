var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


// 1. Calculator
app.MapGet("/calculate", (string number1, string number2, string operation) => {
    var number1Parse = int.TryParse(number1, out int value1);
    var number2Parse = int.TryParse(number2, out int value2);
  
    if(!number1Parse || !number2Parse){
        return Results.Ok(new { Error = $"Unable to parse input '{number1}', '{number2}'!" });  
    }
    var result = 0;
    switch(operation){
        case "add":
            result = value1 + value2;
            break;
         case "substract":
            result = value1 - value2;
            break;
             case "multiply":
            result = value1 * value2;
            break;
        default:
          return Results.Ok(new { Error = $"System not supported - please try again"});  
    }
    return Results.Ok(new String($"the result is {result}"));
});

// 2. Method example
app.MapGet("/input", (string input) =>
{
     var inputParse = int.TryParse(input, out int value);
     if(inputParse){
            System.Console.WriteLine($"The sum is  {AddNumbers(value)}");
        } else{
            System.Console.WriteLine($"The total uppercase is {CountCapitalLetters(input)}");
        }
    
    int AddNumbers(int value){
        var sum = 0;
        for (int i = value; i > 0; i = i / 10){
           sum = sum + i % 10; 
        }return sum;
    }

    int CountCapitalLetters(string input){
        var count = 0;
        for (int i = 0; i < input.Length; i++){
            char ch = input[i];
            if(ch >= 'A' && ch <= 'Z')
                count++;
        }return count;
    }
});

// 3. Distinct alphabetical list 
app.MapGet("/str", (string str) =>
{
    char[] characters = str.ToLower().ToCharArray();

  for (int i = 0; i < characters.Length; i++)
    {
        int word = 0;
        for (int j = 0; j < characters.Length; j++)
        {
            if (characters[i] == characters[j] && i != j)
            {
                word = 1;
            }
        }
        if (word == 0){
            word = characters[i];
        }
            
    } return characters.Distinct().OrderBy(c => c).ToList();
});

// 4.  Word Frequency Count 
app.MapGet("/object", (string input) =>
{
    var myDict = new Dictionary<string, int>();
    string[] sentences = input.Split(" ");
    foreach(string i in sentences){
        string word = i.ToLower();
        if(myDict.ContainsKey(word)){
            myDict[word]++;
        } else {
            myDict.Add(word, 1);
        }
    }
    return myDict;
});

app.Run();
