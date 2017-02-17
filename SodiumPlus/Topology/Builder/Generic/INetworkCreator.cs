using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal interface INetworkCreator<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
    {
        void Build(GenericNetworkBuilderState<TUnit, TConnection, TUnitActivation> state);
    }
}
