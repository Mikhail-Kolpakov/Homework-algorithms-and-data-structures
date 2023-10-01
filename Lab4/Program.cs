//Варіант #9
using System.Text;

BinaryTree tree = new BinaryTree(); //Об'єкт класу BinaryTree для представлення бінарного дерева
Node root = new Node(); //Кореневий вузол дерева
string[] phone_numbers = new string[5] { //Місце зберігання телефонних номерів
    "+1487053765",
    "+8788771563",
    "+9785106071",
    "+8835374842",
    "+3325527854"
};
int[] numbers_sum = new int[5]; //Місце зберігання сум цифр телефонних номерів

Console.OutputEncoding = Encoding.UTF8;

FindSumOfNumbers(phone_numbers); //Знаходимо суму цифр номера телефона

//Додаємо дані до бінарного дерева
foreach (int number in numbers_sum)
    tree.Insert(number);

Console.WriteLine("\nКінцеве бінарне дерево: ");
tree.DisplayTree(); //Виводимо до консолі бінарне дерево
Console.WriteLine();

void FindSumOfNumbers(string[] phone_numbers) //Метод для знаходження суми цифр номера телефона
{
    int i = 0;
    foreach (string phone_number in phone_numbers)
    {
        string temp = phone_number.TrimStart('+');
        foreach (char char_number in temp)
        {
            int int_number = Convert.ToInt32(char_number.ToString());
            numbers_sum[i] += int_number;
        }
        Console.WriteLine($"Сума цифр для номера телефона {phone_number}: {numbers_sum[i]}");
        i++;
    }
}

//Клас, що характеризує вузол бінарного дерева
public class Node
{
    public int Data { get; set; } //Значення вузла
    public Node? Left { get; set; } //Лівий вузол нащадок
    public Node? Right { get; set; } //Правий вузол нащадок

    public Node() {}

    public Node(int data) =>
        Data = data;
}

//Клас, що характеризує бінарне дерево
public class BinaryTree
{
    private Node? _root; //Кореневий вузол бінарного дерева

    public BinaryTree() =>
        _root = null; //Ініціалізуємо кореневий вузол як порожній 

    public void Insert(int data) //Метод для додавання нового елемента до бінарного дерева
    {
        if (_root == null) //Якщо дерево порожнє, то створюємо новий кореневий вузол
        {
            _root = new Node(data);
            return;
        }

        InsertRec(_root, new Node(data)); //В іншому випадку рекурсивно додаємо елемент до дерева
    }

    private void InsertRec(Node root, Node newNode) //Рекурсивний метод для додавання елементу до бінарного дерева
    {
        if (root == null) //Перевіряємо чи поточний вузол є порожнім (достигли кінця дерева)
            root = newNode; //Робимо поточний вузол новим вузлом

        if (newNode.Data < root.Data) //Перевіряємо чи значення нового вузла менше значення поточного вузла
        {
            if (root.Left == null) //Перевіряємо чи лівий нащадок вузла є порожнім
                root.Left = newNode;
            else
                InsertRec(root.Left, newNode); //Інакше рекурсивно переходимо до лівого вузла

        }
        else
        {
            if (root.Right == null) //Перевіряємо чи правий нащадок вузла є порожнім
                root.Right = newNode;
            else
                InsertRec(root.Right, newNode); //Інакше рекурсивно переходимо до правого вузла
        }
    }

    private void DisplayTree(Node? root) //Приватний метод для виводу дерева на екран (прямий обхід)
    {
        if (root == null)
            return;

        DisplayTree(root.Left);
        Console.Write($"{root.Data} ");
        DisplayTree(root.Right);
    }

    public void DisplayTree() => //Публічний метод для виведення дерева на екран
        DisplayTree(_root);
}