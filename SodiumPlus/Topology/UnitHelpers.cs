using System.Collections.Generic;
using System.Linq;

namespace SodiumPlus.Topology
{
    public static class UnitHelpers
    {
        public static IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> GetAllDescendents<TUnit, TConnection, TUnitActivation>(ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation> unit)
            where TConnection : IConnection
        {
            return unit.UnitsBelow.Union(unit.UnitsBelow.SelectMany(u => u.UnitsBelow));
        }
    }
}
