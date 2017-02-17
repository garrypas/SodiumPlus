using System.Collections.Generic;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Namers
{
    public class ConnectionNamer<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        public void NameAllConnectionsInNetwork(IEnumerable<IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> network)
        {
            network.Enumerate(NameAllOutgoingConnections);
        }

        private static void NameAllOutgoingConnections(IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>> layer)
        {
            layer.Enumerate((unit, unitIndex) => unit.OutgoingConnections.Enumerate(NameConnection));
        }

        private static void NameConnection(ITraversableConnection<TUnit, TConnection, TUnitActivation> connection)
        {
            connection.Properties.Name = connection.InputUnit.UnitActivation.Name + " --- " + connection.OutputUnit.UnitActivation.Name;
        }
    }
}