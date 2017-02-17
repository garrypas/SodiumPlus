using SodiumPlus.Diagnostics;
using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    internal class ErrorValueApplier
    {
        private const string OutputError = "OutputError";
        private const string HiddenError = "HiddenError";

        public void SetOutputErrorValue(ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> outputUnit, double error)
        {
            outputUnit.UnitActivation.Properties.Error = error;
            EventEmitter.Log(OutputError, outputUnit.UnitActivation.Name, error);
        }

        public void SetHiddenErrorValue(ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> hiddenUnit, double error)
        {
            hiddenUnit.UnitActivation.Properties.Error = error;
            EventEmitter.Log(HiddenError, hiddenUnit.UnitActivation.Name, hiddenUnit.UnitActivation.Error);
        }
    }
}