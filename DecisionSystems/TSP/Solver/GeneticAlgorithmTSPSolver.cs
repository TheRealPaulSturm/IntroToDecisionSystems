using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DecisionSystems.TSP.Solver
{
    public class GeneticAlgorithmTSPSolver : ITSPSolver
    {
        private readonly Random random = new Random();
        private readonly int populationSize;
        private readonly int generationCount;

        public GeneticAlgorithmTSPSolver(int populationSize, int generationCount)
        {
            this.populationSize = populationSize;
            this.generationCount = generationCount;
        }

        public List<int> Solve(IReadOnlyList<Location> cities)
        {
            Individual overallFittestIndividual = default;

            var population = new Individual[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                var tour = Enumerable.Range(1, cities.Count).Shuffle().ToArray();
                var fitness = Utils.GetDistance(tour, cities);
                var individual = new Individual(tour, fitness);

                if (overallFittestIndividual == default || individual.Fitness < overallFittestIndividual.Fitness)
                {
                    overallFittestIndividual = individual;
                }

                population[i] = individual;
            }
            for (int i = 0; i < generationCount; i++)
            {
                var nextGeneration = new Individual[populationSize];
                for (int j = 0; j < populationSize; j++)
                {
                    var parent1 = TournamentSelect(population, tournamentSize: 3);
                    var parent2 = TournamentSelect(population, tournamentSize: 3);

                    var childTour = OrderCrossover(parent1.Tour, parent2.Tour);

                    TwoOptChange(childTour, mutationProbapility: .05);

                    var child = new Individual(childTour, Utils.GetDistance(childTour, cities));

                    if (overallFittestIndividual == default || child.Fitness < overallFittestIndividual.Fitness)
                    {
                        overallFittestIndividual = child;
                    }

                    nextGeneration[j] = child;
                }
                population = nextGeneration;
            }
            return overallFittestIndividual.Tour.ToList();
        }


        private Individual TournamentSelect(Individual[] population, int tournamentSize)
        {
            //List<Individual> gladiatorIndecies = new List<Individual>();

            //while (gladiatorIndecies.Count < tournamentSize)
            //{
            //    int randomIndex = random.Next(populationSize);
            //    if (!gladiatorIndecies.Contains(population[randomIndex]))
            //    {
            //        gladiatorIndecies.Add(population[randomIndex]); 
            //    }
            //}
            //return gladiatorIndecies.BestBy(v => v.Fitness, (v1, v2) => v1 < v2);

            return population.Shuffle().Take(tournamentSize).BestBy(v => v.Fitness, (v1, v2) => v1 < v2);
        }
        private int[] OrderCrossover(int[] tour1, int[] tour2)
        {
            Debug.Assert(tour1.Length == tour2.Length, "tour1 and tour2 must have the same length!");
           
            //Generate a random range from tour1 (random position, random length):
            var startIndex = random.Next(tour1.Length);
            var endIndex = random.Next(tour1.Length);
            if (startIndex > endIndex)
            {
                (startIndex, endIndex) = (endIndex, startIndex);
            }

            //Write range at same positio to child tour
            int[] childtour = new int[tour1.Length];
            var parent1SubTour = tour1[startIndex..endIndex];
            parent1SubTour.CopyTo(childtour, startIndex);

            //Write remaining cities from tour2 to child tour
            var parent2RemainingTour = tour2.Except(parent1SubTour).ToArray();
            var parent2SubTour1 = parent2RemainingTour[0..startIndex];
            var parent2SubTour2 = parent2RemainingTour[startIndex..^0];

            parent2SubTour1.CopyTo(childtour, 0);
            parent2SubTour2.CopyTo(childtour, endIndex);

            return childtour;
        }

        private void TwoOptChange(int[] childTour, double mutationProbapility)
        {
            //Swap(); ... Kanten tauschen

            if (random.NextDouble() < mutationProbapility)
            {
                //hier zwei zufällige Kanten
                //childTour.Swap(random.Next(1, childTour.Length / 2), random.Next(childTour.Length / 2, childTour.Length));
                
                //hier zwei benachbarte Kanten:
                int index1 = random.Next(childTour.Length);
                int index2 = (index1 + 1) % childTour.Length;
                childTour.Swap(index1, index2);
            }
        }

        private class Individual
        {
            public Individual(int[] tour, double fitness)
            {
                Tour = tour;
                Fitness = fitness;
            }

            public int [] Tour { get; }
            public double Fitness { get; }
        }
    }
}