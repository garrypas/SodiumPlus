using System;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions
{
    /// <summary>
    /// Calculates the least-mean squared error associated with a given set of desired and actual output values 
    /// </summary>
    public class LmsErrorFunction : INetworkErrorFunction
    {
        public double Calculate(IEnumerable<double> desiredValues, IEnumerable<double> actualValues)
        {
            return desiredValues.SelectWithAdjacent(actualValues, DesiredValueMinusActualValueAllToTheTwo).Sum() * 0.5;
        }

        private static double DesiredValueMinusActualValueAllToTheTwo(double desiredValue, double actualValue)
        {
            return Math.Pow(desiredValue - actualValue, 2);
        }
    }
}
