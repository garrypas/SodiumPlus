using System;

namespace SodiumPlus.Topology
{
    public class Unit : IUnit
    {
        public const double UnalteredSlopeMultiplier = 1d;
        private double _netInput;

        public Unit()
        {
            SlopeMultiplier = UnalteredSlopeMultiplier;
        }

        public double NetInput {
            get { return _netInput; }
            set
            {
                
                _netInput = value * SlopeMultiplier;
                if (double.IsNaN(_netInput) || double.IsInfinity(_netInput))
                {
                    throw new InvalidOperationException("NetInput is NaN or Infinity");
                }
            }
        }

        private double _activationValue;
        public double ActivationValue {
            get { return _activationValue; }
            set
            {
                if(double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new InvalidOperationException("Activation value is NaN or Infinity");
                }
                _activationValue = value;
            }
        }

        public string Name { get; set; }

        public IUnit Properties { get { return this; } }

        public double SlopeMultiplier { get; set; }
    }
}