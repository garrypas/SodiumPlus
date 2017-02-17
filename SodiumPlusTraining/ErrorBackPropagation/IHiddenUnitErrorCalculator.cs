using System.Collections.Generic;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IHiddenUnitErrorCalculator
    {
        double CalculateHiddenError(IEnumerable<double> previousLayersErrors, IEnumerable<double> weightsToUpperUnits, double activation, double activationDerivative);
    }
}