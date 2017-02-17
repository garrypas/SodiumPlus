using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder.Generic
{
    public interface INetworkUnitCreator<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
        where TUnitImpl : TUnit, new()
        where TConnectionImpl : TConnection, new()
        where TBiasUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
        where TInputUnitImpl : TUnitActivation, IUnitActivation<TUnit>, new()
    {
        /// <summary>
        /// Creates a layer of hidden units
        /// </summary>
        /// <param name="numberOfUnits">The number of units to create</param>
        IUnitActivationCreatorAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ANewLayerOfHiddenUnits(int numberOfUnits);

        /// <summary>
        /// Creates a layer of output units
        /// </summary>
        /// <param name="numberOfUnits">The number of units to create</param>
        IOutputUnitActivationCreatorAndChaining<TUnit, TConnection, TUnitActivation, TUnitImpl, TConnectionImpl, TInputUnitImpl, TBiasUnitImpl> ANewLayerOfOutputUnits(int numberOfUnits);
    }
}