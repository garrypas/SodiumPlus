using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;
using SodiumPlus.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public class HyperbolicTangentUnitActivationTraining : HyperbolicTangentUnitActivation<IUnitUnderTraining>, IUnitActivationTrainingSingleFold
    {
        private readonly HyperbolicTangentActivationFunctionTraining _activationFunctionTraining;

        public HyperbolicTangentUnitActivationTraining()
        {
            _activationFunctionTraining = new HyperbolicTangentActivationFunctionTraining();
        }

        public double Derivative()
        {
            var activation = Properties.ActivationValue;
            return _activationFunctionTraining.Derivative(activation);
        }

        public double Error { get { return Properties.Error; } }

        public IUnitActivationCreatable<IUnit> Unwrap()
        {
            return new HyperbolicTangentUnitActivation<IUnit>
            {
                Properties = Properties.Unwrap()
            };
        }
    }
}