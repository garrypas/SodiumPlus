using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public interface IUnitActivationTraining : IUnitActivationCreatable<IUnitUnderTraining>
    {
        double Derivative();
        double Error { get; }
        IUnitActivationCreatable<IUnit> Unwrap();
    }
}
