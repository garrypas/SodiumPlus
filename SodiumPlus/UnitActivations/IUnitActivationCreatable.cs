using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public interface IUnitActivationCreatable<TUnit> : IUnitActivation<TUnit> where TUnit : IUnit
    {
        new TUnit Properties { get; set; }
    }
}