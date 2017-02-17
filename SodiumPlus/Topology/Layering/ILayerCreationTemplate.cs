using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Layering
{
    interface ILayerCreationTemplate<in TUnit, in TUnitActivation>
        where TUnitActivation : IUnitActivation<TUnit>
    {
        void AddUnitActivation(TUnitActivation unitActivation);
    }
}
