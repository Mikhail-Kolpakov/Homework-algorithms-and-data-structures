using System.Diagnostics;
using System.Text;
using Lab11_12.Algorithms;
using Lab11_12.Algorithms.KruskalsAlg;

const string outputFilePath = "OutputData.txt";
int selectedFile; // Індекс обраного користувачем файлу
string[] inputFilesPaths = new string[]
{
    "InputData5.txt",
    "InputData10.txt",
    "InputData25.txt",
    "InputData50.txt",
    "InputData100.txt",
    "InputData250.txt",
    "InputData500.txt",
    "InputData1000.txt"
}; // Масив шляхів до файлів з різними наборами вхідних даних
Stopwatch stopwatch = new Stopwatch();

await using (new StreamWriter(outputFilePath, false)) {} // Очищаємо вихідний файл при запуску програми

// Запитуємо у користувача, файл з якими вхідними даними він хоче використати
Console.OutputEncoding = Encoding.UTF8;
Console.Write("Оберіть граф, зі скількома ребрами потрібно обробити?(1:5, 2:10, 3:25, 4:50, 5:100, 6:250, 7:500, 8:1000): ");
while (!int.TryParse(Console.ReadLine(), out selectedFile) || selectedFile <= 0 || selectedFile > 8) // Перевірка на коректність вводу
    Console.Write("Введене не коректне значення. Спробуйте ще раз: ");
Console.WriteLine();

// Заміряємо алгоритм Прима
stopwatch.Start();
await PrimsAlg.FindMinimumSpanningTree(inputFilesPaths[selectedFile - 1]);
stopwatch.Stop();

OutputInfo("Прима", stopwatch);
Console.WriteLine();

// Заміряємо алгоритм Крускала
stopwatch.Restart();
await KruskalsAlg.FindMinimumSpanningTree(inputFilesPaths[selectedFile - 1]);
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