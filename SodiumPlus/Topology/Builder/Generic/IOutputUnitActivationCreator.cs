using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    public interface IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
    {
        /// <summary>
        /// Define the single-fold output activation to be used by all units in this layer
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationSingleFold</typeparam>
        IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> OutputUnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationSingleFold<TUnit>, new();

        /// <summary>
        /// Define the multi-fold output activation to be used
        /// </summary>
        /// <typeparam name="TUnitActivationImpl">An implementation of IUnitActivationMultiFold</typeparam>
        IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> OutputUnitActivationMultiFold<TUnitActivationImpl>()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation>, new();
    }
}