namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{

    public interface INetworkInputUnitCreator
    {
        /// <summary>
        /// Creates a layer of input units
        /// </summary>
        /// <param name="numberOfUnits">The number of units to create</param>
        INetworkUnitCreatorConnectionChaining ANewLayerOfInputUnits(int numberOfUnits);
    }
}