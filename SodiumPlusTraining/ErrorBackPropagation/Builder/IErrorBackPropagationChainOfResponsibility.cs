namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface IErrorBackPropagationChainOfResponsibility
    {
        IInventoryAndChaining InventoryAndChaining();
        IInventoryCreator InventoryCreator();
        INetworkInputUnitCreator NetworkInputUnitCreator();
        INetworkInputUnitCreatorConnectionChaining NetworkInputUnitCreatorConnectionChaining();
        INetworkUnitCreator NetworkUnitCreator();
        INetworkUnitCreatorConnectionChaining NetworkUnitCreatorConnectionChaining();
        IOutputUnitActivationCreator OutputUnitActivationCreator();
        IOutputUnitActivationCreatorAndChaining OutputUnitActivationCreatorAndChaining();
        IUnitActivationCreator UnitActivationCreator();
        IUnitActivationCreatorAndChaining UnitActivationCreatorAndChaining();
        ILastStepsAndChaining LastStepsAndChaining();
        ILastSteps LastSteps();
    }
}