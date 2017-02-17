using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface IUnitActivationCreator
    {
        /// <summary>
        /// Define the single-fold activation to be used by all units in this layer
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationTrainingSingleFold</typeparam>
        INetworkUnitCreatorConnectionChaining UnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : IUnitActivationTrainingSingleFold, new();

        /// <summary>
        /// Define the multi-fold activation to be used by all units in this layer
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationTrainingMultiFold</typeparam>
        INetworkUnitCreatorConnectionChaining UnitActivationMultiFold<TUnitActivationImpl>()
            where TUnitActivationImpl : IUnitActivationTrainingMultiFold, new();
    }
}