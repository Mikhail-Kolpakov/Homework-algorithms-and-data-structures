//Варіант #9
using System.Text;

const int givenPerc = 75;
int N = 9 * 5 + 50;
int S = (int)Math.Round(N * ((double)givenPerc / 100));
Random rand = new Random();
Dictionary<int, int> numbers = new Dictionary<int, int>();

Console.OutputEncoding = Encoding.UTF8;

for (int i = 0; i < S; i++)
    numbers.Add(i, rand.Next(1, 1001)); //Генеруємо випадкові значення

Console.WriteLine("Всі елементи після операції видалення парних чисел: ");
for (int i = 0; i < numbers.Count; i++)
{
    if (numbers[i] % 2 == 0) //Якщо число парне
        numbers.Remove(i);
    else
        Console.Write($"{numbers[i]} ");
}