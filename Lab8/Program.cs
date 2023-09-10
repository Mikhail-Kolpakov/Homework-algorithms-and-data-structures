//Варіант #9
using System.Diagnostics;
using System.Text;
using Lab8;

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
    QuickSorting.GenerateNumbers(rand, numbers, value);
    stopwatch.Stop();

    TimeSpan elapsedGenerating = stopwatch.Elapsed;
    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Random);
    Console.WriteLine($"\nШвидкість генерування списку з випадковими числами: {elapsedGenerating.TotalMilliseconds} мс");

    (assigments, comparisons) = (0, 0);
    stopwatch.Start();
    QuickSorting.QuickSort(numbers, 0, numbers.Count - 1, QuickSorting.TypeOfSorting.Ascending, ref assigments, ref comparisons);
    stopwatch.Stop();

    TimeSpan elapsedSortingAsc = stopwatch.Elapsed;
    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Ascending);
    Console.WriteLine($"\nШвидкість сортування списку за зростанням: {elapsedSortingAsc.TotalMilliseconds} мс");
    Console.WriteLine(String.Format("Було проведено операцій присвоєння: {0:#,###} шт.", assigments));
    Console.WriteLine(String.Format("Було проведено операцій порівняння: {0:#,###} шт.", comparisons));

    QuickSorting.Shuffle(numbers);

    (assigments, comparisons) = (0, 0);
    stopwatch.Start();
    QuickSorting.QuickSort(numbers, 0, numbers.Count - 1, QuickSorting.TypeOfSorting.Descending, ref assigments, ref comparisons);
    stopwatch.Stop();

    TimeSpan elapsedSortingDesc = stopwatch.Elapsed;
    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Descending);
    Console.WriteLine($"\nШвидкість сортування списку за спаданням: {elapsedSortingDesc.TotalMilliseconds} мс");
    Console.WriteLine(String.Format("Було проведено операцій присвоєння: {0:#,###} шт.", assigments));
    Console.WriteLine(String.Format("Було проведено операцій порівняння: {0:#,###} шт.", comparisons));
}

public class QuickSorting
{
    public static void GenerateNumbers(Random rand, List<int> numbers, int amountOfNumbers) //Генерування випадкових чисел
    {
        numbers.Clear();
        for (int i = 0; i < amountOfNumbers; i++)
            numbers.Add(rand.Next(-amountOfNumbers, amountOfNumbers + 1));
    }

    public static void QuickSort(List<int> numbers, int minIndex, int maxIndex, TypeOfSorting typeOfSorting, ref int assigments, ref int comparisons)
    {
        if (minIndex >= maxIndex) //Поки наш підмасив не буде містити в собі одного елемента
            return;

        int pivot;
        if (typeOfSorting == TypeOfSorting.Ascending)
        {
            pivot = FindPivotAsc(numbers, minIndex, maxIndex, ref assigments, ref comparisons);
            QuickSort(numbers, minIndex, pivot - 1, TypeOfSorting.Ascending, ref assigments, ref comparisons); //Для підмасиву до опорного елемента
            QuickSort(numbers, pivot + 1, maxIndex, TypeOfSorting.Ascending, ref assigments, ref comparisons); //Для підмасиву після опорного елемента
        }  
        else
        {
            pivot = FindPivotDesc(numbers, minIndex, maxIndex, ref assigments, ref comparisons);
            QuickSort(numbers, minIndex, pivot - 1, TypeOfSorting.Descending, ref assigments, ref comparisons);
            QuickSort(numbers, pivot + 1, maxIndex, TypeOfSorting.Descending, ref assigments, ref comparisons);
        }
    }

    public static int FindPivotAsc(List<int> numbers, int minIndex, int maxIndex, ref int assigments, ref int comparisons) //Швидке сортування за зростанням
    {
        int pivot = minIndex - 1; //Ймовірний індекс опорного числа
        int temp = 0;

        for (int i = minIndex; i < maxIndex; i++)
        {
            if (numbers[i] < numbers[maxIndex]) //Якщо поточне значення менше останнього елементу списку (опорного числа)
            {
                comparisons++;
                pivot++;
                //Міняємо місцями поточне значення та значення з ймовірним індексом
                temp = numbers[pivot];
                numbers[pivot] = numbers[i];
                assigments++;
                numbers[i] = temp;
                assigments++;
            }
        }

        pivot++;
        //Міняємо місцями елемент списку з ймовірним індексом з останнім елементом списку (опорним значенням)
        temp = numbers[pivot];
        numbers[pivot] = numbers[maxIndex];
        assigments++;
        numbers[maxIndex] = temp;
        assigments++;

        return pivot;
    }

    public static int FindPivotDesc(List<int> numbers, int minIndex, int maxIndex, ref int assigments, ref int comparisons) //Швидке сортування за спаданням
    {
        int pivot = minIndex - 1;
        int temp = 0;

        for (int i = minIndex; i < maxIndex; i++)
        {
            if (numbers[i] > numbers[maxIndex]) //Якщо поточне значення більше останнього елементу списку (опорного числа)
            {
                comparisons++;
                pivot++;
                temp = numbers[pivot];
                numbers[pivot] = numbers[i];
                assigments++;
                numbers[i] = temp;
                assigments++;
            }
        }

        pivot++;
        temp = numbers[pivot];
        numbers[pivot] = numbers[maxIndex];
        assigments++;
        numbers[maxIndex] = temp;
        assigments++;

        return pivot;
    }

    public static void Shuffle<T>(List<T> list) //Метод Fisher-Yates (також відомий як Knuth Shuffle) для перемішування списку
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

    public enum TypeOfSorting
    {
        Ascending,
        Descending
    }
}