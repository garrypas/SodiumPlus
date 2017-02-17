using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public class ReluUnitActivation<TUnit> : UnitActivationBase<TUnit>
        where TUnit: IUnit
    {
        public ReluUnitActivation() : base(new ReluActivationFunction()) { }

        public override UnitType UnitType { get { return UnitType.NormalUnit; } }

    }
}