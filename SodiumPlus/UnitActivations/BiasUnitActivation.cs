using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public class BiasUnitActivation<TUnit> : UnitActivationBase<TUnit>
        where TUnit : IUnit
    {
        public BiasUnitActivation() : base(new IdentityActivationFunction()) { }

        public override UnitType UnitType { get { return UnitType.BiasUnit; } }
    }
}