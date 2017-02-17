using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class OutputUnitActivationCreator : IOutputUnitActivationCreator
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public OutputUnitActivationCreator(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public IInventoryAndChaining OutputUnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : IUnitActivationTrainingSingleFold, new()
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.OutputUnitActivationCreator().OutputUnitActivation<TUnitActivationImpl>();
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining OutputUnitActivationMultiFold<TUnitActivationImpl>() where TUnitActivationImpl : IUnitActivationTrainingMultiFold, new()
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.OutputUnitActivationCreator().OutputUnitActivationMultiFold<TUnitActivationImpl>();
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }
    }
}