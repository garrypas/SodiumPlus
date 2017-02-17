using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;
using SodiumPlus.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public class BipolarUnitActivationTraining : BipolarUnitActivation<IUnitUnderTraining>, IUnitActivationTrainingSingleFold
    {
        private readonly BipolarActivationFunctionTraining _activationFunctionTraining;

        public BipolarUnitActivationTraining()
        {
            _activationFunctionTraining = new BipolarActivationFunctionTraining();
        }

        public double Derivative()
        {
            var activation = Properties.ActivationValue;
            return _activationFunctionTraining.Derivative(activation);
        }

        public double Error { get { return Properties.Error; } }

        public IUnitActivationCreatable<IUnit> Unwrap()
        {
            return new BipolarUnitActivation<IUnit>
            {
                Properties = Properties.Unwrap()
            };
        }
    }
}