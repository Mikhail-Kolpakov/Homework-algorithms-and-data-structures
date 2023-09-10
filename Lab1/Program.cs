//Варіант #9
using System.Text;

int S = 9 * 5 + 50;
Random rand = new Random();
(Stack<int> initialNums, Stack<int> evenNums, Stack<int> oddNums) = (new Stack<int>(S), new Stack<int>(S), new Stack<int>(S));

Console.OutputEncoding = Encoding.UTF8;

for (int i = 0; i < S; i++) 
    initialNums.Push(rand.Next(1, 1001)); //Генеруємо випадков значення

foreach (int number in initialNums) 
{
    if (number % 2 == 0) //Перевіряємо на парність
        evenNums.Push(number);
    else
        oddNums.Push(number);
}

Console.WriteLine("Елементи парного стеку evenNums: ");
for (int i = 0; i < evenNums.Count; i++)
    Console.Write($"{evenNums.Pop()} ");
Console.WriteLine("\n\nЕлементи не парного стеку oddNums: ");
for (int i = 0; i < oddNums.Count; i++)
    Console.Write($"{oddNums.Pop()} ");