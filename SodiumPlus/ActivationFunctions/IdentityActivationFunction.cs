namespace SodiumPlus.ActivationFunctions
{
    public class IdentityActivationFunction : IActivationFunction
    {
        public double Activation(double x)
        {
            return x;
        }
    }
}