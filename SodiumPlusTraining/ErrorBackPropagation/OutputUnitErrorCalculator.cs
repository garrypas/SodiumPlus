namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class OutputUnitErrorCalculator : IOutputUnitErrorCalculator
    {
        public double CalculateOutputError(double ideal, double activation, double activationDerivative)
        {
            return (ideal - activation) * activationDerivative;
        }
    }
}
