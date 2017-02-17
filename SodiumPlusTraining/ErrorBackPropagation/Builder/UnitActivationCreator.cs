using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class UnitActivationCreator : IUnitActivationCreator
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public UnitActivationCreator(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public INetworkUnitCreatorConnectionChaining UnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : IUnitActivationTrainingSingleFold, new()
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.UnitActivationCreator().UnitActivation<TUnitActivationImpl>();
            return _errorBackPropagationChainOfResponsibility.NetworkUnitCreatorConnectionChaining();
        }

        public INetworkUnitCreatorConnectionChaining UnitActivationMultiFold<TUnitActivationImpl>()
            where TUnitActivationImpl : IUnitActivationTrainingMultiFold, new()
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.UnitActivationCreator().UnitActivationMultiFold<TUnitActivationImpl>();
            return _errorBackPropagationChainOfResponsibility.NetworkUnitCreatorConnectionChaining();
        }
    }
}