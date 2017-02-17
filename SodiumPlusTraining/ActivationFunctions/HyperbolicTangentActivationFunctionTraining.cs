using System;
using SodiumPlus.ActivationFunctions;

namespace SodiumPlusTraining.ActivationFunctions
{
    public class HyperbolicTangentActivationFunctionTraining : ActivationFunctionTrainingBase<HyperbolicTangentActivationFunction>
    {
        public override double Derivative(double x)
        {
            return 1d / Math.Pow(Math.Cosh(x), 2d);
        }
    }
}