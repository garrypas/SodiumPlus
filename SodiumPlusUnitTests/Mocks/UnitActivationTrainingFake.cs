using System;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusUnitTests.Mocks
{
    public class UnitActivationTrainingFake : IUnitActivationTrainingSingleFold
    {
        public UnitActivationTrainingFake()
        {

        }

        public UnitActivationTrainingFake(UnitType unitType)
        {
            UnitType = unitType;
        }

        public void Activate()
        {
            Properties.ActivationValue = NetInput;
        }

        public bool IsInput { get; set; }

        public void Stimulate(double netInput)
        {
            if (!IsInput)
            {
                Properties.NetInput = netInput;
            }
        }

        public double ActivationValue
        {
            get
            {
                return Properties.ActivationValue;
            }
        }
        public double NetInput
        {
            get
            {
                return Properties.NetInput;
            }
        }

        public IUnitUnderTraining Properties { get; set; }
        public double Derivative()
        {
            return DerivativeValue;
        }

        public IUnitActivationCreatable<IUnit> Unwrap()
        {
            throw new NotImplementedException();
        }

        public double DerivativeValue { get; set; }

        public double Error { get { return Properties.Error; } }

        public string Name { get { return Properties.Name; } }

        public UnitType UnitType { get; set; }
    }
}
