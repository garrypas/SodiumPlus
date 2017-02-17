using SodiumPlus.UnitActivations;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlus.Topology.Layering
{
    internal static class LayeredUnitCollectionHelper<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        public static IEnumerable<IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>> LayerUnits(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> inputUnits)
        {
            var layered = new List<IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>>();

            List<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> thisList = null;

            foreach (var item in Sort(inputUnits))
            {
                var descendents = item.GetAllInputsRecursive();
                if (thisList == null || descendents.Any(d => thisList.Contains(d)) || (layered.Count == 1 && item.UnitActivation.UnitType != UnitType.InputUnit))
                {
                    thisList = new List<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>();
                    layered.Add(thisList);
                }
                thisList.Add(item);
            }
            return layered;
        }

        private static IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> GetEveryUnit(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> units)
        {
            // Scan forward:
            var unitsAbove = GetEveryUnitAbove(units);
            var unitsBelow = GetEveryUnitBelow(unitsAbove);
            return units.Union(unitsAbove.Union(unitsBelow)).Distinct();
        }

        private static IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> GetEveryUnitAbove(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> units)
        {
            // Scan forward:
            if(units.Any() == false)
            {
                return Enumerable.Empty<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>();
            }
            return GetEveryUnitAbove(units.SelectMany(u => u.UnitsAbove)).Union(units);
        }

        private static IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> GetEveryUnitBelow(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> units)
        {
            // Scan back:
            if (units.Any() == false)
            {
                return Enumerable.Empty<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>();
            }
            return GetEveryUnitBelow(units.SelectMany(u => u.UnitsBelow)).Union(units);
        }

        private static IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> Sort(IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>> units)
        {
            units = GetEveryUnit(units);
            return units.OrderBy(u => u).ToList();
        }
    }
}