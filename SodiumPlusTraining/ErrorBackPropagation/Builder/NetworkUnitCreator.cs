using SodiumPlusTraining.Topology;
using System.Collections.Generic;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class NetworkUnitCreator : INetworkUnitCreator
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public NetworkUnitCreator(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public IUnitActivationCreatorAndChaining ANewLayerOfHiddenUnits(int numberOfUnits)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.NetworkUnitCreator().ANewLayerOfHiddenUnits(numberOfUnits);
            return _errorBackPropagationChainOfResponsibility.UnitActivationCreatorAndChaining();
        }

        public IUnitActivationCreatorAndChaining ANewLayerOfHiddenUnitsOptimizedForTrainingPatterns(IEnumerable<TrainingPattern> trainingPatterns)
        {
            var numberOfUnits = OptimumHiddenUnitsCalculator.CalculateFor(trainingPatterns);
            return ANewLayerOfHiddenUnits(numberOfUnits);
        }

        public IOutputUnitActivationCreatorAndChaining ANewLayerOfOutputUnits(int numberOfUnits)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.NetworkUnitCreator().ANewLayerOfOutputUnits(numberOfUnits);
            return _errorBackPropagationChainOfResponsibility.OutputUnitActivationCreatorAndChaining();
        }
    }
}