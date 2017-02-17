using System;
using System.Collections.Generic;
using SodiumPlus.Topology;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public interface IInventoryCreator
    {
        /// <summary>
        /// Define a network-wide bias
        /// </summary>
        /// <param name="bias">The bias value. Has the effect of shifting the linear function to the left or right</param>
        IInventoryAndChaining Bias(double bias);

        /// <summary>
        /// Increase or decrease the steepness of the curve of the graph
        /// </summary>
        /// <param name="slopeMultiplier">Set to 1.0 by default</param>
        IInventoryAndChaining SlopeMultiplier(double slopeMultiplier);

        /// <summary>
        /// Learning rates of around 0.5 are generally OK, but these can be increased or decreased to speed up learning when it is slow, or slow it down when more precision is needed
        /// </summary>
        /// <param name="learningRate">The learning rate to apply</param>
        IInventoryAndChaining LearningRate(double learningRate);

        /// <summary>
        /// A momentum term can be used to increase jumps down the error surface at each iteration of the training set. Particularly useful for irregular error surfaces
        /// </summary>
        /// <param name="momentum">The momentum to apply. 0 by default (not applied)</param>
        IInventoryAndChaining Momentum(double momentum);

        /// <summary>
        /// One-hot encoding chooses one of the output unit representations of a particular classification that the input is most likely to belong to based on previous training and makes it "hot" (1). All other units are "cold" (0).
        /// </summary>
        IInventoryAndChaining UseOneHotEncoding();

        /// <summary>
        /// Use batched training. Weight updates are accumulated and applied in a batch at the end of each epoch.
        /// </summary>
        IInventoryAndChaining Batch();

        /// <summary>
        /// Specify how the network error is calculated
        /// </summary>
        /// <typeparam name="TNetworkErrorFunction">The network error function to use</typeparam>
        IInventoryAndChaining NetworkErrorFunction<TNetworkErrorFunction>() where TNetworkErrorFunction : INetworkErrorFunction, new();

        /// <summary>
        /// Sets up the network based on the configuration provided
        /// </summary>
        /// <param name="networkInterceptor">A callback function that can be used to get a handle on the network and modify it if required</param>
        ILastStepsAndChaining SetupNetwork(Action<ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>> networkInterceptor = null);
    }
}