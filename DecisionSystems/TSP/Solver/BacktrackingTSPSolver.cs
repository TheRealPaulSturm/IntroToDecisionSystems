﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.TSP.Solver
{
    public class BacktrackingTSPSolver : ITSPSolver
    {
        public List<int> Solve(IReadOnlyList<Location> cities)
        {
            var permutations = CalculatePermutations(cities);
            return permutations.BestBy(tour => Utils.GetDistance(tour, cities), (v1, v2) => v1 < v2).ToList();
        }

        private List<int[]> CalculatePermutations(IReadOnlyList<Location> cities)
        {
            var result = new List<int[]>();
            var baseTour = Enumerable.Range(1, cities.Count).ToArray();
            CalculatePermutationsRecursive(baseTour, 0, result);
            return result;
        }

        private void CalculatePermutationsRecursive(int[] baseTour, int startIndex, List<int[]> result)
        {
            if (startIndex == baseTour.Length - 1)
            {
                result.Add(baseTour.ToArray());
            }
            else
            {
                //Calculate permitations by placing baseTour[startIndex] at every possible index.
                //Then calculate permutations of elements with index > startIndex.

                for (int i = startIndex; i < baseTour.Length; i++)
                {
                    baseTour.Swap(startIndex, i);
                    CalculatePermutationsRecursive(baseTour, startIndex + 1, result);
                    baseTour.Swap(startIndex, i);
                }
            }
        }
    }
}