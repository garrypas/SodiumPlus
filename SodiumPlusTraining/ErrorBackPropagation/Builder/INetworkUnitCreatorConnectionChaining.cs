namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface INetworkUnitCreatorConnectionChaining
    {
        INetworkUnitCreator ConnectedTo { get; }
    }
}