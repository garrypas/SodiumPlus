using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusTraining.Perceptrons
{
    using TUnit = ITraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>;
    using TUnitUnderTraining = ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>;
    using TUnitActivationMultiFold = IUnitActivationMultiFold<IUnit, IConnection, IUnitActivation<IUnit>>;

    public class PerceptronFinalizer
    {
        public IEnumerable<IEnumerable<TUnit>> Clean(IEnumerable<IEnumerable<TUnitUnderTraining>> network)
        {
            var unitMapper = network.SelectMany(u => u).ToDictionary(u => u, CreateCleanUnit);
            var newNetwork = new List<IEnumerable<TUnit>>();
            foreach (var layer in network)
            {
                var newLayer = new List<TUnit>();
                layer.Enumerate(oldUnit => newLayer.Add(unitMapper[oldUnit]));
                newNetwork.Add(newLayer);
            }

            var oldConnections = unitMapper.SelectMany(u => u.Key.OutgoingConnections);
            oldConnections.Enumerate(c =>
            {
                var thisNewUnit = unitMapper[c.InputUnit];
                var otherNewUnit = unitMapper[c.OutputUnit];
                var newConnection = TraversableConnection<IUnit, IConnection, IUnitActivation<IUnit>>.CreateConnection<Connection>(thisNewUnit, otherNewUnit);
                newConnection.Properties.Name = c.Properties.Name;
                newConnection.Properties.Weight = c.Properties.Weight;
            });

            //Map network to multifolds
            unitMapper.Select(u => u.Value.UnitActivation).Where(ua => ua is TUnitActivationMultiFold).Cast<TUnitActivationMultiFold>().Enumerate(ua => ua.Network = newNetwork);

            return newNetwork;
        }

        private static TUnit CreateCleanUnit(TUnitUnderTraining dirtyUnit)
        {
            var unit = TraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>.CreateUnit<Unit, IUnitActivationCreatable<IUnit>>(dirtyUnit.UnitActivation.Unwrap());
            unit.UnitActivation.Properties.ActivationValue = dirtyUnit.UnitActivation.Properties.ActivationValue;
            unit.UnitActivation.Properties.Name = dirtyUnit.UnitActivation.Properties.Name;
            unit.UnitActivation.Properties.NetInput = dirtyUnit.UnitActivation.Properties.NetInput;
            unit.UnitActivation.Properties.SlopeMultiplier = dirtyUnit.UnitActivation.Properties.SlopeMultiplier;
            return unit;
        }
    }
}
