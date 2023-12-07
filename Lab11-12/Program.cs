// Варіант #9
using System.Diagnostics;
using System.Text;
using Lab11_12.Algorithms;
using Lab11_12.Algorithms.KruskalsAlg;

const string outputFilePath = "OutputData.txt";
Stopwatch stopwatch = new Stopwatch();

Console.OutputEncoding = Encoding.UTF8;
await using (new StreamWriter(outputFilePath, false)) {} // Очищаємо вихідний файл при запуску програми

// Зміряємо алгоритм Прима
stopwatch.Start();
await PrimsAlg.FindMinimumSpanningTree();
stopwatch.Stop();

OutputInfo("Прима", stopwatch);
Console.WriteLine();

// Заміряємо алгоритм Крускала
stopwatch.Restart();
await KruskalsAlg.FindMinimumSpanningTree();
stopwatch.Stop();

OutputInfo("Крускала", stopwatch);

void OutputInfo(string algName, Stopwatch time) // Метод для виводу інформації щодо алгоритмів
{
    Console.WriteLine($"Швидкість роботи алгоритма {algName}: {time.Elapsed.TotalMilliseconds} мс");
    switch (algName)
    {
        case "Прима":
            Console.WriteLine($"Було проведено операцій присвоєння: {PrimsAlg.Assigments}");
            Console.WriteLine($"Було проведено операцій порівняння: {PrimsAlg.Comparisons}");
            break;
        case "Крускала":
            Console.WriteLine($"Було проведено операцій присвоєння: {KruskalsAlg.Assigments}");
            Console.WriteLine($"Було проведено операцій порівняння: {KruskalsAlg.Comparisons}");
            break;
    }
}