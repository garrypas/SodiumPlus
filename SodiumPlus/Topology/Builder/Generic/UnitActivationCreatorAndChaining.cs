using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class UnitActivationCreatorAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> : IUnitActivationCreatorAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        private readonly GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> _genericNetworkChainOfResponsibility;

        public UnitActivationCreatorAndChaining(GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> genericNetworkChainOfResponsibility)
        {
            _genericNetworkChainOfResponsibility = genericNetworkChainOfResponsibility;
        }

        public IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> With
        {
            get
            {
                return _genericNetworkChainOfResponsibility.UnitActivationCreator();
            }
        }
    }
}