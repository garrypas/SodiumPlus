namespace SodiumPlus.ActivationFunctions
{
    /// <summary>
    /// Generic interface for all single-fold activation functions
    /// </summary>
    public interface IActivationFunction
    {
        /// <summary>
        /// Runs an activation function given an input x
        /// </summary>
        /// <param name="x">The input value</param>
        /// <returns>An output as a function of x</returns>
        double Activation(double x);
    }
}