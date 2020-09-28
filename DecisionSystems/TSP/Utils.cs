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
            var distance = 0.0;
            Location help = new Location(0,0);
            help = cities[solution.Last()-1];                
            
            foreach (int index in solution)
            {
                distance +=GetDistance(help, cities[index-1]);
                help = cities[index-1];
            }
           
            return distance;
        }

        private static double GetDistance(Location help, Location location)
        {
            return System.Math.Sqrt(Math.Pow((location.X - help.X),2.00) + Math.Pow((location.Y - help.Y), 2.00));
        }
    }
}