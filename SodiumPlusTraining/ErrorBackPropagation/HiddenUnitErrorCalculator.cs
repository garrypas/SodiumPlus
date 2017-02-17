using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class HiddenUnitErrorCalculator : IHiddenUnitErrorCalculator
    {
        /// <summary>
        /// Calculate the hidden error δ(h) = f'(activation) * Σ(weight(ho)(n) * δ(o)(n)) for all n output units connected to this hidden unit
        /// </summary>
        /// <param name="previousLayersErrors"></param>
        /// <param name="weightsToUpperUnits"></param>
        /// <param name="activation"></param>
        /// <param name="activationDerivative"></param>
        /// <returns></returns>
        public double CalculateHiddenError(IEnumerable<double> previousLayersErrors, IEnumerable<double> weightsToUpperUnits, double activation, double activationDerivative)
        {
            return activationDerivative * TotalErrorSignalFromUnitsAbove(previousLayersErrors, weightsToUpperUnits);
        }

        private static double TotalErrorSignalFromUnitsAbove(IEnumerable<double> previousLayersErrors, IEnumerable<double> weightsToUpperUnits)
        {
            return weightsToUpperUnits
                .SelectWithAdjacent(previousLayersErrors, ErrorAboveTimesWeightToUnitAbove)
                .Sum();
        }

        private static double ErrorAboveTimesWeightToUnitAbove(double weightToUpperUnit, double unitAboveError)
        {
            return weightToUpperUnit * unitAboveError;
        }
    }

}
