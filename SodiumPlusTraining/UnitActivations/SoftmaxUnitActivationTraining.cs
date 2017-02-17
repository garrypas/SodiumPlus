using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;
using SodiumPlus.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public class SoftmaxUnitActivationTraining : SoftmaxUnitActivation<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>, IUnitActivationTrainingMultiFold
    {
        private readonly ISoftmaxActivationFunctionTraining _activationFunctionTraining;

        public SoftmaxUnitActivationTraining() : this(new SoftmaxActivationFunctionTraining()) { }

        public SoftmaxUnitActivationTraining(ISoftmaxActivationFunctionTraining activationFunction)
        {
            _activationFunctionTraining = activationFunction;
        }

        public double Derivative()
        {
            var derivative = _activationFunctionTraining.Derivative(Properties.ActivationValue);
            return derivative;
        }

        public double Error { get { return Properties.Error; } }

        public IUnitActivationCreatable<IUnit> Unwrap()
        {
            return new SoftmaxUnitActivation<IUnit, IConnection, IUnitActivation<IUnit>>
            {
                Properties = Properties.Unwrap()
            };
        }
    }
}