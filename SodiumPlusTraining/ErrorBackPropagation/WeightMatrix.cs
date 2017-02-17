using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusTraining.ErrorBackPropagation
{

    internal static class WeightMatrix
    {
        public static IEnumerable<double> GetWeightsToLayerAbove(ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> thisUnit)
        {
            var outgoingConnections = thisUnit.OutgoingConnections;
            var weightsToUpperUnits = outgoingConnections.Select(connection => connection.Properties.Weight);
            return weightsToUpperUnits;
        }
    }
}