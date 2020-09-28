using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems.TSP
{
    public static class Utils
    {
        public static double GetDistance(IReadOnlyCollection<int> solution, IReadOnlyList<Location> cities)
        {
            double distance = 0.0;
            int previousCityIndex = solution.Last();

            foreach (int cityIndex in solution)
            {
                distance += GetDistance(cities[previousCityIndex - 1], cities[cityIndex - 1]);
                previousCityIndex = cityIndex;
            }
            return distance;
        }

        public static double GetDistance(Location a, Location b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}