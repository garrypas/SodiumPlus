using System;

namespace SodiumPlus.ActivationFunctions
{
    /// <summary>
    /// Bipolar functions return a value between -1 and 1
    /// </summary>
    public class BipolarActivationFunction : IActivationFunction
    {
        public double Activation(double x)
        {
            return 2 / (1 + Math.Exp(-x)) - 1;
        }
    }
}