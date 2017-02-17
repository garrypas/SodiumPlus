using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;
using SodiumPlus.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public class ReluUnitActivationTraining : ReluUnitActivation<IUnitUnderTraining>, IUnitActivationTrainingSingleFold
    {
        private readonly ReluActivationFunctionTraining _activationFunctionTraining;

        public ReluUnitActivationTraining()
        {
            _activationFunctionTraining = new ReluActivationFunctionTraining();
        }

        public double Derivative()
        {
            var activation = Properties.ActivationValue;
            return _activationFunctionTraining.Derivative(activation);
        }

        public double Error { get { return Properties.Error; } }

        public IUnitActivationCreatable<IUnit> Unwrap()
        {
            return new ReluUnitActivation<IUnit>
            {
                Properties = Properties.Unwrap()
            };
        }
    }
}