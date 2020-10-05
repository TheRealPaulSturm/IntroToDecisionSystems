using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;



namespace DecisionSystems.TSP
{
    public static class Utils
    {
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

            Location getLocation(int solutionIndex)
            {
                return cities[solutionIndex - 1];
            }

            return solution
                .Append(solution.First())
                .Select(getLocation)
                .Pairwise(GetDistance)
                .Sum();
        }

        private static double GetDistance(Location help, Location location)
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
       
    }
}