namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class LastStepsAndChaining : ILastStepsAndChaining
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public LastStepsAndChaining(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public ILastSteps And { get { return _errorBackPropagationChainOfResponsibility.LastSteps(); } }
    }
}