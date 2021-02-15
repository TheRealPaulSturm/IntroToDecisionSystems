
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.TSP.Solver
{
    public class NearestNeighbourConstructionWithOptimalStartTSPSolver : ITSPSolver
    {
        public List<int> Solve(IReadOnlyList<Location> cities)
        {
            var bestTour = SolveWithStartCity(cities, 1);
            var minDistance = Utils.GetDistance(bestTour, cities);

            for (int i = 2; i < cities.Count; i++)
            {
                var tour = SolveWithStartCity(cities, i);
                var distance = Utils.GetDistance(tour, cities);

                if (distance < minDistance)
                {
                    bestTour = tour;
                }
            }
            return bestTour;
        }

        private static List<int> SolveWithStartCity(IReadOnlyList<Location> cities, int startCity)
        {
            List<int> result = Enumerable.Range(1, cities.Count).ToList();
            result.Swap(0, startCity - 1);

            for (int i = 1; i < result.Count; i++)
            {
                var minDistance = cities.GetDistance(result[i - 1], result[i]);
                var next = i;

                for (int j = i + 1; j < result.Count; j++)
                {
                    var currentDistance = cities.GetDistance(result[j - 1], result[j]);
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        next = j;
                    }
                }
                result.Swap(i, next);
            }
            return result;
        }
    }
}