using System.Collections.Generic;
using System.Linq;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Namers;
using SodiumPlusTraining.Perceptrons;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class LastSteps : ILastSteps
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public LastSteps(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public ILastStepsAndChaining NameEverything(INamer<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> namer = null)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.LastSteps().WithNamesAssignedToEverything(namer);
            return _errorBackPropagationChainOfResponsibility.LastStepsAndChaining();
        }

        public IErrorBackPropagationTraining ReadyForTraining(IErrorBackPropagationDependencyFactory errorBackPropagationDependencyFactory = null)
        {
            var inputUnits = _errorBackPropagationChainOfResponsibility.Network.First();
            var learningRate = _errorBackPropagationChainOfResponsibility.State.LearningRate;
            var rangeFrom = _errorBackPropagationChainOfResponsibility.State.WeightRangeFrom;
            var rangeTo = _errorBackPropagationChainOfResponsibility.State.WeightRangeTo;
            var momentum = _errorBackPropagationChainOfResponsibility.State.Momentum;
            var oneHot = _errorBackPropagationChainOfResponsibility.State.OneHot;
            var batch = _errorBackPropagationChainOfResponsibility.State.Batch;

            errorBackPropagationDependencyFactory = errorBackPropagationDependencyFactory ?? new ErrorBackPropagationDependencyFactory(new ErrorBackPropagationStepsDependencyFactory());
            var errorBackPropagationSteps = errorBackPropagationDependencyFactory.CreateSteps(rangeFrom, rangeTo, oneHot, inputUnits, learningRate, momentum, batch);
            return new ErrorBackPropagationTraining(errorBackPropagationSteps, _errorBackPropagationChainOfResponsibility.State.NetworkErrorFunction);
        }
    }

    public class ErrorBackPropagationDependencyFactory : IErrorBackPropagationDependencyFactory
    {
        private readonly IErrorBackPropagationStepsDependencyFactory _errorBackPropagationStepsDependencyFactory;

        public ErrorBackPropagationDependencyFactory(IErrorBackPropagationStepsDependencyFactory errorBackPropagationStepsDependencyFactory)
        {
            _errorBackPropagationStepsDependencyFactory = errorBackPropagationStepsDependencyFactory;
        }

        public IErrorBackPropagationSteps CreateSteps(double weightRangeFrom, double weightRangeTo, bool oneHot, IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> inputUnits, double learningRate, double momentum, bool batch)
        {
            var weightSetter = _errorBackPropagationStepsDependencyFactory.CreateWeightSetter(weightRangeFrom, weightRangeTo);
            var perceptronUnderTraining = _errorBackPropagationStepsDependencyFactory.CreatePerceptronUnderTraining(oneHot, inputUnits);
            if (batch)
            {
                return new ErrorBackPropagationStepsBatch(weightSetter, perceptronUnderTraining, learningRate, momentum);
            }
            return new ErrorBackPropagationStepsOnline(weightSetter, perceptronUnderTraining, learningRate, momentum);
        }
    }

    public class ErrorBackPropagationStepsDependencyFactory : IErrorBackPropagationStepsDependencyFactory
    {

        public IWeightSetter CreateWeightSetter(double rangeFrom, double rangeTo)
        {
            return new WeightSetter(rangeFrom: rangeFrom, rangeTo: rangeTo);
        }

        public IPerceptronUnderTraining CreatePerceptronUnderTraining(bool oneHot, IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> inputUnits)
        {
            if (oneHot)
            {
                return new OneHotPerceptronUnderTraining(inputUnits);
            }
            return new PerceptronUnderTraining(inputUnits);
        }
    }
}