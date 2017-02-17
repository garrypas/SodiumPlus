using System.Collections.Generic;
using SodiumPlus.Topology.Layering;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
    {
        internal ICollection<LayerCreationTemplate<TUnit, TConnection, TUnitActivation>> Layers { get; set; }

        public int NumberOfUnitsNextLayer { get; set; }

        public double SlopeMultiplier { get; set; }

        public double Bias { get; set; }
        public ICollection<ICollection<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> Network { get; private set; }

        public GenericNetworkBuilderState()
        {
            SlopeMultiplier = 1d;
            Layers = new List<LayerCreationTemplate<TUnit, TConnection, TUnitActivation>>();
            Network = new List<ICollection<ITraversableUnit<TUnit, TConnection, TUnitActivation>>>();
        }
    }
}