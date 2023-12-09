// Варіант #9
using System.Text;
using Lab14;

const string inputFilePath = "InputData.txt";
var matrix = await FileActions.ReadMatrixFromFile(inputFilePath);

Console.OutputEncoding = Encoding.UTF8;

if (matrix is null)
{
    Console.WriteLine("Файлу з вхідними даними не існує або не є коректний");
    return;
}

Console.WriteLine("Початкова задана матриця: ");
PrintMatrix(matrix);

var result = FindMaxPrize(matrix);

Console.WriteLine("\nОтримана матриця сум: ");
PrintMatrix(result.SumMatrix);

Console.WriteLine("\nКінцевий шлях: ");
foreach (var value in result.Path)
    Console.WriteLine($"↓({value.Item1 + 1}, {value.Item2 + 1})");

Console.WriteLine($"\nПриз: {result.MaxPrize}");

return;

void PrintMatrix(in int[,] matrix) // Метод для друку матриці
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
            Console.Write($"{matrix[i, j],3} ");
        Console.WriteLine();
    }
}

(int MaxPrize, (int, int)[] Path, int[,] SumMatrix) FindMaxPrize(int[,] matrix) { // Метод для знаходження максимального призу
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    
    int[,] sumMatrix = new int[rows, cols]; // Створення матриці для збереження проміжних результатів
    
    sumMatrix[rows - 1, 0] = matrix[rows - 1, 0]; // Ініціалізація першого елементу, що знаходиться у нижньому лівому куті
    
    for (int i = rows - 2; i >= 0; i--) // Заповнення останнього стовпчика
        sumMatrix[i, 0] = sumMatrix[i + 1, 0] + matrix[i, 0];
    
    for (int j = 1; j < cols; j++) // Заповнення першого рядка
        sumMatrix[rows - 1, j] = sumMatrix[rows - 1, j - 1] + matrix[rows - 1, j];

    // Заповнення решти матриці
    for (int i = rows - 2; i >= 0; i--)
        for (int j = 1; j < cols; j++)
            sumMatrix[i, j] = matrix[i, j] + Math.Max(sumMatrix[i + 1, j], Math.Max(sumMatrix[i, j - 1], sumMatrix[i + 1, j - 1]));

    var path = FindPath(sumMatrix);

    return (sumMatrix[0, cols - 1], path, sumMatrix);
}

(int, int)[] FindPath(in int[,] sumMatrix) { // Метод для знаходження шляху гравця
    int rows = sumMatrix.GetLength(0);
    int cols = sumMatrix.GetLength(1);

    var path = new List<(int, int)>();
    int i = 0, j = cols - 1;

    while (i < rows - 1 || j > 0)
    {
        path.Add((i, j));
        
        if (i == rows - 1)
            j--;
        else if (j == 0)
            i++;
        else
        {
            if (sumMatrix[i + 1, j] > sumMatrix[i, j - 1] && sumMatrix[i + 1, j] > sumMatrix[i + 1, j - 1])
                i++;
            else if (sumMatrix[i, j - 1] > sumMatrix[i + 1, j] && sumMatrix[i, j - 1] > sumMatrix[i + 1, j - 1])
                j--;
            else
            {
                i++;
                j--;
            }
        }
    }

    path.Add((rows - 1, 0));

    return path.ToArray();
}