
using System.Collections.Generic;

namespace DecisionSystems.TSP.Solver
{
    public class DummySolver : ITSPSolver
    {
        public List<int> Solve(IReadOnlyList<Location> cities)
        {
            var output = new List<int>(cities.Count);

            for (int i = 1; i <= cities.Count; i++)
            {
                output.Add(i);
            }
            return output;
        
        }
    }
}