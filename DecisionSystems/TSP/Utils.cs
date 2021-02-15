using System;
using System.Collections.Generic;
using System.Linq;



namespace DecisionSystems.TSP
{
    public static class Utils
    {

        public static double GetDistance(Location help, Location location)
        {
            if (help is null)
            {
                throw new ArgumentNullException(nameof(help));
            }

            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return System.Math.Sqrt(Math.Pow((location.X - help.X),2.00) + Math.Pow((location.Y - help.Y), 2.00));
        }
       
        public static double GetDistance(this IReadOnlyList<Location> cities, int cityIndex1, int cityIndex2)
        {
            return GetDistance(cities[cityIndex1 - 1], cities[cityIndex2 - 1]);
        }
        public static double GetDistance(IReadOnlyCollection<int> solution, IReadOnlyList<Location> cities)
        {
            //var distance = 0.0;
            //Location help = new Location(0,0);
            //help = cities[solution.Last()-1];                

            //foreach (int index in solution)
            //{
            //    distance +=GetDistance(help, cities[index-1]);
            //    help = cities[index-1];
            //}

            //return distance;

            return solution
                .Append(solution.First())
                .Pairwise(cities.GetDistance)
                .Sum();
        }
    }
}