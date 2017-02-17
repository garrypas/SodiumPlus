using SodiumPlus.Topology.Layering;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class UnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> : IUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        private readonly GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> _genericNetworkChainOfResponsibility;

        public UnitActivationCreator(GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> genericNetworkChainOfResponsibility)
        {
            _genericNetworkChainOfResponsibility = genericNetworkChainOfResponsibility;
        }

        public INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> UnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationSingleFold<TUnit>,  new()
        {
            LayerTemplateUnitActivationCreator<TUnit, TConnection, TUnitActivation>.AddSingleFold<TUnitActivationImpl>(_genericNetworkChainOfResponsibility.State);
            return _genericNetworkChainOfResponsibility.NetworkUnitCreatorConnectionChaining();
        }

        public INetworkUnitCreatorConnectionChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> UnitActivationMultiFold<TUnitActivationImpl>() 
            where TUnitActivationImpl : TUnitActivation, IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation>, new()
        {
            LayerTemplateUnitActivationCreator<TUnit, TConnection, TUnitActivation>.AddMultiFold<TUnitActivationImpl>(_genericNetworkChainOfResponsibility.State);
            return _genericNetworkChainOfResponsibility.NetworkUnitCreatorConnectionChaining();
        }
    }
}