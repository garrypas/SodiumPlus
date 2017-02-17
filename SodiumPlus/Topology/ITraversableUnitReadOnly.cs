using System;
using System.Collections.Generic;

namespace SodiumPlus.Topology
{
    public interface ITraversableUnitReadOnly<out TUnit, out TConnection, out TUnitActivation> : IComparable
    {
        IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> UnitsAbove { get; }
        IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> UnitsBelow { get; }
        IEnumerable<ITraversableConnection<TUnit, TConnection, TUnitActivation>> IncomingConnections { get; }
        IEnumerable<ITraversableConnection<TUnit, TConnection, TUnitActivation>> OutgoingConnections { get; }
        IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> GetAllInputsRecursive();

        TUnitActivation UnitActivation { get; }
        void LoadNetInput();
        double ActivationValue { get; }
        double NetInput { get; }
    }
}
