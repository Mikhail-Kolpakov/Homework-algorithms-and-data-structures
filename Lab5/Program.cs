//Варіант #9
using System.Diagnostics;
using System.Text;
using Lab5;

int assigments;
int comparisons;
List<int> numbers = new List<int>();
int[] nValues = new int[5] { 10, 100, 1000, 5000, 10000 };
Random rand = new Random();
Stopwatch stopwatch = new Stopwatch();

Console.OutputEncoding = Encoding.UTF8;

foreach (int value in nValues)
{
    Console.WriteLine("\n------------------------------------------------------\n");
    Console.WriteLine($"При n = {value}: ");

    stopwatch.Start();
    InsertionSorting.GenerateNumbers(rand, numbers, value);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Random);
    //Console.WriteLine($"\nШвидкість генерування списку з випадковими числами: {stopwatch.Elapsed.TotalMilliseconds} мс");

    //За зростанням
    stopwatch.Restart();
    InsertionSorting.SortNumbersAsc(numbers, out assigments, out comparisons);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Ascending);
    Output(assigments, comparisons, stopwatch, "за зростанням з випадковими числами");

    stopwatch.Restart();
    InsertionSorting.SortNumbersAsc(numbers, out assigments, out comparisons);
    stopwatch.Stop();

    Output(assigments, comparisons, stopwatch, "за зростанням з числами за зростанням");

    InsertionSorting.SortNumbersDesc(numbers, out assigments, out comparisons);
    stopwatch.Restart();
    InsertionSorting.SortNumbersAsc(numbers, out assigments, out comparisons);
    stopwatch.Stop();

    Output(assigments, comparisons, stopwatch, "за зростанням з числами за спаданням");

    //За спаданням
    InsertionSorting.Shuffle(numbers);
    
    stopwatch.Restart();
    InsertionSorting.SortNumbersDesc(numbers, out assigments, out comparisons);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Descending);
    Output(assigments, comparisons, stopwatch, "за спаданням з випадковими числами");

    stopwatch.Restart();
    InsertionSorting.SortNumbersDesc(numbers, out assigments, out comparisons);
    stopwatch.Stop();

    Output(assigments, comparisons, stopwatch, "за спаданням з числами за спаданням");

    InsertionSorting.SortNumbersAsc(numbers, out assigments, out comparisons);
    stopwatch.Restart();
    InsertionSorting.SortNumbersDesc(numbers, out assigments, out comparisons);
    stopwatch.Stop();

    Output(assigments, comparisons, stopwatch, "за спаданням з числами за зростанням");
}

void Output(int assigments, int comparisons, Stopwatch time, string typeOfTimeRecording)
{
    Console.WriteLine($"\nШвидкість сортування списку {typeOfTimeRecording}: {time.Elapsed.TotalMilliseconds} мс");
    Console.WriteLine(String.Format("Було проведено операцій присвоєння: {0:#,###} шт.", assigments));
    if (comparisons != 0)
        Console.WriteLine(String.Format("Було проведено операцій порівняння: {0:#,###} шт.", comparisons));
    else
        Console.WriteLine($"Було проведено операцій порівняння: {comparisons} шт.");
}

public class InsertionSorting
{
    public static void GenerateNumbers(Random rand, List<int> numbers, int amountOfNumbers) //Генерування випадкових чисел
    {
        numbers.Clear();
        for (int i = 0; i < amountOfNumbers; i++)
            numbers.Add(rand.Next(-amountOfNumbers, amountOfNumbers + 1));
    }

    public static void SortNumbersAsc(List<int> numbers, out int assigments, out int comparisons) //Сортування вставками за зростанням
    {
        (assigments, comparisons) = (0, 0);
        int currentNumber;
        int indexOfCurrentNumber;
        for (int i = 0; i < numbers.Count; i++)
        {
            indexOfCurrentNumber = i;
            currentNumber = numbers[i];
            while (indexOfCurrentNumber > 0 && numbers[indexOfCurrentNumber - 1] > currentNumber) //Поки ми не перебрали весь список та попереднє число більше за поточне
            {
                comparisons++;
                numbers[indexOfCurrentNumber] = numbers[indexOfCurrentNumber - 1]; //На місце поточного числа записуємо попереднє число
                assigments++;
                indexOfCurrentNumber--;
            }
            numbers[indexOfCurrentNumber] = currentNumber; //На місце попереднього числа записуємо поточне запам'ятоване число
            assigments++;
        }
    }

    public static void SortNumbersDesc(List<int> numbers, out int assigments, out int comparisons) //Сортування вставками за спаданням
    {
        (assigments, comparisons) = (0, 0);
        int currentNumber;
        int indexOfCurrentNumber;
        for (int i = numbers.Count - 1; i >= 0; i--) //Перебір списку у зворотньому порядку
        {
            indexOfCurrentNumber = i;
            currentNumber = numbers[i];
            while (indexOfCurrentNumber < numbers.Count - 1 && numbers[indexOfCurrentNumber + 1] > currentNumber) //Поки ми не перебрали весь список та наступне число більше за попереднє
            {
                comparisons++;
                numbers[indexOfCurrentNumber] = numbers[indexOfCurrentNumber + 1]; //На місце попереднього числа записуємо наступне число
                assigments++;
                indexOfCurrentNumber++;
            }
            numbers[indexOfCurrentNumber] = currentNumber; //На місце наступного числа записуємо попереднє число
            assigments++;
        }
    }

    public static void Shuffle<T> (List<T> list) //Метод Fisher-Yates (також відомий як Knuth Shuffle) для перемішування списку
    {
        Random rand = new Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}