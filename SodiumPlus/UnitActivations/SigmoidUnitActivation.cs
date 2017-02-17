using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public class SigmoidUnitActivation<TUnit> : UnitActivationBase<TUnit>
        where TUnit : IUnit
    {
        public SigmoidUnitActivation() : base(new SigmoidActivationFunction()) { }

        public override UnitType UnitType { get { return UnitType.NormalUnit; } }
    }
}