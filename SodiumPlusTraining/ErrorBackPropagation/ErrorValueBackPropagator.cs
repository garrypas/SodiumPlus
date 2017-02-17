using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class ErrorValueBackPropagator : IErrorValueBackPropagator
    {
        private readonly IOutputUnitErrorCalculator _outputUnitErrorCalculator;
        private readonly IHiddenUnitErrorCalculator _hiddenUnitErrorCalculator;
        private readonly IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> _units;
        private readonly ErrorValueApplier _errorValueApplier;

        public ErrorValueBackPropagator(IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> layeredUnits, IOutputUnitErrorCalculator outputUnitErrorCalculator = null, IHiddenUnitErrorCalculator hiddenUnitErrorCalculator = null)
        {
            _units = layeredUnits.Reverse();
            _outputUnitErrorCalculator = outputUnitErrorCalculator ?? new OutputUnitErrorCalculator();
            _hiddenUnitErrorCalculator = hiddenUnitErrorCalculator ?? new HiddenUnitErrorCalculator();
            _errorValueApplier = new ErrorValueApplier();
        }

        public async Task BackPropagateAllErrorsAsync(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> outputs, IEnumerable<double> idealValues)
        {
            await BackPropagateOutputLayer(_units.First(), idealValues);

            foreach (var outputUnits in _units.Skip(1))
            {
                await BackPropagateHiddenLayer(outputUnits);
            }
        }

        public async Task BackPropagateOutputLayer(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> outputs, IEnumerable<double> idealValues)
        {
            await Task.Run(() => outputs.EnumerateWithAdjacent(idealValues, (outputUnit, ideal) =>
            {
                var error = _outputUnitErrorCalculator.CalculateOutputError(ideal, outputUnit.UnitActivation.Properties.ActivationValue, outputUnit.UnitActivation.Derivative());
                _errorValueApplier.SetOutputErrorValue(outputUnit, error);
            }));
        }

        public async Task BackPropagateHiddenLayer(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> outputs)
        {
            await Task.Run(() =>
            {
                foreach (var hiddenUnit in outputs)
                {
                    var previousLayerErrors = ErrorMatrix.GetErrorsInLayerAbove(hiddenUnit);
                    var weightsToUpperUnits = WeightMatrix.GetWeightsToLayerAbove(hiddenUnit);
                    var error = _hiddenUnitErrorCalculator.CalculateHiddenError(previousLayerErrors, weightsToUpperUnits, hiddenUnit.UnitActivation.Properties.ActivationValue, hiddenUnit.UnitActivation.Derivative());
                    _errorValueApplier.SetHiddenErrorValue(hiddenUnit, error);
                }
            });
        }
    }
}
