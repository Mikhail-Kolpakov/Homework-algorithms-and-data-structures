namespace Lab11_12.Algorithms.KruskalsAlg;

public class KruskalsAlg
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
        var edges = new List<Edge>(); // Список ребер графа

        // Заповнення списку ребер
        for (int i = 0; i < verticesCount; i++)
            for (int j = i + 1; j < verticesCount; j++)
                if (graph[i, j] != 0)
                    edges.Add(new Edge(i, j, graph[i, j]));
        
        edges.Sort((a, b) => a.Weight.CompareTo(b.Weight)); // Сортуємо ребра за зростанням ваги
        
        var parent = Enumerable.Range(0, verticesCount).ToArray(); // Ініціалізація представлення множин вершин
        var minimumSpanningTree = new List<Edge>(); // Список для збереження ребер мінімального остового дерева

        // Проходимось по всім ребрам
        foreach (var edge in edges)
        {
            int root1 = Find(parent, edge.Source);
            int root2 = Find(parent, edge.Destination);

            if (root1 != root2) // Перевірка, чи додавання ребра не створить цикл
            {
                minimumSpanningTree.Add(edge);
                Union(parent, root1, root2);
            }
        }

        await FileActions.WriteGraphToFile(OutputFilePath, minimumSpanningTree, "Крускала");
    }

    private static int Find(int[] parent, int i) // Пошук кореня дерева 
    {
        while (parent[i] != i)
            i = parent[i];

        return i;
    }

    private static void Union(int[] parent, int x, int y) // Об'єднання двох множин
    {
        int rootX = Find(parent, x);
        int rootY = Find(parent, y);
        parent[rootX] = rootY;
    }
}