using System.Collections.Generic;
using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface IErrorBackPropagationDependencyFactory
    {
        IErrorBackPropagationSteps CreateSteps(double weightRangeFrom, double weightRangeTo, bool oneHot, IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> inputUnits, double learningRate, double momentum, bool batch);
    }
}