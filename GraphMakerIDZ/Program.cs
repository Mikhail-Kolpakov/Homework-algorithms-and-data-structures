Random rand = new Random();
int numVertices;
const string outputFileName = "OutputData.txt";

Console.Write("Enter number of vertices: ");
while(!int.TryParse(Console.ReadLine(), out numVertices))
    Console.Write("Wrong format. Try one more time: ");

var graphMatrix = new int[numVertices, numVertices];

for (int i = 0; i < numVertices; i++)
{
    int numEdges = rand.Next(1, Math.Max(2, (int)Math.Sqrt(numVertices) + 1));
    int edgesSet = 0;

    for (int j = 0; j < numEdges && edgesSet < numVertices - 1; j++)
    {
        int targetVertex;
        
        do
        {
            targetVertex = rand.Next(0, numVertices);
        } while (targetVertex == i || graphMatrix[i, targetVertex] != 0);

        int weight = rand.Next(1, 11);
        graphMatrix[i, targetVertex] = weight;
        graphMatrix[targetVertex, i] = weight;

        edgesSet++;
    }
}

await WriteGraphToFile(outputFileName, graphMatrix);
    
async Task WriteGraphToFile(string path, int[,] graphMatrix)
{
    await using var writer = new StreamWriter(path, false);
    await writer.WriteLineAsync(graphMatrix.GetLength(0).ToString());
    
    for (int i = 0; i < graphMatrix.GetLength(0); i++)
    {
        for (int j = 0; j < graphMatrix.GetLength(1); j++)
            await writer.WriteAsync($"{graphMatrix[i, j]} ");
        await writer.WriteAsync("\n");
    }

    await writer.WriteLineAsync("\nNeeded format for website: ");
    
    for (int i = 0; i < graphMatrix.GetLength(0); i++)
        for (int j = i + 1; j < graphMatrix.GetLength(1); j++)
            if (graphMatrix[i, j] != 0)
                await writer.WriteLineAsync($"{i + 1}-({graphMatrix[i, j]})-{j + 1}");
    
    Console.WriteLine($"\nAnswers was saved in file {path}");
}
