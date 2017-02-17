using SodiumPlus.Topology.Namers;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface ILastSteps
    {
        /// <summary>
        /// Automatically assign names to all units and connections; useful for easily referencing elements of the network
        /// </summary>
        /// <param name="namer">Use the default naming functionality or define your own</param>
        ILastStepsAndChaining NameEverything(INamer<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> namer = null);

        /// <summary>
        /// At this point the network will be setup and is ready for training
        /// </summary>
        /// <param name="errorBackPropagationDependencyFactory">Use the default error back propagation dependencies (recommended) or fully customize</param>
        IErrorBackPropagationTraining ReadyForTraining(IErrorBackPropagationDependencyFactory errorBackPropagationDependencyFactory = null);
    }
}