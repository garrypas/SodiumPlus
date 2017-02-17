using System.Collections.Generic;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class TrainingPattern
    {
        public IEnumerable<double> InputValues { get; private set; }
        public IEnumerable<double> IdealActivations { get; private set; }

        public TrainingPattern(IEnumerable<double> inputValues, IEnumerable<double> idealActivations)
        {
            InputValues = inputValues;
            IdealActivations = idealActivations;
        }
    }
}
