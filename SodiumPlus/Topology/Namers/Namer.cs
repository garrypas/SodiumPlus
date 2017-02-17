using System.Collections.Generic;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Namers
{
    public class Namer<TUnit, TConnection, TUnitActivation> : INamer<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        private readonly UnitNamer<TUnit, TConnection, TUnitActivation> _unitNamer;
        private readonly ConnectionNamer<TUnit, TConnection, TUnitActivation> _connectionNamer;

        public Namer()
        {
            _unitNamer = new UnitNamer<TUnit, TConnection, TUnitActivation>();
            _connectionNamer = new ConnectionNamer<TUnit, TConnection, TUnitActivation>();
        }

        public void NameItemsInNetwork(ICollection<ICollection<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> network)
        {
            _unitNamer.NameAllUnitsInNetwork(network);
            _connectionNamer.NameAllConnectionsInNetwork(network);
        }
    }
}
