namespace Lab5
{
    internal class DataOutput
    {
        public static void PrintToConsoleValues(List<int> numbers, TypeOfOperation typeOfOperation)
        {
            switch (typeOfOperation)
            {
                case TypeOfOperation.Random:
                    Console.WriteLine("\n\nВипадкова послідовність списку: ");
                    break;
                case TypeOfOperation.Ascending:
                    Console.WriteLine("\n\nПослідовність списку за зростанням: ");
                    break;
                case TypeOfOperation.Descending:
                    Console.WriteLine("\n\nПослідовність списку за спаданням: ");
                    break;
            }
            foreach (int number in numbers)
                Console.Write($"{number} ");
            Console.WriteLine();
        }

        public enum TypeOfOperation
        {
            Random,
            Ascending,
            Descending
        }
    }
}
