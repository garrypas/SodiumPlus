using SodiumPlus.Topology.Builder.Generic;
using SodiumPlus.UnitActivations;
using System;

namespace SodiumPlus.Topology.Builder
{
    public static class StandardNetworkBuilderExtensions
    {
        /// <summary>
        /// All units in this layer will use a sigmoid activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> SigmoidActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return Activation(unitActivationCreator, typeof(SigmoidUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a bipolar activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> BipolarActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return Activation(unitActivationCreator, typeof(BipolarUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will an identity activation function (returns the same value inputed). Equal to Σ(weight(n) * netInput(n)) for all n units in the layer below
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> IdentityActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return Activation(unitActivationCreator, typeof(IdentityUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a hyperbolic tangent (tanh) activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> HyperbolicTangentActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return Activation(unitActivationCreator, typeof(HyperbolicTangentUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a leaky rectified linear unit (ReLU) activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ReluActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return Activation(unitActivationCreator, typeof(ReluUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a softmax activation function
        /// </summary>
        public static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> SoftmaxActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return ActivationMultiFold(unitActivationCreator, typeof(SoftmaxUnitActivation<TUnit, TConnection, TUnitActivation>));
        }

        private static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> Activation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator, Type unitActivationType)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            var genMethod = unitActivationCreator.GetType().GetMethod("UnitActivation").MakeGenericMethod(unitActivationType);
            return (INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>)genMethod.Invoke(unitActivationCreator, new object[] { });
        }

        private static INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ActivationMultiFold<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator, Type unitActivationType)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            var genMethod = unitActivationCreator.GetType().GetMethod("UnitActivationMultiFold").MakeGenericMethod(unitActivationType);
            return (INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>)genMethod.Invoke(unitActivationCreator, new object[] { });
        }

        /// <summary>
        /// All units in this layer will use a sigmoid activation function
        /// </summary>
        public static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> SigmoidActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return ActivationOut(unitActivationCreator, typeof(SigmoidUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a bipolar activation function
        /// </summary>
        public static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> BipolarActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return ActivationOut(unitActivationCreator, typeof(BipolarUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will an identity activation function (returns the same value inputed). Equal to Σ(weight(n) * netInput(n)) for all n units in the layer below
        /// </summary>
        public static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> IdentityActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return ActivationOut(unitActivationCreator, typeof(IdentityUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a hyperbolic tangent (tanh) activation function
        /// </summary>
        public static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> HyperbolicTangentActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return ActivationOut(unitActivationCreator, typeof(HyperbolicTangentUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a leaky rectified linear unit (ReLU) activation function
        /// </summary>
        public static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ReluActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return ActivationOut(unitActivationCreator, typeof(ReluUnitActivation<TUnit>));
        }

        /// <summary>
        /// All units in this layer will use a softmax activation function
        /// </summary>
        public static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> SoftmaxActivation<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(this IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            return ActivationOutMultiFold(unitActivationCreator, typeof(SoftmaxUnitActivation<TUnit, TConnection, TUnitActivation>));
        }

        private static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ActivationOut<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator, Type unitActivationType)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            var genMethod = unitActivationCreator.GetType().GetMethod("OutputUnitActivation").MakeGenericMethod(unitActivationType);
            return (IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>)genMethod.Invoke(unitActivationCreator, new object[] { });
        }

        private static IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ActivationOutMultiFold<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>(IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> unitActivationCreator, Type unitActivationType)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivationCreatable<TUnit>
            where TUnitImpl : TUnit, new()
            where TConnectionImpl : TConnection, new()
            where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
            where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        {
            var genMethod = unitActivationCreator.GetType().GetMethod("OutputUnitActivationMultiFold").MakeGenericMethod(unitActivationType);
            return (IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>)genMethod.Invoke(unitActivationCreator, new object[] { });
        }
    }
}