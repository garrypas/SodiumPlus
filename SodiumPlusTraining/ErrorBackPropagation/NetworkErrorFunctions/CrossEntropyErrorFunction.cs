using System;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions
{
    /// <summary>
    /// Calculates the cross-entropy error associated with a given set of desired and actual output values 
    /// </summary>
    public class CrossEntropyErrorFunction : INetworkErrorFunction
    {
        public double Calculate(IEnumerable<double> desiredValues, IEnumerable<double> actualValues)
        {
            return -actualValues.SelectWithAdjacent(desiredValues, (o, t) => Math.Log(o) * t).Sum();
        }
    }
}
