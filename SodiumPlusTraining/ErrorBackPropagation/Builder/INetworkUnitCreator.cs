using System.Collections.Generic;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface INetworkUnitCreator
    {
        /// <summary>
        /// Creates a layer of hidden units
        /// </summary>
        /// <param name="numberOfUnits">The number of units to create</param>
        IUnitActivationCreatorAndChaining ANewLayerOfHiddenUnits(int numberOfUnits);

        /// <summary>
        /// Creates a new layer of hidden units, optimized based on the training patterns to be used
        /// </summary>
        /// <param name="trainingPatterns">The patterns that will be used during training</param>
        IUnitActivationCreatorAndChaining ANewLayerOfHiddenUnitsOptimizedForTrainingPatterns(IEnumerable<TrainingPattern> trainingPatterns);

        /// <summary>
        /// Creates a layer of output units
        /// </summary>
        /// <param name="numberOfUnits">The number of units to create</param>
        IOutputUnitActivationCreatorAndChaining ANewLayerOfOutputUnits(int numberOfUnits);
    }
}