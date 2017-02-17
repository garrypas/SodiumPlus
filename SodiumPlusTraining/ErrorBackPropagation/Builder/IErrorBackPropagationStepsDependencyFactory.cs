using System.Collections.Generic;
using SodiumPlus.Topology;
using SodiumPlusTraining.Perceptrons;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface IErrorBackPropagationStepsDependencyFactory
    {
        IPerceptronUnderTraining CreatePerceptronUnderTraining(bool oneHot, IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> inputUnits);
        IWeightSetter CreateWeightSetter(double rangeFrom, double rangeTo);
    }
}
