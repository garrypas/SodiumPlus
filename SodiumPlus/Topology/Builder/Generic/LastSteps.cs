using System.Collections.Generic;
using SodiumPlus.Topology.Namers;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    internal class LastSteps<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> : ILastSteps<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        private readonly GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> _genericNetworkChainOfResponsibility;

        public LastSteps(GenericNetworkChainOfResponsibility<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> genericNetworkChainOfResponsibility)
        {
            _genericNetworkChainOfResponsibility = genericNetworkChainOfResponsibility;
        }

        public ICollection<ICollection<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> GetNetwork()
        {
            return _genericNetworkChainOfResponsibility.State.Network;
        }

        public ILastStepsAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> WithNamesAssignedToEverything(INamer<TUnit, TConnection, TUnitActivation> namer = null)
        {
            (namer ?? new Namer<TUnit, TConnection, TUnitActivation>()).NameItemsInNetwork(_genericNetworkChainOfResponsibility.State.Network);
            return _genericNetworkChainOfResponsibility.LastStepsAndChaining();
        }
    }
}