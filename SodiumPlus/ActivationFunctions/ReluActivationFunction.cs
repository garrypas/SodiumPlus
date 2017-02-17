namespace SodiumPlus.ActivationFunctions
{
    /// <summary>
    /// Bipolar functions return a value between -1 and 1
    /// </summary>
    public class ReluActivationFunction : IActivationFunction
    {
        public double Activation(double x)
        {
            return x < 0 ? 0.0000000001 : x;
        }
    }
}