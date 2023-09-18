//Варіант #9
using System.Diagnostics;
using System.Text;
using Lab6;

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
    MergeSorting.GenerateNumbers(rand, numbers, value);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Random);
    //Console.WriteLine($"\nШвидкість генерування списку з випадковими числами: {stopwatch.Elapsed.TotalMilliseconds} мс");

    //За зростанням
    (assigments, comparisons) = (0, 0);
    stopwatch.Restart();
    int left = 0;
    int right = numbers.Count - 1;
    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Ascending, ref assigments , ref comparisons);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Ascending);
    Output(assigments, comparisons, stopwatch, "за зростанням з випадковими числами");

    (assigments, comparisons) = (0, 0);
    stopwatch.Restart();
    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Ascending, ref assigments, ref comparisons);
    stopwatch.Stop();

    Output(assigments, comparisons, stopwatch, "за зростанням з числами за зростанням");

    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Descending, ref assigments, ref comparisons);
    (assigments, comparisons) = (0, 0);
    stopwatch.Restart();
    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Ascending, ref assigments, ref comparisons);
    stopwatch.Stop();

    Output(assigments, comparisons, stopwatch, "за зростанням з числами за спаданням");

    //За спаданням
    MergeSorting.Shuffle(numbers);

    (assigments, comparisons) = (0, 0);
    stopwatch.Restart();
    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Descending, ref assigments, ref comparisons);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Descending);
    Output(assigments, comparisons, stopwatch, "за спаданням з випадковими числами");

    (assigments, comparisons) = (0, 0);
    stopwatch.Restart();
    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Descending, ref assigments, ref comparisons);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Descending);
    Output(assigments, comparisons, stopwatch, "за спаданням з числами за спаданням");

    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Ascending, ref assigments, ref comparisons);
    (assigments, comparisons) = (0, 0);
    stopwatch.Restart();
    MergeSorting.MergeSort(numbers, left, right, MergeSorting.TypeOfSorting.Descending, ref assigments, ref comparisons);
    stopwatch.Stop();

    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Descending);
    Output(assigments, comparisons, stopwatch, "за спаданням з числами за зростанням");
}

void Output(int assigments, int comparisons, Stopwatch time, string typeOfTimeRecording)
{
    Console.WriteLine($"\nШвидкість сортування списку {typeOfTimeRecording}: {time.Elapsed.TotalMilliseconds} мс");
    Console.WriteLine(String.Format("Було проведено операцій присвоєння: {0:#,###} шт.", assigments));
    Console.WriteLine(String.Format("Було проведено операцій порівняння: {0:#,###} шт.", comparisons));
}

public class MergeSorting
{
    public static void GenerateNumbers(Random rand, List<int> numbers, int amountOfNumbers) //Генерування випадкових чисел
    {
        numbers.Clear();
        for (int i = 0; i < amountOfNumbers; i++)
            numbers.Add(rand.Next(-amountOfNumbers, amountOfNumbers + 1));
    }

    public static void MergeSort(List<int> list, int left, int right, TypeOfSorting typeOfSorting, ref int assigments, ref int comparisons)
    {
        int middle;

        if (left < right)
        {
            comparisons++;
            middle = (left + right) / 2;
            if (typeOfSorting == TypeOfSorting.Ascending)
            {
                MergeSort(list, left, middle, TypeOfSorting.Ascending, ref assigments, ref comparisons);
                MergeSort(list, middle + 1, right, TypeOfSorting.Ascending, ref assigments, ref comparisons);
                MergeAsc(list, left, middle, right, ref assigments, ref comparisons);
            }
            else
            {
                MergeSort(list, left, middle, TypeOfSorting.Descending, ref assigments, ref comparisons);
                MergeSort(list, middle + 1, right, TypeOfSorting.Descending, ref assigments, ref comparisons);
                MergeDesc(list, left, middle, right, ref assigments, ref comparisons);
            }
        }
    }

    public static void MergeAsc(List<int> list, int left, int middle, int right, ref int assigments, ref int comparisons) //Сортування злиттям за зростанням
    {
        int i, j;

        //Дізнаємось довжини наших майбутніх двох частин (підмасивів)
        int length1 = middle - left + 1;
        int length2 = right - middle;

        //Створюємо два підмасиви з однією запасною ячейкою
        int[] leftPart = new int[length1 + 1];
        int[] rightPart = new int[length2 + 1];

        //Заповнюємо лівий підмасив першою частиною головного масива, правий підмасив другою частиною головного масива 
        for (i = 0; i < length1; i++)
            leftPart[i] = list[left + i];

        for (j = 1; j <= length2; j++)
            rightPart[j - 1] = list[middle + j];

        //Додаємо дуже великі числа у запасні ячейки
        leftPart[length1] = int.MaxValue;
        rightPart[length2] = int.MaxValue;
        assigments += 2;

        i = 0;
        j = 0;

        for (int k = left; k <= right; k++)
        {
            if (leftPart[i] < rightPart[j]) //Порівнюємо значення з двох підмасивів, більше йде у головний масив
            {
                comparisons++;
                list[k] = leftPart[i];
                assigments++;
                i++;
            }
            else
            {
                comparisons++;
                list[k] = rightPart[j];
                assigments++;
                j++;
            }
        }
    }

    public static void MergeDesc(List<int> list, int left, int middle, int right, ref int assigments, ref int comparisons) //Сортування злиттям за спаданням
    {
        int i, j;

        int length1 = middle - left + 1;
        int length2 = right - middle;

        int[] leftPart = new int[length1 + 1];
        int[] rightPart = new int[length2 + 1];

        for (i = 0; i < length1; i++)
            leftPart[i] = list[left + i];

        for (j = 1; j <= length2; j++)
            rightPart[j - 1] = list[middle + j];

        //Додаємо дуже малі числа у запасні ячейки
        leftPart[length1] = int.MinValue;
        rightPart[length2] = int.MinValue;
        assigments += 2;

        i = 0;
        j = 0;

        for (int k = left; k <= right; k++)
        {
            if (leftPart[i] >= rightPart[j]) //Порівнюємо значення з двох підмасивів, більше йде у головний масив
            {
                comparisons++;
                list[k] = leftPart[i];
                assigments++;
                i++;
            }
            else
            {
                comparisons++;
                list[k] = rightPart[j];
                assigments++;
                j++;
            }
        }
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