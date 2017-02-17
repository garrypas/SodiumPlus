using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class NetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> : INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        private readonly GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> _genericNetworkChainOfResponsibility;

        public NetworkUnitCreatorConnectionChaining(GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> genericNetworkChainOfResponsibility)
        {
            _genericNetworkChainOfResponsibility = genericNetworkChainOfResponsibility;
        }

        public INetworkUnitCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ConnectedTo
        {
            get
            {
                return _genericNetworkChainOfResponsibility.NetworkUnitCreator();
            }
        }
    }
}