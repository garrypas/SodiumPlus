using System;
using System.Diagnostics;

namespace SodiumPlus.Topology
{
    [DebuggerDisplay("Name = {Name}, Weight = {Weight}")]
    public class TraversableConnection<TUnit, TConnection, TUnitActivation> : ITraversableConnection<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
    {
        protected TraversableConnection() { }

        public ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation> OutputUnit { get; private set; }

        public ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation> InputUnit { get; private set; }

        public static ITraversableConnection<TUnit, TConnection, TUnitActivation> CreateConnection<TConnectionImpl>(ITraversableUnit<TUnit, TConnection, TUnitActivation> unitBelow, ITraversableUnit<TUnit, TConnection, TUnitActivation> unitAbove)
            where TConnectionImpl: TConnection, new()
        {
            var connection = new TraversableConnection<TUnit, TConnection, TUnitActivation>
            {
                Properties = new TConnectionImpl(),
                InputUnit = unitBelow,
                OutputUnit = unitAbove
            };
            unitBelow.AddOutgoingConnection(connection);
            unitAbove.AddIncomingConnection(connection);
            return connection;
        }

        public TConnection Properties { get; set; }

#if DEBUG
        private double Weight { get { return Properties.Weight; } }
        private string Name { get { return Properties.Name; } }
#endif
    }
}
