using System.Collections.Generic;

namespace SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions
{
    /// <summary>
    /// A generic interface for calculating network error, used in cost reduction during back-propagation
    /// </summary>
    public interface INetworkErrorFunction
    {
        double Calculate(IEnumerable<double> desiredValues, IEnumerable<double> actualValues);
    }
}