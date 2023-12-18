namespace Lab15;

public static class GeneticAlgorithm
{
    private static readonly Random Rand = new Random();

    public static void Algorithmize(in int populationSize, in int chromosomeLength, in int numGenerations,
        in int numParents, in int numOffsprings, in float mutationRate)
    {
        var maxPopulation = CreatePopulation(populationSize, chromosomeLength);
        var minPopulation = new List<List<float>>(maxPopulation);

        for (int generation = 0; generation < numGenerations; generation++)
        {
            var bestChromosome = FindMaxFitnessChromosome(maxPopulation);
            var worstChromosome = FindMinFitnessChromosome(minPopulation);
            
            var maxParents = Selection(maxPopulation, numParents, x => FindMaxFitnessChromosome(x));
            var minParents = Selection(minPopulation, numParents, x => FindMinFitnessChromosome(x));
            
            var maxOffsprings = new List<List<float>>();
            var minOffsprings = new List<List<float>>();
            
            Console.WriteLine($"\nГенерація #{generation + 1}:\nнайкраще співпадіння: {Fitness(bestChromosome, MaxOfMin.Max)}\n" +
                              $"найгірше співпадіння: {Fitness(worstChromosome, MaxOfMin.Min)}");

            for (int i = 0; i < numOffsprings; i++)
            {
                var maxSelectedParents = new List<List<float>>
                {
                    maxParents[Rand.Next(maxParents.Count)],
                    maxParents[Rand.Next(maxParents.Count)]
                };
                
                var minSelectedParents = new List<List<float>>
                {
                    minParents[Rand.Next(minParents.Count)],
                    minParents[Rand.Next(minParents.Count)]
                };
                
                var maxOffspring = Crossover(maxSelectedParents);
                var minOffspring = Crossover(minSelectedParents);
                
                maxOffsprings.Add(maxOffspring);
                minOffsprings.Add(minOffspring);
            }

            maxOffsprings = Mutation(maxOffsprings, mutationRate);
            minOffsprings = Mutation(minOffsprings, mutationRate);
            
            maxPopulation = maxParents.Concat(maxOffsprings).ToList();
            minPopulation = minParents.Concat(minOffsprings).ToList();
        }

        var finalBestChromosome = FindMaxFitnessChromosome(maxPopulation);
        var finalWorstChromosome = FindMinFitnessChromosome(minPopulation);
        
        float finalBestFitness = Fitness(finalBestChromosome, MaxOfMin.Max);
        float finalWorstFitness = Fitness(finalWorstChromosome, MaxOfMin.Min);

        Console.WriteLine("\nОтримані результати:");
        Console.WriteLine($"\nНайкраща хромосома: {string.Join(", ", finalBestChromosome)}");
        Console.WriteLine($"Найкраще співпадіння: {finalBestFitness:f5}");
        Console.WriteLine($"\nНайгірша хромосома: {string.Join(", ", finalWorstChromosome)}");
        Console.WriteLine($"Найкраще співпадіння: {finalWorstFitness:f5}");
    }

    private static List<List<float>> CreatePopulation(in int size, in int chromosomeLength)
    {
        var population = new List<List<float>>();
        
        for (int i = 0; i < size; i++)
        {
            var chromosome = new List<float>();
            for (int j = 0; j < chromosomeLength; j++)
                chromosome.Add(Rand.NextSingle() * 6);
            population.Add(chromosome);
        }

        return population;
    }

    private static float Fitness(in List<float> chromosome, MaxOfMin type)
    {
        if (2 * chromosome[0] + chromosome[1] <= 10 && -2 * chromosome[0] + 3 * chromosome[1] <= 6 &&
            2 * chromosome[0] + 4 * chromosome[1] >= 8 && chromosome[0] >= 0 && chromosome[1] >= 0)
            return Function(chromosome);

        return type switch
        {
            MaxOfMin.Max => float.NegativeInfinity,
            MaxOfMin.Min => float.PositiveInfinity,
            _ => 0
        };
    }

    private static List<List<float>> Selection(in List<List<float>> population, in int numParents,
        Func<List<List<float>>, List<float>> func)
    {
        var selectedParents = new List<List<float>>();
        
        for (int i = 0; i < numParents; i++)
            selectedParents.Add(func(population));
        
        return selectedParents;
    }

    private static List<float> Crossover(in List<List<float>> parents)
    {
        int chromosomeLength = parents[0].Count;
        var offspring = new List<float>();
        int crossoverPoint = Rand.Next(1, chromosomeLength);
        
        offspring.AddRange(parents[0].GetRange(0, crossoverPoint));
        offspring.AddRange(parents[1].GetRange(crossoverPoint, chromosomeLength - crossoverPoint));
        
        return offspring;
    }
    
    private static List<List<float>> Mutation(List<List<float>> offsprings, in float mutationRate)
    {
        foreach (var offspring in offsprings)
        {
            for (int j = 0; j < offspring.Count; j++)
            {
                if (Rand.NextSingle() < mutationRate)
                {
                    offspring[j] += Rand.NextSingle() * 2 - 1;
                    offspring[j] = Math.Max(0, offspring[j]);
                }
            }
        }

        return offsprings;
    }
    
    private static List<float> FindMaxFitnessChromosome(in List<List<float>> population)
    {
        float maxFitness = float.NegativeInfinity;
        var maxFitnessChromosome = new List<float>();

        foreach (var chromosome in population)
        {
            float currentFitness = Fitness(chromosome, MaxOfMin.Max);
            if (currentFitness > maxFitness)
            {
                maxFitness = currentFitness;
                maxFitnessChromosome = chromosome;
            }
        }

        return maxFitnessChromosome;
    }

    private static List<float> FindMinFitnessChromosome(in List<List<float>> population)
    {
        float minFitness = float.PositiveInfinity;
        var minFitnessChromosome = new List<float>();

        foreach (var chromosome in population)
        {
            float currentFitness = Fitness(chromosome, MaxOfMin.Min);
            if (currentFitness < minFitness)
            {
                minFitness = currentFitness;
                minFitnessChromosome = chromosome;
            }
        }

        return minFitnessChromosome;
    }
    
    private static float Function(in List<float> x) =>
        (float)(Math.Pow(x[0] - 2, 2) + Math.Pow(x[1] - 2, 2));

    private enum MaxOfMin
    {
        Max,
        Min
    }
}