namespace SodiumPlus.Topology
{
    public interface ITraversableUnitConnectable<in TUnit, in TConnection, in TUnitActivation>
    {
        void AddIncomingConnection(ITraversableConnection<TUnit, TConnection, TUnitActivation> connection);
        void AddOutgoingConnection(ITraversableConnection<TUnit, TConnection, TUnitActivation> connection);
    }
}