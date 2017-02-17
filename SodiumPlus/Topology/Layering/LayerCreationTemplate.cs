using System;
using System.Collections.Generic;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Topology.Layering
{
    internal class LayerCreationTemplate<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        public LayerCreationTemplate(int numberOfUnitsToAdd)
        {
            NumberOfUnitsToAdd = numberOfUnitsToAdd;
        }

        public int NumberOfUnitsToAdd { get; private set; }

        public ICollection<ITraversableUnit<TUnit, TConnection, TUnitActivation>> Layer
        {
            get
            {
                return new List<ITraversableUnit<TUnit, TConnection, TUnitActivation>>();
            }
        }

        public Func<TUnitActivation> CreateUnitActivation { get; set; }
    }
}
