using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public class InputUnitActivationTraining : InputUnitActivation<IUnitUnderTraining>, IUnitActivationTrainingSingleFold
    {
        private readonly IdentityActivationFunctionTraining _activationFunctionTraining;
        public InputUnitActivationTraining()
        {
            _activationFunctionTraining = new IdentityActivationFunctionTraining();
        }

        public double Derivative()
        {
            var activation = Properties.ActivationValue;
            return _activationFunctionTraining.Derivative(activation);
        }

        public double Error { get { return Properties.Error; } }

        public IUnitActivationCreatable<IUnit> Unwrap()
        {
            return new InputUnitActivation<IUnit>
            {
                Properties = Properties.Unwrap()
            };
        }
    }
}