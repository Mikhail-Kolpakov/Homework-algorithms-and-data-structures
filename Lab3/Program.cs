//Варіант #9
using System.Text;

const int givenPerc = 75;
const int N = 9 * 5 + 50;
const int S = (int)(N * ((double)givenPerc / 100));
Random rand = new Random();
Dictionary<int, int> numbers = new Dictionary<int, int>(S);

Console.OutputEncoding = Encoding.UTF8;

//Додаємо значення до хеш-таблиці
for (int i = 0; i < N; i++)
{
    int random_number;
    do
    {
        random_number = rand.Next(-100, 101);
    }
    while (numbers.ContainsKey(random_number));
    numbers.Add(random_number, random_number);
}

//Перебираємо хеш-таблицю
Console.WriteLine("Кінцеві непарні значення з хеш-таблиці: ");
foreach (KeyValuePair<int, int> number in numbers)
{
    if (number.Value % 2 == 0)
        numbers.Remove(number.Key); //Видаляємо парні значення
    else
        Console.Write($"{number.Value} "); //Виводимо кінцеві непарні значення
}

Console.WriteLine();