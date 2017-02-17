using System.Collections.Generic;
using SodiumPlus.Topology.Namers;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    public interface ILastSteps<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivationCreatable<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivationCreatable<TUnit>, new()
    {
        /// <summary>
        /// Returns the created network
        /// </summary>
        ICollection<ICollection<ITraversableUnit<TUnit, TConnection, TUnitActivation>>> GetNetwork();

        /// <summary>
        /// Automatically assign names to all units and connections; useful for easily referencing elements of the network
        /// </summary>
        /// <param name="namer">Use the default naming functionality or define your own</param>
        ILastStepsAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> WithNamesAssignedToEverything(INamer<TUnit, TConnection, TUnitActivation> namer = null);
    }
}