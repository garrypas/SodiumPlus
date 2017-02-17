using SodiumPlus.ActivationFunctions;

namespace SodiumPlusTraining.ActivationFunctions
{
    public interface IActivationFunctionTraining : IActivationFunction
    {
        double Derivative(double x);

        IActivationFunction GetActivationFunction();
    }
}