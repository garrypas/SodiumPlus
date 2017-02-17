namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class NetworkUnitCreatorConnectionChaining : INetworkUnitCreatorConnectionChaining
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public NetworkUnitCreatorConnectionChaining(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public INetworkUnitCreator ConnectedTo
        {
            get
            {
                return _errorBackPropagationChainOfResponsibility.NetworkUnitCreator();
            }
        }
    }
}