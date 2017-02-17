namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class NetworkInputUnitCreatorConnectionChaining : INetworkInputUnitCreatorConnectionChaining
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public NetworkInputUnitCreatorConnectionChaining(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public INetworkInputUnitCreator ConnectedTo
        {
            get
            {
                return _errorBackPropagationChainOfResponsibility.NetworkInputUnitCreator();
            }
        }
    }
}