using SodiumPlus.UnitActivations;
using System.Collections.Generic;

namespace SodiumPlus.Topology.Layering
{
    public class LayeredUnitCollection<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        private readonly IEnumerable<IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>> _layeredUnits;

        public LayeredUnitCollection(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> inputUnits)
        {
            _layeredUnits = LayeredUnitCollectionHelper<TUnit, TConnection, TUnitActivation>.LayerUnits(inputUnits);
        }

        public IEnumerable<IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>> GetLayeredUnits()
        {
            return _layeredUnits;
        }
    }
}