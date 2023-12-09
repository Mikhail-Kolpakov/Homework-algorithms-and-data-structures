namespace Lab14;

public static class FileActions
{
    public static async Task<int[,]?> ReadMatrixFromFile(string path) // Метод для зчитування матриц з файлу
    {
        if (File.Exists(path)) // Перевіряємо, чи існує файл з вхідними даними
        {
            using var reader = new StreamReader(path);

            string[] dimensions = (await reader.ReadLineAsync())!.Split(' ');
            bool resultRows = int.TryParse(dimensions[0], out int rows); // Зчитуємо кількість рядків матриці
            bool resultCol = int.TryParse(dimensions[1], out int columns); // Зчитуємо кількість стовпчиків матриці
            
            if (!resultRows || !resultCol)
                return null;

            int[,] matrix = new int[rows, columns]; // Матриця для занесення зчитаних даних
                
            // Зчитуємо ваги ребер графа
            for (int i = 0; i < rows; i++)
            {
                string[] weights = (await reader.ReadLineAsync())!.Split(' ');
                for (int j = 0; j < columns; j++)
                    if (!int.TryParse(weights[j], out matrix[i, j])) // Якщо значення ваги зчиталось не коректно
                        return null;
            }
            
            return matrix;
                
        }
        else
            return null;
    }
}