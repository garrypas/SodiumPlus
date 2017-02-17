using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public class InputUnitActivation<TUnit> : UnitActivationBase<TUnit>
        where TUnit : IUnit
    {
        public InputUnitActivation() : base(new IdentityActivationFunction()) {  }

        public override UnitType UnitType { get { return UnitType.InputUnit; } }
    }
}