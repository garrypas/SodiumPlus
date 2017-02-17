namespace SodiumPlus.Topology
{
    /// <summary>
    /// A traversable connection between two traversable units
    /// </summary>
    public interface ITraversableConnection<out TUnit, out TConnection, out TUnitActivation>
    {
        ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation> OutputUnit { get; }

        ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation> InputUnit { get; }
        
        /// <summary>
        /// Can be used to store data associated with the connection, such as connection weight
        /// </summary>
        TConnection Properties { get; }
    }
}