using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    public interface INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
    {
        INetworkUnitCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ConnectedTo { get; }
    }
}