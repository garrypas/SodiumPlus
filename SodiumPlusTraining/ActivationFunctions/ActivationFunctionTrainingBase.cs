using SodiumPlus.ActivationFunctions;

namespace SodiumPlusTraining.ActivationFunctions
{
    public abstract class ActivationFunctionTrainingBase<TActivationFunction> : IActivationFunctionTraining
        where TActivationFunction : IActivationFunction, new()
    {
        private readonly IActivationFunction _activationFunction;

        protected ActivationFunctionTrainingBase()
        {
            _activationFunction = new TActivationFunction();
        }

        public double Activation(double x)
        {
            return _activationFunction.Activation(x);
        }

        public abstract double Derivative(double x);
        public IActivationFunction GetActivationFunction()
        {
            return _activationFunction;
        }
    }
}