using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;

namespace SodiumPlus.UnitActivations
{
    public abstract class UnitActivationBase<TUnit> : IUnitActivationSingleFold<TUnit>
        where TUnit: IUnit
    {
        protected readonly IActivationFunction ActivationFunction;

        protected UnitActivationBase(IActivationFunction activationFunction)
        {
            ActivationFunction = activationFunction;
        }

        public void Activate()
        {
            var netInput = Properties.NetInput;
            var activation = ActivationFunction.Activation(netInput);
            Properties.ActivationValue = activation;
        }

        public void Stimulate(double netInput)
        {
            Properties.NetInput = netInput;
        }

        public TUnit Properties { get; set; }

        public double ActivationValue { get { return Properties.ActivationValue; } }

        public double NetInput { get { return Properties.NetInput; } }

        public string Name { get { return Properties.Name; } }

        public abstract UnitType UnitType { get; }
    }
}