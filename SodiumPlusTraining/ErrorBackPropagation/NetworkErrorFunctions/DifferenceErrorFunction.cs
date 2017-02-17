using System;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions
{
    /// <summary>
    /// Calculates the total difference between a given set of desired and actual output values 
    /// </summary>
    public class DifferenceErrorFunction : INetworkErrorFunction
    {
        public double Calculate(IEnumerable<double> desiredValues, IEnumerable<double> actualValues)
        {
            return desiredValues.SelectWithAdjacent(actualValues, DesiredValueMinusActualValue).Sum();
        }

        private static double DesiredValueMinusActualValue(double desiredValue, double actualValue)
        {
            return Math.Abs(desiredValue - actualValue);
        }
    }
}
