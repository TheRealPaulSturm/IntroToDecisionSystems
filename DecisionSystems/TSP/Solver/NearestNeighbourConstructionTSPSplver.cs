﻿
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.TSP.Solver
{
    public class NearestNeighbourConstructionTSPSolver : ITSPSolver
    {
        public List<int> Solve(IReadOnlyList<Location> cities)
        {
            var result = Enumerable.Range(1, cities.Count).ToList();

            for (int i = 1; i < result.Count; i++)
            {
                var minDistance = cities.GetDistance(result[i - 1], result[i]);
                var next = i;

                for (int j = i + 1; j < result.Count; j++)
                {
                    var currentDistance = cities.GetDistance(result[i - 1], result[j]);
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