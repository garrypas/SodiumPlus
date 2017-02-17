using SodiumPlus.ActivationFunctions;

namespace SodiumPlusTraining.ActivationFunctions
{
    public class ReluActivationFunctionTraining : ActivationFunctionTrainingBase<ReluActivationFunction>
    {
        public override double Derivative(double x)
        {
            return 1;
        }
    }
}