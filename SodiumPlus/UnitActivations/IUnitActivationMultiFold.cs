using System.Collections.Generic;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public interface IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation> : IUnitActivationCreatable<TUnit>
        where TUnit : IUnit
    {
        IEnumerable<IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>> Network { get; set; }
    }
}