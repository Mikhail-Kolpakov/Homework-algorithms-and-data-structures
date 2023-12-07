namespace Lab11_12.Algorithms.KruskalsAlg;

// Клас, що характеризує прдедставлення ребра графа
public class Edge
{
    public int Source { get; set; } // Вершина, з якої виходить ребро
    public int Destination { get; set; } // Вершина, в яку входить ребро
    public int Weight { get; set; } // Вага ребра

    public Edge(int source, int destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
}