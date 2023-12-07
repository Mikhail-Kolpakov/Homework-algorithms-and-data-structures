using Lab11_12.Algorithms.KruskalsAlg;
namespace Lab11_12.Algorithms;

public static class FileActions
{
    public static async Task<int[,]?> ReadGraphFromFile(string path) // Метод для зчитування матриці ваг з файлу
    {
        if (File.Exists(path)) // Перевіряємо, чи існує файл з вхідними даними
        {
            using var reader = new StreamReader(path);
            bool result = int.TryParse((await reader.ReadLineAsync())!, out int verticesCount); // Зчитуємо кількість вершин графа
            int[,] graph = new int[verticesCount, verticesCount]; // Зчитаний граф

            if (result) // Якщо кількість вершин графа коректно зчиталась
            {
                // Зчитуємо ваги ребер графа
                for (int i = 0; i < verticesCount; i++)
                {
                    string[] weights = (await reader.ReadLineAsync())!.Split(' ');
                    for (int j = 0; j < verticesCount; j++)
                    {
                        result = int.TryParse(weights[j], out graph[i, j]);
                        if (!result) // Якщо значення ваги зчиталось не коректно
                            return null;
                    }
                }
            
                return graph;
            }
            else
                return null;
        }
        else
            return null;
    }
    
    public static async Task WriteGraphToFile<T>(string path, T results, string algName, int[]? parent = null) // Методя для виведення кінцевих результатів до файлу
    {
        await using var writer = new StreamWriter(path, true);

        if (results is int[] intArray)
        {
            await writer.WriteLineAsync($"Остовне дерево за допомогою алгоритму {algName}: ");
            
            if (parent is null)
            {
                Console.WriteLine($"Не вистачає параметрів при типі даних {typeof(T)}");
                await writer.WriteLineAsync("|помилка|");
                return;
            }
            
            for (int i = 1; i < intArray.Length; i++)
                await writer.WriteLineAsync($"Ребро {parent[i] + 1} - {i + 1} ({intArray[i]})");

            await writer.WriteLineAsync();
        }
        else if (results is List<Edge> edgeList)
        {
            await writer.WriteLineAsync($"Остовне дерево за допомогою алгоритму {algName}: ");
            
            foreach (var edge in edgeList)
                await writer.WriteLineAsync($"Ребро {edge.Source + 1} - {edge.Destination + 1} ({edge.Weight})");
        }
        else
            Console.WriteLine($"Необроблений тип даних: {typeof(T)}");
        
        Console.WriteLine($"Результати розрахунків за алгоритмом {algName} були збережені у файл {path}");
    }
}