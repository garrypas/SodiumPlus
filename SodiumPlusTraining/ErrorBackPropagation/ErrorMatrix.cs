using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    internal static class ErrorMatrix
    {
        public static IEnumerable<double> GetErrorsInLayerAbove(ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> thisUnit)
        {
            var outgoingConnections = thisUnit.OutgoingConnections;
            var previousLayerErrors = outgoingConnections.Select(connection => connection.OutputUnit.UnitActivation.Properties.Error);
            return previousLayerErrors;
        }
    }
}