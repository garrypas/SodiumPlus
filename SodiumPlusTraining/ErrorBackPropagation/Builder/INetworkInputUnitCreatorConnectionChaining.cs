namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface INetworkInputUnitCreatorConnectionChaining
    {
        INetworkInputUnitCreator ConnectedTo { get; }
    }
}