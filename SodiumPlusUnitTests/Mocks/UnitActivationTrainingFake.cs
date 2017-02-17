using System;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using Moq;

namespace SodiumPlusUnitTests.Mocks
{
    public class UnitActivationTrainingFake : IUnitActivationTrainingSingleFold
    {
        public UnitActivationTrainingFake()
        {
            Properties = new Mock<IUnitUnderTraining>().SetupAllProperties().Object;
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
            return new UnitActivationFake();
        }

        /// God bless Moq; it doesn't work with overriden properties on interfaces........
        private class UnitActivationFake : IUnitActivationCreatable<IUnit>
        {
            public UnitActivationFake()
            {
                Properties = new Unit();
            }

            public double ActivationValue
            {
                get { return Properties.ActivationValue; }
            }

            public string Name
            {
                get { return Properties.Name; }
            }

            public double NetInput
            {
                get { return Properties.NetInput; }
            }

            public IUnit Properties
            {
                get;
                set;
            }

            public UnitType UnitType
            {
                get { return UnitType.NormalUnit; }
            }

            IUnit IUnitActivation<IUnit>.Properties
            {
                get { return Properties; }
            }

            public virtual void Activate()
            {
            }

            public virtual void Stimulate(double netInput)
            {
            }
        }

        public double DerivativeValue { get; set; }

        public double Error { get { return Properties.Error; } }

        public string Name { get { return Properties.Name; } }

        public UnitType UnitType { get; set; }
    }
}
