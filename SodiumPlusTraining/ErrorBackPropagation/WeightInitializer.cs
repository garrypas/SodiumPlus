using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SodiumPlus.Diagnostics;
using SodiumPlusTraining.Topology;
using SodiumPlus.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class WeightInitializer : IWeightInitializer
    {
        private readonly IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> _layeredUnits;
        private readonly IWeightSetter _weightSetter;
        private const string WeightInitialized = "WeightInitialized";

        public WeightInitializer(IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> layeredUnits, IWeightSetter weightSetter)
        {
            _weightSetter = weightSetter;
            _layeredUnits = layeredUnits;
        }

        public async Task PropagateWeightInitializationAsync()
        {
            foreach (var connections in _layeredUnits.Skip(1).SelectMany(u => u).Select(u => u.IncomingConnections))
            {
                await PropagateWeightInitialization(connections);
            }
        }

        private async Task PropagateWeightInitialization(IEnumerable<ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> connections)
        {
            foreach (var connection in connections)
            {
                await Task.Run(() => InitializeWeight(connection.Properties));
            }
        }

        private void InitializeWeight(IConnectionUnderTraining connectionUnderTraining)
        {
            _weightSetter.SetWeight(connectionUnderTraining);
            EventEmitter.Log(WeightInitialized, connectionUnderTraining.Name, connectionUnderTraining.Weight);
        }
    }
}
