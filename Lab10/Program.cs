//Варіант #9
using System;
using System.Collections;
using System.Text;

string stringToEncode = "Добрав бобру добра добу";
string decodedString;
HuffmanTree huffmanTree = new HuffmanTree();

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine($"Початковий текст: \n{stringToEncode}");

huffmanTree.Build(stringToEncode);
BitArray encoded = huffmanTree.Encode(stringToEncode);

Console.WriteLine("\nЗакодований текст: ");
foreach (bool bit in encoded)
    Console.Write(bit ? 1 : 0);

Console.WriteLine("\n\nРозкодований текст: ");
decodedString = huffmanTree.Decode(encoded);
Console.WriteLine(decodedString);

//Визначення класу Node, який представляє вузол дерева Хаффмана
public class Node
{
    public char Symbol { get; set; } //Символ (лист дерева)
    public int Frequency { get; set; } //Частота символа
    public Node? Right { get; set; } //Правий нащадок вузла
    public Node? Left { get; set; } //Лівий нащадок вузла

    //Рекурсивна функція для обходу дерева та пошуку символа
    public List<bool>? Traverse(char symbol, List<bool> data)
    {
        //Якщо вузол - лист
        if (Right == null && Left == null)
        {
            if (symbol.Equals(this.Symbol))
                return data; //Символ знайдено, повертаємо бітову послідовність
            else
                return null; //Символ не знайдено
        }
        else
        {
            List<bool>? left = null;
            List<bool>? right = null;

            //Рекурсивний обхід лівого піддерева
            if (Left != null)
            {
                List<bool> leftPath = new List<bool>();
                leftPath.AddRange(data);
                leftPath.Add(false);

                left = Left.Traverse(symbol, leftPath);
            }

            //Рекурсивний обхід правого піддерева
            if (Right != null)
            {
                List<bool> rightPath = new List<bool>();
                rightPath.AddRange(data);
                rightPath.Add(true);
                right = Right.Traverse(symbol, rightPath);
            }

            //Якщо символ знайдено в одному з піддерев, повертаємо відповідний результат
            if (left != null)
                return left;
            else
                return right;
        }
    }
}

public class HuffmanTree
{
    private List<Node> nodes = new List<Node>(); //Список вузлів дерева
    public Node? Root { get; set; } //Корінь дерева
    public Dictionary<char, int> Frequencies = new Dictionary<char, int>(); //Частоти символів

    //Побудова дерева Хаффмана на основі вихідного рядка
    public void Build(string source)
    {
        //Підрахунок частот символів у рядку
        for (int i = 0; i < source.Length; i++)
        {
            if (!Frequencies.ContainsKey(source[i]))
                Frequencies.Add(source[i], 0);

            Frequencies[source[i]]++;
        }

        //Створення вузлів для кожного символу
        foreach (var symbol in Frequencies)
            nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });

        //Побудова дерева, об'єднуючи вузли з найменшими частотами
        while (nodes.Count > 1)
        {
            List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

            if (orderedNodes.Count >= 2)
            {
                //Вибір двох вузлів з найменшими частотами
                List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                //Створення батьківського вузла, об'єднуючи частоти дочірніх вузлів
                Node parent = new Node()
                {
                    Symbol = '*',
                    Frequency = taken[0].Frequency + taken[1].Frequency,
                    Left = taken[0],
                    Right = taken[1]
                };

                nodes.Remove(taken[0]);
                nodes.Remove(taken[1]);
                nodes.Add(parent);
            }

            this.Root = nodes.FirstOrDefault();
        }
    }

    //Кодування вихідного рядка з використанням побудованого дерева Хаффмана
    public BitArray Encode(string source)
    {
        List<bool> encodedSource = new List<bool>();

        for (int i = 0; i < source.Length; i++)
        {
            //Отримання бітової послідовності для символу
            List<bool>? encodedSymbol = this.Root?.Traverse(source[i], new List<bool>());
            if (encodedSymbol is not null)
                encodedSource.AddRange(encodedSymbol);
        }

        BitArray bits = new BitArray(encodedSource.ToArray());

        return bits;
    }

    //Метод для розкодування бітової послідовності
    public string Decode(BitArray bits)
    {
        Node? current = this.Root; //Поточний вузол починається з кореня дерева
        string decoded = String.Empty; //Розкодований рядок

        foreach (bool bit in bits)
        {
            //Якщо біт дорівнює 1
            if (bit)
            {
                if (current?.Right != null)
                    current = current.Right; //Перехід до правого нащадка
            }
            else
            {
                if (current?.Left != null)
                    current = current.Left; //Перехід до лівого нащадка
            }

            //Якщо поточний вузол - лист
            if (IsLeaf(current))
            {
                decoded += current?.Symbol; //Додавання символу до розкодованого рядка
                current = this.Root; //Повернення до кореня дерева для нового символу
            }
        }

        return decoded;
    }

    //Перевірка, чи є вузол листом (не має нащадків)
    public bool IsLeaf(Node? node) => 
        node?.Left == null && node?.Right == null;
}