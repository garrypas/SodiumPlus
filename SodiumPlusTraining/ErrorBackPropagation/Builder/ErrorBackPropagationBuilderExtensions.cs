using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public static class ErrorBackPropagationBuilderExtensions
    {
        /// <summary>
        /// All units in this layer will use a sigmoid activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining SigmoidActivation(this IUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.UnitActivation<SigmoidUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a bipolar activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining BipolarActivation(this IUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.UnitActivation<BipolarUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will an identity activation function (returns the same value inputed). Equal to Σ(weight(n) * netInput(n)) for all n units in the layer below
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining IdentityActivation(this IUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.UnitActivation<IdentityUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a hyperbolic tangent (tanh) activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining HyperbolicTangentActivation(this IUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.UnitActivation<HyperbolicTangentUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a leaky rectified linear unit (ReLU) activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining ReluActivation(this IUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.UnitActivation<ReluUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a softmax activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining SoftmaxActivation(this IUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.UnitActivationMultiFold<SoftmaxUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a sigmoid activation function
        /// </summary>
        public static IInventoryAndChaining SigmoidActivation(this IOutputUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.OutputUnitActivation<SigmoidUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a bipolar activation function
        /// </summary>
        public static IInventoryAndChaining BipolarActivation(this IOutputUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.OutputUnitActivation<BipolarUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will an identity activation function (returns the same value inputed). Equal to Σ(weight(n) * netInput(n)) for all n units in the layer below
        /// </summary>
        public static IInventoryAndChaining IdentityActivation(this IOutputUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.OutputUnitActivation<IdentityUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a hyperbolic tangent (tanh) activation function
        /// </summary>
        public static IInventoryAndChaining HyperbolicTangentActivation(this IOutputUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.OutputUnitActivation<HyperbolicTangentUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a leaky rectified linear unit (ReLU) activation function
        /// </summary>
        public static IInventoryAndChaining ReluActivation(this IOutputUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.OutputUnitActivation<ReluUnitActivationTraining>();
        }

        /// <summary>
        /// All units in this layer will use a softmax activation function
        /// </summary>
        public static IInventoryAndChaining SoftmaxActivation(this IOutputUnitActivationCreator unitActivationCreator)
        {
            return unitActivationCreator.OutputUnitActivationMultiFold<SoftmaxUnitActivationTraining>();
        }

        public static IInventoryAndChaining UseCrossEntropyErrorFunction(this IInventoryCreator unitActivationCreator)
        {
            return unitActivationCreator.NetworkErrorFunction<CrossEntropyErrorFunction>();
        }

        public static IInventoryAndChaining UseDifferenceErrorFunction(this IInventoryCreator unitActivationCreator)
        {
            return unitActivationCreator.NetworkErrorFunction<DifferenceErrorFunction>();
        }

        public static IInventoryAndChaining UseLmsErrorFunction(this IInventoryCreator unitActivationCreator)
        {
            return unitActivationCreator.NetworkErrorFunction<LmsErrorFunction>();
        }
    }
}