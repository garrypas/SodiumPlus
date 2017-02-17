using System.Collections.Generic;
using SodiumPlus.Diagnostics;
using SodiumPlusTraining.Topology;
using System.Threading.Tasks;
using System.Linq;
using SodiumPlus.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class WeightChangeBackPropagator : IWeightChangeBackPropagator
    {
        private const string WeightChange = "WeightChange";

        private readonly IWeightChangeCalculator _calculator;
        private readonly IEnumerable<IEnumerable<ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> _layeredConnections;
        private readonly double _momentum;
        private readonly IWeightChangeApplier _weightChangeApplier;

        public WeightChangeBackPropagator(IWeightChangeApplier weightChangeApplier, IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> layeredUnits, double learningRate, double momentum = 0d)
        {
            _momentum = momentum;
            _layeredConnections = layeredUnits.Skip(1).SelectMany(u => u).Select(u => u.IncomingConnections);
            _calculator = new WeightChangeCalculator(learningRate);
            _weightChangeApplier = weightChangeApplier;
        }

        public async Task UpdateAllWeightsAsync()
        {
            var tasks = _layeredConnections.Select(UpdateConnectionWeightsAsync).ToList();
            await Task.WhenAll(tasks);
        }

        private async Task UpdateConnectionWeightsAsync(IEnumerable<ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> connections)
        {
            await Task.Run(() =>
            {
                foreach (var connection in connections)
                {
                    UpdateConnectionWeight(connection);
                }
            });
        }

        private void UpdateConnectionWeight(ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> connection)
        {
            var weightChange = _calculator.CalculateChange(connection.InputUnit.ActivationValue, connection.OutputUnit.UnitActivation.Error);
            var momentumWeightChange = connection.Properties.LastWeightChange * _momentum;
            _weightChangeApplier.ApplyWeightChange(connection.Properties, weightChange + momentumWeightChange);
            EventEmitter.Log(WeightChange, connection.Properties.Name, connection.Properties.Weight);
        }
    }
}