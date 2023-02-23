// 1. String manipulation
string input = "word and world";
string reversed = string.Empty;
for (int i = input.Length - 1; i >= 0; i--){
    reversed += input[i];
}
Console.WriteLine($"Reversed input value: {reversed}");

//2. String/Math
string inputVowels = "Intellectualization";
int len = inputVowels.Length;
int vowelCount = 0;
for (int i = 0; i < len; i++){
 if (inputVowels[i] == 'a'|| inputVowels[i] == 'i' || inputVowels[i] == 'u' || inputVowels[i] == 'e' || inputVowels[i] == 'o' || inputVowels[i] == 'A'|| inputVowels[i] == 'I' || inputVowels[i] == 'U' || inputVowels[i] == 'E' || inputVowels[i] == 'O')
    {
 vowelCount++;
    }
};
Console.WriteLine($"Number of vowels: {vowelCount}");

//3. Math/Array
int[] arr = new[] { 271, -3, 1, 14, -100, 13, 2, 1, -8, -59,  -1852, 41, 5 };

int positivSum = 1;
int negativSum = 0;
for (int i = 0; i < arr.Length; i++)
{
  if(arr[i] > 0){
     positivSum *= arr[i];
    } else if(arr[i] < 0){
        negativSum += arr[i];
    }
}
Console.WriteLine($"Sum of negative numbers: {negativSum}. Multiplication of positive numbers: {positivSum}");

//4. Classical task
int number1 = 0;
int number2 = 1;
int nthNumber;
int n = 6;
for (int i = 0; i < n; i++)
{
    nthNumber = number1 + number2;
    number1 = number2;
    number2 = nthNumber;
}
Console.WriteLine($"Nth fibonacci number is {number1}");
