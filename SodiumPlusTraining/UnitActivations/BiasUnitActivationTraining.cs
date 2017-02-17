using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public class BiasUnitActivationTraining : BiasUnitActivation<IUnitUnderTraining>, IUnitActivationTrainingSingleFold
    {
        private readonly IdentityActivationFunctionTraining _activationFunctionTraining;

        public BiasUnitActivationTraining()
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
            return new BiasUnitActivation<IUnit>
            {
                Properties = Properties.Unwrap()
            };
        }
    }
}