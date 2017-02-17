using SodiumPlus.Topology;
using System;

namespace SodiumPlusTraining.Topology
{
    public class UnitUnderTraining: Unit, IUnitUnderTraining
    {
        private double _error;

        public double Error
        {
            get
            {
                return _error;
            }
            set
            {
                if(double.IsNaN(value))
                {
                    throw new InvalidOperationException("Error is NaN");
                }
                _error = value;
            }
        }

        public IUnit Unwrap()
        {
            return new Unit
            {
                ActivationValue = ActivationValue,
                Name = Name,
                NetInput = NetInput,
                SlopeMultiplier = SlopeMultiplier
            };
        }
    }
}