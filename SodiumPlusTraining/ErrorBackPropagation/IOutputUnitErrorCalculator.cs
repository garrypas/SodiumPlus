namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IOutputUnitErrorCalculator
    {        
        /// <summary>
        /// Calculate the output error δ(o) = (ideal - activation) * f'(activation)
        /// </summary>
        /// <param name="ideal">The ideal activation value</param>
        /// <param name="activation">The actual activation value</param>
        /// <param name="activationDerivative">The derived value, given the activation</param>
        /// <returns>The output error for this unit</returns>
        double CalculateOutputError(double ideal, double activation, double activationDerivative);
    }
}