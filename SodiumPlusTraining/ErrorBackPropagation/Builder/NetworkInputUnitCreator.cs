namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class NetworkInputUnitCreator : INetworkInputUnitCreator
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public NetworkInputUnitCreator(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public INetworkUnitCreatorConnectionChaining ANewLayerOfInputUnits(int numberOfUnits)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.NetworkInputUnitCreator().ANewLayerOfInputUnits(numberOfUnits);
            return _errorBackPropagationChainOfResponsibility.NetworkUnitCreatorConnectionChaining();
        }
    }
}