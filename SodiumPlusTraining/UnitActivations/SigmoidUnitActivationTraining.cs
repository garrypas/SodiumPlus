using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public class SigmoidUnitActivationTraining : SigmoidUnitActivation<IUnitUnderTraining>, IUnitActivationTrainingSingleFold
    {
        private readonly SigmoidActivationFunctionTraining _activationFunctionTraining;

        public SigmoidUnitActivationTraining()
        {
            _activationFunctionTraining = new SigmoidActivationFunctionTraining();
        }

        public double Derivative()
        {
            var activation = Properties.ActivationValue;
            return _activationFunctionTraining.Derivative(activation);
        }

        public double Error { get { return Properties.Error; } }

        public IUnitActivationCreatable<IUnit> Unwrap()
        {
            return new SigmoidUnitActivation<IUnit>
            {
                Properties = Properties.Unwrap()
            };
        }
    }
}