using System;

namespace SodiumPlus.ActivationFunctions
{
    /// <summary>
    /// A unipolar sigmoid function that returns a value between 0 and 1
    /// </summary>
    public class SigmoidActivationFunction : IActivationFunction
    {
        public double Activation(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}