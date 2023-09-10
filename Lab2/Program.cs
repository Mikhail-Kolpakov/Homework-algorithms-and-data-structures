//Варіант #9
using System.Text;

int S = 9 * 5 + 50;
Random rand = new Random();
Queue<int> initialNums = new Queue<int>(S);

Console.OutputEncoding = Encoding.UTF8;

for (int i = 0; i < S; i++)
    initialNums.Enqueue(rand.Next(1, 1001)); //Генеруємо випадкові значення

Console.WriteLine("Прості числа з черги: ");
for (int i = 0; i < initialNums.Count; i++)
{
    int number = initialNums.Dequeue();
    bool isPrime = true;

    //Робимо перевірку чи просте число
    if (number <= 1)
        isPrime = false;
    else
    {
        for (int j = 2; j * j <= number; j++)
        {
            if (number % j == 0)
            {
                isPrime = false;
                break;
            }
        }
    }

    if (isPrime)
        Console.Write($"{number} ");
}