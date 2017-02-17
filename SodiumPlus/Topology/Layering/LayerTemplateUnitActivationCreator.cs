using SodiumPlus.Topology.Builder.Generic;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Layering
{

    internal static class LayerTemplateUnitActivationCreator<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
    {
        public static void AddSingleFold<TUnitActivationImpl>(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> builderState)
            where TUnitActivationImpl : TUnitActivation, IUnitActivationSingleFold<TUnit>, new()
        {
            var newLayer = SetupLayerTemplate(builderState);
            newLayer.CreateUnitActivation = () => new TUnitActivationImpl();
        }

        public static void AddMultiFold<TUnitActivationImpl>(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> builderState)
            where TUnitActivationImpl : TUnitActivation, IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation>, new()
        {
            var newLayer = SetupLayerTemplate(builderState);
            newLayer.CreateUnitActivation = () =>
            {
                var unitActivation = new TUnitActivationImpl
                {
                    Network = builderState.Network
                };
                return unitActivation;
            };
        }

        private static LayerCreationTemplate<TUnit, TConnection, TUnitActivation> SetupLayerTemplate(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> builderState)
        {
            var numberOfUnits = builderState.NumberOfUnitsNextLayer;
            var newLayer = new LayerCreationTemplate<TUnit, TConnection, TUnitActivation>(numberOfUnits);
            builderState.Layers.Add(newLayer);
            return newLayer;
        }
    }
}