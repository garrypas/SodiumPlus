using SodiumPlus.UnitActivations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SodiumPlus.Topology
{
    /// <summary>
    /// A unit connected to other units whose connections can be traversed
    /// </summary>
    /// <typeparam name="TUnit">IUnit, or an implementation</typeparam>
    /// <typeparam name="TConnection">IConnection, or an implementation</typeparam>
    /// <typeparam name="TUnitActivation">IUnitActivation&lt;IUnit&gt; or an implementation</typeparam>
    [DebuggerDisplay("Name = {Name}")]
    public class TraversableUnit<TUnit, TConnection, TUnitActivation> : ITraversableUnit<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        private readonly List<ITraversableConnection<TUnit, TConnection, TUnitActivation>> _incomingConnections;
        private readonly List<ITraversableConnection<TUnit, TConnection, TUnitActivation>> _outgoingConnections;

        protected TraversableUnit()
        {
            _incomingConnections = new List<ITraversableConnection<TUnit, TConnection, TUnitActivation>>();
            _outgoingConnections = new List<ITraversableConnection<TUnit, TConnection, TUnitActivation>>();
        }

        public static ITraversableUnit<TUnit, TConnection, TUnitActivation> CreateUnit<TUnitImpl, TUnitActivationImpl>(TUnitActivationImpl unitActivation)
            where TUnitImpl : TUnit, new()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationCreatable<TUnit>
        {
            var unit = new TUnitImpl();
            unitActivation.Properties = unit;
            var traversableUnit = new TraversableUnit<TUnit, TConnection, TUnitActivation>
            {
                UnitActivation = unitActivation
            };
            return traversableUnit;
        }

        public static ITraversableUnit<TUnit, TConnection, TUnitActivation> CreateUnit<TUnitImpl, TUnitActivationImpl>()
            where TUnitImpl : TUnit, new()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            var unit = new TUnitImpl();
            var unitActivation = new TUnitActivationImpl
            {
                Properties = unit
            };
            var traversableUnit = new TraversableUnit<TUnit, TConnection, TUnitActivation>
            {
                UnitActivation = unitActivation
            };
            return traversableUnit;
        }

        /// <summary>
        /// Gets every unit below this one in the topology
        /// </summary>
        /// <returns>Every unit below this one in the topology</returns>
        public IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> GetAllInputsRecursive()
        {
            return UnitHelpers.GetAllDescendents(this);
        }

        /// <summary>
        /// All incoming connections to this unit (i.e. from units closer to the inputs that this unit)
        /// </summary>
        public IEnumerable<ITraversableConnection<TUnit, TConnection, TUnitActivation>> OutgoingConnections
        {
            get
            {
                return _outgoingConnections;
            }
        }

        /// <summary>
        /// All outgoing connections from this unit (i.e. to units closer to the outputs that this unit)
        /// </summary>
        public IEnumerable<ITraversableConnection<TUnit, TConnection, TUnitActivation>> IncomingConnections
        {
            get
            {
                return _incomingConnections;
            }
        }

        /// <summary>
        /// All input units with connections to this unit (i.e. units closer to the inputs that this unit)
        /// </summary>
        public IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> UnitsAbove
        {
            get
            {
                return _outgoingConnections.Select(connection => connection.OutputUnit);
            }
        }

        /// <summary>
        /// All output units with connections from this unit (i.e. units closer to the outputs that this unit)
        /// </summary>
        public IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> UnitsBelow
        {
            get
            {
                return _incomingConnections.Select(connection => connection.InputUnit);
            }
        }

        /// <summary>
        /// Used to add an incoming connection, from a unit that sends input, to this unit
        /// </summary>
        /// <param name="connection">The incoming connection to be added</param>
        public void AddIncomingConnection(ITraversableConnection<TUnit, TConnection, TUnitActivation> connection)
        {
            _incomingConnections.Add(connection);
        }

        /// <summary>
        /// Used to add an outgoing connection, to a unit that received input, from this unit
        /// </summary>
        /// <param name="connection">The outgoing connection to be added</param>
        public void AddOutgoingConnection(ITraversableConnection<TUnit, TConnection, TUnitActivation> connection)
        {
            _outgoingConnections.Add(connection);
        }

        private static int Compare(ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation> x, ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation> y)
        {
            if (x.GetAllInputsRecursive().Any(xChild => xChild == y)) return 1;
            if (y.GetAllInputsRecursive().Any(yChild => yChild == x)) return -1;
            return 0;
        }

        public int CompareTo(object obj)
        {
            return Compare(this, obj as TraversableUnit<TUnit, TConnection, TUnitActivation>);
        }

        /// <summary>
        /// Loads the sum of activation * weight for all incoming connections and stimulates the unit activation
        /// </summary>
        public void LoadNetInput()
        {
            var netInput = UnitActivation.UnitType == UnitType.NormalUnit
                ? IncomingConnections.Sum(i => i.Properties.Weight * i.InputUnit.ActivationValue)
                : UnitActivation.NetInput;

            UnitActivation.Stimulate(netInput);
        }
        
        public double ActivationValue { get { return UnitActivation.ActivationValue; } }
        public double NetInput { get { return UnitActivation.NetInput; } }

        public TUnitActivation UnitActivation { get; set; }

#if DEBUG
        private string Name { get { return UnitActivation.Name; } }
#endif
    }
}