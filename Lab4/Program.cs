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
int[] numbersSum = new int[5]; //Місце зберігання сум цифр телефонних номерів

Console.OutputEncoding = Encoding.UTF8;

numbersSum = FindSumOfNumbers(phone_numbers); //Знаходимо суму цифр номера телефона

//Додаємо дані до бінарного дерева
foreach (int number in numbersSum)
    tree.Insert(number);

Console.WriteLine("\nКінцеве бінарне дерево: ");
tree.DisplayTree(); //Виводимо до консолі бінарне дерево
Console.WriteLine();

Console.WriteLine("\nНамагаємося знайти в дереві телефон +8835374842: ");
int index = Array.IndexOf(phone_numbers, "+8835374842");
Console.WriteLine($"Елемент знайден?: {(tree.Search(numbersSum[index]) ? "знайден" : "не знайден")}");
Console.WriteLine("\nНамагаємося знайти в дереві телефон +4371459234: ");
int[] numberSum = FindSumOfNumbers(new string[1] { "+4371459234" });
Console.WriteLine($"Елемент знайден?: {(tree.Search(numberSum[0]) ? "знайден" : "не знайден")}");

Console.WriteLine("\nНамагаємося видалити з дерева телефоно +8788771563: ");
index = Array.IndexOf(phone_numbers, "+8788771563");
Console.WriteLine($"Елемент знайден?: {(tree.Delete(numbersSum[index]) ? "знайден" : "не знайден")}");
Console.WriteLine($"Елемент видалено");

Console.WriteLine("\nВигляд бінарного дерева після видалення телефону +8788771563: ");
tree.DisplayTree();

Console.WriteLine();

int[] FindSumOfNumbers(string[] phone_numbers) //Метод для знаходження суми цифр номера телефона
{
    int i = 0;
    int length = phone_numbers.Length;
    int[] array = new int[length];

    foreach (string phone_number in phone_numbers)
    {
        string temp = phone_number.TrimStart('+');
        foreach (char char_number in temp)
        {
            int int_number = Convert.ToInt32(char_number.ToString());
            array[i] += int_number;
        }
        Console.WriteLine($"Сума цифр для номера телефона {phone_number}: {array[i]}");
        i++;
    }

    return array;
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

    public bool Search(int data) => //Метод для пошуку у бінарному дереві
        SearchRec(_root, data);

    private bool SearchRec(Node? root, int data) //Рекурсивний метод для пошуку елемента
    {
        //Якщо дійшли до кінця дерева і не знайшли елемент, повертаємо false
        if (root is null)
            return false;

        //Якщо знайшли елемент - повертаємо true
        if (data == root.Data)
            return true;

        //Рекурсивно йдемо вліво, якщо наш елемент менший за поточний
        if (data < root.Data)
            return SearchRec(root.Left, data);

        return SearchRec(root.Right, data); //Рекурсивно йдемо вправо, якщо наш елемент більший або дорівнює поточному
    }

    public bool Delete(int data) //Метод для видалення елемента бінарного дерева
    {
        bool isFound = Search(data); //перевіряємо, чи існує елемент у дереві
        if (!isFound)
            return false;

        _root = DeleteRec(_root, data);

        return true;
    }

    private Node? DeleteRec(Node? root, int data) //Рекурсивний метод для видалення елемента з бінарного дерева
    {
        if (root is null)
            return null;

        if (data < root.Data)
            root.Left = DeleteRec(root.Left, data); //Рекурсивно йдемо вліво
        else if (data > root.Data)
            root.Right = DeleteRec(root.Right, data); //Рекурсивно йдемо вправо
        else
        {
            if (root.Left is null) //Якщо у видаляємому вузлі немає лівого піддерева, замінюємо його правим піддеревом
                return root.Right;
            else if (root.Right is null) //Якщо у видаляємому вузлі немає правого піддерева, замінюємо його лівим піддеревом
                return root.Left;

            root.Data = MinValue(root.Right); //Шукаємо мінімальне значення в правому піддереві
            root.Right = DeleteRec(root.Right, root.Data); //Видаляємо вузол з мінімальним значенням з правого піддерева
        }

        return root;
    }

    private int MinValue(Node? node) //Допоміжний метод для пошуку мінімального значення в дереві
    {
        int minValue = node!.Data;

        while (node.Left is not null)
        {
            minValue = node.Left.Data;
            node = node.Left;
        }

        return minValue;
    }

    private void DisplayTreeRec(Node? root) //Приватний метод для виводу дерева на екран (прямий обхід)
    {
        if (root == null)
            return;

        DisplayTreeRec(root.Left);
        Console.Write($"{root.Data} ");
        DisplayTreeRec(root.Right);
    }

    public void DisplayTree() => //Публічний метод для виведення дерева на екран
        DisplayTreeRec(_root);
}