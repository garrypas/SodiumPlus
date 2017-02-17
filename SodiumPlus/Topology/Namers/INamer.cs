using System.Collections.Generic;

namespace SodiumPlus.Topology.Namers
{
    public interface INamer<TUnit, TConnection, TUnitTraining>
    {
        void NameItemsInNetwork(ICollection<ICollection<ITraversableUnit<TUnit, TConnection, TUnitTraining>>> network);
    }
}
