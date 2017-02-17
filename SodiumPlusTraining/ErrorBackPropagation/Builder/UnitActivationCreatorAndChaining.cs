namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class UnitActivationCreatorAndChaining : IUnitActivationCreatorAndChaining
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public UnitActivationCreatorAndChaining(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public IUnitActivationCreator With
        {
            get
            {
                return _errorBackPropagationChainOfResponsibility.UnitActivationCreator();
            }
        }
    }
}