using System;

namespace SodiumPlus.ActivationFunctions
{
    public class HyperbolicTangentActivationFunction : IActivationFunction
    {
        public double Activation(double x)
        {
            return Math.Tanh(x);
        }
    }
}