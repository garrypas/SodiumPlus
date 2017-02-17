using SodiumPlus.ActivationFunctions;

namespace SodiumPlusTraining.ActivationFunctions
{
    public class SigmoidActivationFunctionTraining : ActivationFunctionTrainingBase<SigmoidActivationFunction>
    {
        public override double Derivative(double x)
        {
            return x * (1 - x);
        }
    }
}