using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.TSP.Solver
{
    public class RandomSolver : ITSPSolver
    {
        private readonly int iterations;
        public RandomSolver(int iterations)
        {
            if (iterations < 1)
            {
                throw new ArgumentException("There must be at least one iteration!");
            }
            this.iterations = iterations;
        }

        public List<int> Solve(IReadOnlyList<Location> cities)
        {
            //Random numberGenerator = new Random();
            //var remainingIndicies = Enumerable.Range(1, cities.Count).ToList();
            //var reslut = new List<int>();
            //while (remainingIndicies.Count > 0)
            //{
            //    var index = numberGenerator.Next(0, remainingIndicies.Count);
            //    reslut.Add(remainingIndicies[index]);
            //    remainingIndicies.RemoveAt(index);
            //}
            //return reslut;

            //mehrere Iterationen eigene Lösung:

            //List<int> shortestList = Enumerable.Range(1, cities.Count).Shuffle().ToList();
            //double shortestDistance = Utils.GetDistance(shortestList, cities);

            //for (int i = 0; i < iterations; i++)
            //{
            //    List<int> helpList = Enumerable.Range(1, cities.Count).Shuffle().ToList();
            //    double help = Utils.GetDistance(helpList, cities);

            //    if (help < shortestDistance)
            //    {
            //        shortestDistance = help;
            //        shortestList = helpList;
            //    }
            //}

            //return shortestList;

            //Linq-Lösung

            return Enumerable
                .Repeat(0, iterations)
                .Select(_ => Enumerable.Range(1, cities.Count).Shuffle().ToList())
                .OrderBy(solution => Utils.GetDistance(solution, cities))
                .First();
        }
    }
}