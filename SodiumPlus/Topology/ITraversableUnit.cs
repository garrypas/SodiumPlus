namespace SodiumPlus.Topology
{

    public interface ITraversableUnit<TUnit, TConnection, TUnitActivation> :
        ITraversableUnitConnectable<TUnit, TConnection, TUnitActivation>,
        ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>
    {
        
    }
}