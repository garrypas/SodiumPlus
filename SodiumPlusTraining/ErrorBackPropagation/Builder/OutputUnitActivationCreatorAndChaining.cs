namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class OutputUnitActivationCreatorAndChaining : IOutputUnitActivationCreatorAndChaining
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public OutputUnitActivationCreatorAndChaining(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public IOutputUnitActivationCreator With
        {
            get
            {
                return _errorBackPropagationChainOfResponsibility.OutputUnitActivationCreator();
            }
        }
    }
}