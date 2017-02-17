using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface IOutputUnitActivationCreator
    {
        /// <summary>
        /// Define the single-fold output activation to be used by all units in this layer
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationTrainingSingleFold</typeparam>
        IInventoryAndChaining OutputUnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : IUnitActivationTrainingSingleFold, new();

        /// <summary>
        /// Define the multi-fold output activation to be used
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationTrainingMultiFold</typeparam>
        IInventoryAndChaining OutputUnitActivationMultiFold<TUnitActivationImpl>()
            where TUnitActivationImpl : IUnitActivationTrainingMultiFold, new();
    }
}