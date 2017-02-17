namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class InventoryAndChaining : IInventoryAndChaining
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public InventoryAndChaining(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public IInventoryCreator And
        {
            get
            {
                return _errorBackPropagationChainOfResponsibility.InventoryCreator();
            }
        }
    }
}