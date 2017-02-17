using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public interface IUnitActivationSingleFold<TUnit> : IUnitActivationCreatable<TUnit> where TUnit : IUnit { }
}