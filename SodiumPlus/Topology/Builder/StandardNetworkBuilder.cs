using SodiumPlus.Topology.Builder.Generic;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Builder
{
    public class StandardNetworkBuilder : GenericNetworkBuilder<IUnit, IConnection, IUnitActivationCreatable<IUnit>, Unit, Connection, InputUnitActivation<IUnit>, BiasUnitActivation<IUnit>> { }
}