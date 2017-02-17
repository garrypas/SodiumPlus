using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public class HyperbolicTangentUnitActivation<TUnit> : UnitActivationBase<TUnit>
        where TUnit: IUnit
    {
        public HyperbolicTangentUnitActivation() : base(new HyperbolicTangentActivationFunction()) { }

        public override UnitType UnitType { get { return UnitType.NormalUnit; } }
    }
}