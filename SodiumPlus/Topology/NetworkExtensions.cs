using System;
using System.Collections.Generic;
using System.Linq;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology
{
    public static class NetworkExtensions
    {
        public static ITraversableUnit<TUnit, TConnection, TUnitActivation> FindUnit<TUnit, TConnection, TUnitActivation>(this IEnumerable<IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> network, string name)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivation<TUnit>
        {
            foreach (var layer in network)
            {
                foreach (var unit in layer)
                {
                    if (NamesMatch(name, unit.UnitActivation.Name))
                    {
                        return unit;
                    }
                }
            }
            throw new ArgumentException(string.Format("The unit named '{0}' could not be found.", name));
        }

        public static ITraversableConnection<TUnit, TConnection, TUnitActivation> FindConnection<TUnit, TConnection, TUnitActivation>(this IEnumerable<IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> network, string unitBelow, string unitAbove)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivation<TUnit>
        {
            var match = FindConnectionWithoutException(network, unitBelow, unitAbove);
            if (match != null)
            {
                return match;
            }
            throw new ArgumentException(string.Format("The connection from '{0}' to '{1}' could not be found.", unitBelow, unitAbove));
        }

        private static ITraversableConnection<TUnit, TConnection, TUnitActivation> FindConnectionWithoutException<TUnit, TConnection, TUnitActivation>(this IEnumerable<IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> network, string unitBelow, string unitAbove)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivation<TUnit>
        {
            var allConnections = network.SelectMany(u => u).SelectMany(u => u.IncomingConnections);
            var match = allConnections.FirstOrDefault(c => NamesMatch(unitBelow, c.InputUnit.UnitActivation.Name)
                                                           && NamesMatch(unitAbove, c.OutputUnit.UnitActivation.Name));
            return match;
        }

        private static bool NamesMatch(string name, string otherName)
        {
            return string.Equals(name, otherName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
