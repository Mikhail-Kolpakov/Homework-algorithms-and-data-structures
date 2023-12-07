namespace Lab11_12.Algorithms;

public static class PrimsAlg
{
    private const string InputFilePath = "InputData.txt";
    private const string OutputFilePath = "OutputData.txt";
    
    public static async Task FindMinimumSpanningTree() // Метод для виконання основних розрахунків
    {
        int[,]? graph = await FileActions.ReadGraphFromFile(InputFilePath); // Граф у вигляді матриці ваг

        if (graph is null)
        {
            Console.WriteLine("Файлу з вхідними даними не існує або не є коректний");
            return;
        }
        
        int verticesCount = graph.GetLength(0); // Кількість вершин графа
        bool[]? included = new bool[verticesCount]; // Масив для визначення, чи вже вузол включений у остовне дерево
        int[] key = new int[verticesCount]; // Вага ребра, що приєднує кожен вузол до остовного дерева
        int[] parent = new int[verticesCount]; // Масив для збереження індексів вершин, що приєднані до остовного дерева
            
        // Ініціалізація ваг та встановлення всіх ваг на "необмежено" (велике значення)
        Array.Fill(included, false);
        Array.Fill(key, int.MaxValue);
        Array.Fill(parent, -1);

        key[0] = 0; // Початковий вузол остовного дерева

        // Цикл для побудови остовного дерева
        for (int i = 0; i < verticesCount - 1; i++)
        {
            int u = MinKey(key, included, verticesCount);
            included[u] = true;
                
            // Цикл для оновлення ключів суміжних вершин, якщо знайдено меншу вагу ребра
            for (int j = 0; j < verticesCount; j++)
            {
                // Перевірка наявності ребра між поточною вершиною (u) та суміжною вершиною (j),
                // чи суміжна вершина (j) ще не включена в остове дерево та чи нова вага менша поточного ключа для вершини (j)
                if (graph[u, j] != 0 && !included[j] && graph[u, j] < key[j])
                {
                    parent[j] = u;
                    key[j] = graph[u, j]; // Оновлюємо ключ для вершини (j) на новий, менший за поточний
                }  
            }
        }
            
        await FileActions.WriteGraphToFile(OutputFilePath, key, "Прима", parent); // Виводимо результати 
    }

    public static int MinKey(int[] key, bool[] included, int verticesCounts) // Метод для знаходження вершини з мінімальним ключем серед тих, які ще не включені в основне дерево
    {
        int min = int.MaxValue;
        int minIndex = -1;
        
        // Перебираємо всі вершини графа
        for (int i = 0; i < verticesCounts; i++)
        {
            if (included[i] == false && key[i] < min) // Перевірка чи вершина ще не включена в основне дерево та її ключ менший за поточний мінімум
            {
                min = key[i];
                minIndex = i;
            }
        }

        return minIndex; // Повертаємо індекс вершини з мінімальним ключем
    }
}