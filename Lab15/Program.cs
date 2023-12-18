// Варіант #9
using System.Text;
using Lab15;

const int populationSize = 1000;
const int chromosomeLength = 2;
const int numGenerations = 45;
const int numParents = 350;
const int numOffsprings = 650;
const float mutationRate = 0.15F;

Console.OutputEncoding = Encoding.UTF8;
GeneticAlgorithm.Algorithmize(populationSize, chromosomeLength, numGenerations, numParents, numOffsprings, mutationRate);