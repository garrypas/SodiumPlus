using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class InventoryCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> : IInventoryCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        private readonly GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> _genericNetworkChainOfResponsibility;
        private readonly INetworkCreator<TUnit, TConnection, TUnitActivation> _networkCreator;

        public InventoryCreator(GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> genericNetworkChainOfResponsibility, INetworkCreator<TUnit, TConnection, TUnitActivation> networkCreator)
        {
            _genericNetworkChainOfResponsibility = genericNetworkChainOfResponsibility;
            _networkCreator = networkCreator;
        }

        public IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> Bias(double bias)
        {
            _genericNetworkChainOfResponsibility.State.Bias = bias;
            return _genericNetworkChainOfResponsibility.InventoryAndChaining();
        }

        public ILastStepsAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> Build()
        {
            _networkCreator.Build(_genericNetworkChainOfResponsibility.State);
            return _genericNetworkChainOfResponsibility.LastStepsAndChaining();
        }

        public IInventoryAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> SlopeMultiplier(double slopeMultiplier)
        {
            _genericNetworkChainOfResponsibility.State.SlopeMultiplier = slopeMultiplier;
            return _genericNetworkChainOfResponsibility.InventoryAndChaining();
        }
    }
}