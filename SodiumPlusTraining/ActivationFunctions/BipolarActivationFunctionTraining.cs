using System;
using SodiumPlus.ActivationFunctions;

namespace SodiumPlusTraining.ActivationFunctions
{
    public class BipolarActivationFunctionTraining : ActivationFunctionTrainingBase<BipolarActivationFunction>
    {
        public override double Derivative(double x)
        {
            return 0.5 * (1 - Math.Pow(x, 2));
        }
    }
}