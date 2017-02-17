using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    public interface IUnitActivationCreatorAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
    {
        IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> With { get; }
    }
}