using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public class IdentityUnitActivation<TUnit> : UnitActivationBase<TUnit>
        where TUnit : IUnit
    {
        public IdentityUnitActivation() : base(new IdentityActivationFunction()) { }

        public override UnitType UnitType { get { return UnitType.NormalUnit; } }
    }
}