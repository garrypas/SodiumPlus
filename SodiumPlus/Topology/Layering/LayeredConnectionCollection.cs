using SodiumPlus.UnitActivations;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlus.Topology.Layering
{
    public class LayeredConnectionCollection<TUnit, TConnection, TUnitActivation>
        where TUnit : class, IUnit
        where TConnection : class, IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        private readonly IEnumerable<IEnumerable<ITraversableConnection<TUnit, TConnection, TUnitActivation>>> _layeredConnections;

        public LayeredConnectionCollection(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> inputUnits)
        {
            var unitsInLayers = LayeredUnitCollectionHelper<TUnit, TConnection, TUnitActivation>.LayerUnits(inputUnits);
            _layeredConnections = unitsInLayers.Skip(1).Reverse().Select(GetAllIncomingConnections).Reverse();
        }

        public static IEnumerable<ITraversableConnection<TUnit, TConnection, TUnitActivation>> GetAllIncomingConnections(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> units)
        {
            return units.SelectMany(e => e.IncomingConnections);
        }
        
        public IEnumerable<IEnumerable<ITraversableConnection<TUnit, TConnection, TUnitActivation>>> GetLayeredConnection()
        {
            return _layeredConnections.Where(e => e.Any());
        }
    }
}