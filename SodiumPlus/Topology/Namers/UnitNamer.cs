using System.Collections.Generic;
using System.Linq;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Namers
{
    public class UnitNamer<TUnit, TConnection, TUnitActivation>
        where TUnit:IUnit
        where TUnitActivation : IUnitActivation<TUnit>
    {
        public void NameAllUnitsInNetwork(ICollection<ICollection<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> network)
        {
            if (network.Any())
            {
                NameAllInputUnits(network.First(), 0);
            }

            if(network.Count() > 1) {
                NameAllOutputUnits(network.Last(), 0);
            }

            for (var i = 1; i < network.Count() - 1; i++)
            {
                var layer = network.ElementAt(i);
                NameAllHiddenUnits(layer, i);
            }
        }

        private static void NameAllInputUnits(IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>> layer, int layerIndex)
        {
            layer.Enumerate((unit, unitIndex) =>
            {
                if (unit.UnitActivation.UnitType == UnitType.BiasUnit)
                    NameBiasUnit(unit.UnitActivation.Properties, layerIndex, unitIndex);
                else
                    NameInputUnit(unit.UnitActivation.Properties, unitIndex);
            });
        }

        private static void NameInputUnit(IUnit inputUnit, int unitIndex)
        {
            inputUnit.Name = "I(1)(" + (unitIndex + 1) + ")";
        }

        private static void NameAllOutputUnits(IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>> layer, int layerIndex)
        {
            layer.Enumerate((unit, unitIndex) =>
            {
                if (unit.UnitActivation.UnitType == UnitType.BiasUnit)
                    NameBiasUnit(unit.UnitActivation.Properties, layerIndex, unitIndex);
                else
                    NameOutputUnit(unit.UnitActivation.Properties, layerIndex, unitIndex);
            });
        }

        private static void NameOutputUnit(IUnit outputUnit, int layerIndex, int unitIndex)
        {
            outputUnit.Name = "O(" + (layerIndex + 1) + ")(" + (unitIndex + 1) + ")";
        }

        private static void NameAllHiddenUnits(IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>> layer, int layerIndex)
        {
            layer.Enumerate((unit, unitIndex) =>
            {
                if (unit.UnitActivation.UnitType == UnitType.BiasUnit)
                    NameBiasUnit(unit.UnitActivation.Properties, layerIndex, unitIndex);
                else
                    NameHiddenUnit(unit.UnitActivation.Properties, layerIndex, unitIndex);
            });
        }

        private static void NameHiddenUnit(IUnit hiddenUnit, int layerIndex, int unitIndex)
        {
            hiddenUnit.Name = "H(" + (layerIndex + 1) + ")(" + (unitIndex + 1) + ")";
        }

        private static void NameBiasUnit(IUnit biasUnit, int layerIndex, int unitIndex)
        {
            biasUnit.Name = "B(" + (layerIndex + 1) + ")(" + (unitIndex + 1) + ")";
        }
    }
}
