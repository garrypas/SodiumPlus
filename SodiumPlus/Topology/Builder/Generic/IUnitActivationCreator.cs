using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    public interface IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        /// <summary>
        /// Define the single-fold activation to be used by all units in this layer
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationSingleFold</typeparam>
        INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> UnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationSingleFold<TUnit>, new();

        /// <summary>
        /// Define the multi-fold activation to be used by all units in this layer
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationMultiFold</typeparam>
        INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> UnitActivationMultiFold<TUnitActivationImpl>()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation>, new();
    }
}