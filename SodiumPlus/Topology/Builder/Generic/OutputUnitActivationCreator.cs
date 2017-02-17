using SodiumPlus.Topology.Layering;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class OutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> : IOutputUnitActivationCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        private readonly GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> _genericNetworkChainOfResponsibility;

        public OutputUnitActivationCreator(GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> genericNetworkChainOfResponsibility)
        {
            _genericNetworkChainOfResponsibility = genericNetworkChainOfResponsibility;
        }

        public IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> OutputUnitActivation<TUnitActivationImpl>()
            where TUnitActivationImpl : TUnitActivation, IUnitActivationSingleFold<TUnit>, new()
        {
            LayerTemplateUnitActivationCreator<TUnit, TConnection, TUnitActivation>.AddSingleFold<TUnitActivationImpl>(_genericNetworkChainOfResponsibility.State);
            return _genericNetworkChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> OutputUnitActivationMultiFold<TUnitActivationImpl>() 
            where TUnitActivationImpl : TUnitActivation, IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation>, new()
        {
            LayerTemplateUnitActivationCreator<TUnit, TConnection, TUnitActivation>.AddMultiFold<TUnitActivationImpl>(_genericNetworkChainOfResponsibility.State);
            return _genericNetworkChainOfResponsibility.InventoryAndChaining();
        }
    }
}