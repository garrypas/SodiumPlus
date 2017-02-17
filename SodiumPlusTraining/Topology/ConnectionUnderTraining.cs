using System.Diagnostics;
using SodiumPlus.Topology;
using System;

namespace SodiumPlusTraining.Topology
{
    [DebuggerDisplay("Weight = {Weight}, Name = {Name}, LastWeightChange = {LastWeightChange}")]
    public class ConnectionUnderTraining : Connection, IConnectionUnderTraining
    {
        public override double Weight
        {
            get
            {
                return base.Weight;
            }
            set
            {
                if (double.IsNaN(value))
                {
                    throw new InvalidOperationException("Weight is NaN");
                }
                LastWeightChange = value - base.Weight;
                base.Weight = value;
            }
        }

        public double LastWeightChange
        {
            get;
            private set;
        }

        public double UncommittedWeightChange { get; set; }
    }
}
