using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public class BipolarUnitActivation<TUnit> : UnitActivationBase<TUnit>
        where TUnit: IUnit
    {
        public BipolarUnitActivation() : base(new BipolarActivationFunction()) { }

        public override UnitType UnitType { get { return UnitType.NormalUnit; } }
    }
}