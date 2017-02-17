using System.Collections.Generic;
using System.Linq;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class NetworkCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> : INetworkCreator<TUnit,TConnection,TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        public void Build(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> state)
        {
            AddUnitsToLayers(state);
            AddBiasUnits(state);
            AddSlopeMultiplier(state);
            ConnectUnits(state);
        }

        private static void AddUnitsToLayers(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> state)
        {
            foreach (var layer in state.Layers)
            {
                var isInputLayer = layer == state.Layers.First();
                var thisLayer = new List<ITraversableUnit<TUnit, TConnection, TUnitActivation>>();
                for (var i = 0; i < layer.NumberOfUnitsToAdd; i++)
                {
                    var unit = isInputLayer
                        ? TraversableUnit<TUnit, TConnection, TUnitActivation>.CreateUnit<TUnitImpl, TInputUnitImpl>(new TInputUnitImpl())
                        : TraversableUnit<TUnit, TConnection, TUnitActivation>.CreateUnit<TUnitImpl, TUnitActivation>(layer.CreateUnitActivation());

                    thisLayer.Add(unit);
                }
                state.Network.Add(thisLayer);
            }
        }

        private static void AddSlopeMultiplier(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> state)
        {
            state.Network.SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.NormalUnit).Enumerate(u => u.UnitActivation.Properties.SlopeMultiplier = state.SlopeMultiplier);
        }

        private static void AddBiasUnits(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> state)
        {
            if (!(state.Bias > 0d))
            {
                return;
            }

            for (var i = 0; i < state.Network.Count - 1; i++)
            {
                var thisLayer = state.Network.ElementAt(i);

                var biasUnit = TraversableUnit<TUnit, TConnection, TUnitActivation>.CreateUnit<TUnitImpl, TBiasUnitImpl>(new TBiasUnitImpl());
                biasUnit.UnitActivation.Properties.NetInput = state.Bias;
                thisLayer.Add(biasUnit);
            }
        }

        private static void ConnectUnits(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> state)
        {
            // Connect everything
            for (var i = 0; i < state.Network.Count - 1; i++)
            {
                var thisLayer = state.Network.ElementAt(i);
                var nextLayer = state.Network.ElementAt(i + 1);
                thisLayer.EnumerateCartesian(nextLayer, (l1, l2) => TraversableConnection<TUnit, TConnection, TUnitActivation>.CreateConnection<TConnectionImpl>(l1, l2));
            }
        }
    }
}