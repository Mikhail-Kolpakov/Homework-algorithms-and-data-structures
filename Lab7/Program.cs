//Варіант #9
using System.Diagnostics;
using System.Text;
using Lab7;

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
    HeapSorting.GenerateNumbers(rand, numbers, value);
    stopwatch.Stop();

    TimeSpan elapsedGenerating = stopwatch.Elapsed;
    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Random);
    Console.WriteLine($"\nШвидкість генерування списку з випадковими числами: {elapsedGenerating.TotalMilliseconds} мс");

    (assigments, comparisons) = (0, 0);
    stopwatch.Start();
    HeapSorting.SortHeap(numbers, HeapSorting.TypeOfSorting.Ascending, ref assigments, ref comparisons);
    stopwatch.Stop();

    TimeSpan elapsedSortingAsc = stopwatch.Elapsed;
    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Ascending);
    Console.WriteLine($"\nШвидкість сортування списку за зростанням: {elapsedSortingAsc.TotalMilliseconds} мс");
    Console.WriteLine(String.Format("Було проведено операцій присвоєння: {0:#,###} шт.", assigments));
    Console.WriteLine(String.Format("Було проведено операцій порівняння: {0:#,###} шт.", comparisons));

    HeapSorting.Shuffle(numbers);

    (assigments, comparisons) = (0, 0);
    stopwatch.Start();
    HeapSorting.SortHeap(numbers, HeapSorting.TypeOfSorting.Descending, ref assigments, ref comparisons);
    stopwatch.Stop();

    TimeSpan elapsedSortingDesc = stopwatch.Elapsed;
    //DataOutput.PrintToConsoleValues(numbers, DataOutput.TypeOfOperation.Descending);
    Console.WriteLine($"\nШвидкість сортування списку за спаданням: {elapsedSortingDesc.TotalMilliseconds} мс");
    Console.WriteLine(String.Format("Було проведено операцій присвоєння: {0:#,###} шт.", assigments));
    Console.WriteLine(String.Format("Було проведено операцій порівняння: {0:#,###} шт.", comparisons));
}

public class HeapSorting
{
    public static void GenerateNumbers(Random rand, List<int> numbers, int amountOfNumbers) //Генерування випадкових чисел
    {
        numbers.Clear();
        for (int i = 0; i < amountOfNumbers; i++)
            numbers.Add(rand.Next(-amountOfNumbers, amountOfNumbers + 1));
    }

    public static void SortHeap(List<int> numbers, TypeOfSorting typeOfOperation, ref int assigments, ref int comparisons)
    {
        int length = numbers.Count;

        //Побудова максимальної купи
        //Почнемо із середини масиву, тому що елементи після середини вже вважаються купою
        for (int i = length / 2 - 1; i >= 0; i--)
        {
            if (typeOfOperation == TypeOfSorting.Ascending)
                MakeHeapAsc(numbers, length, i, ref assigments, ref comparisons);
            else
                MakeHeapDesc(numbers, length, i, ref assigments, ref comparisons);
        }

        //Вилучення елементів з купи та сортування масиву
        for (int i = length - 1; i >= 0; i--)
        {
            //Поміняємо максимальний елемент (корінь купи) з останнім елементом
            int temp = numbers[0];
            numbers[0] = numbers[i];
            assigments++;
            numbers[i] = temp;
            assigments++;

            //Викликаємо MakeHeapAsc/MakeHeapDesc для зменшеної купи(без останнього елемента)
            if (typeOfOperation == TypeOfSorting.Ascending)
                MakeHeapAsc(numbers, i, 0, ref assigments, ref comparisons);
            else
                MakeHeapDesc(numbers, i, 0, ref assigments, ref comparisons);
        }
    }

    public static void MakeHeapAsc(List<int> numbers, int length, int root, ref int assigments, ref int comparisons) //Сортування купою за зростанням
    {
        int largest = root; //Ініціалізуємо найбільший елемент як корінь
        int leftChild = 2 * root + 1; //Лівий дочірній елемент
        int rightChild = 2 * root + 2; //Правий дочірній елемент

        //Якщо лівий дочірній елемент більший за корінь
        if (leftChild < length && numbers[leftChild] > numbers[largest])
        {
            comparisons++;
            largest = leftChild;
            assigments++;
        }
        //Якщо правий дочірній елемент більший за поточний найбільший елемент
        if (rightChild < length && numbers[rightChild] > numbers[largest])
        {
            comparisons++;
            largest = rightChild;
            assigments++;
        }

        //Якщо найбільший елемент не корінь
        if (largest != root)
        {
            comparisons++;

            //Поміняємо місцями корінь та найбільший елемент
            int temp = numbers[root];
            numbers[root] = numbers[largest];
            assigments++;
            numbers[largest] = temp;
            assigments++;

            //Рекурсивно застосовуємо MakeHeapAsc до піддерева
            MakeHeapAsc(numbers, length, largest, ref assigments, ref comparisons);
        }
    }

    public static void MakeHeapDesc(List<int> numbers, int length, int root, ref int assigments, ref int comparisons) //Сортування купою за спаданням
    {
        int smallest = root; //Ініціалізуємо найменший елемент як корінь
        int leftChild = 2 * root + 1; //Лівий дочірній елемент
        int rightChild = 2 * root + 2; //Правий дочірній елемент

        //Якщо лівий дочірній елемент менше кореня
        if (leftChild < length && numbers[leftChild] < numbers[smallest])
        {
            comparisons++;
            smallest = leftChild;
            assigments++;
        }

        //Якщо правий дочірній елемент менший за поточний найменший елемент
        if (rightChild < length && numbers[rightChild] < numbers[smallest])
        {
            comparisons++;
            smallest = rightChild;
            assigments++;
        }

        //Якщо найменший елемент не корінь
        if (smallest != root)
        {
            comparisons++;

            //Поміняємо місцями корінь та найменший елемент
            int temp = numbers[root];
            numbers[root] = numbers[smallest];
            assigments++;
            numbers[smallest] = temp;
            assigments++;

            //Рекурсивно застосовуємо MakeHeapDesc до піддерева
            MakeHeapDesc(numbers, length, smallest, ref assigments, ref comparisons);
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