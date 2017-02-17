using System.Collections.Generic;
using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;
using System.Linq;
using System;

namespace SodiumPlus.UnitActivations
{
    public class SoftmaxUnitActivation<TUnit, TConnection, TUnitActivation> : IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
    {
        private readonly ISoftmaxActivationFunction _activationFunction;

        private readonly Lazy<int> _layerIndex;
        public int LayerIndex
        {
            get { return _layerIndex.Value; }
        }

        public SoftmaxUnitActivation() : this(new SoftmaxActivationFunction()) { }

        public SoftmaxUnitActivation(ISoftmaxActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
            _layerIndex = new Lazy<int>(ResolveLayerIndex);
        }

        public void Activate()
        {
            //mi.eng.cam.ac.uk/~mjfg/local/4F10/lect6.pdf
            var netInput = Properties.NetInput;
            var otherUnitNetInputs = Network.ElementAt(LayerIndex).Select(h => h.NetInput);
            Properties.ActivationValue = _activationFunction.Activation(netInput, otherUnitNetInputs);
            if (double.IsInfinity(Properties.ActivationValue))
            {
            //http://stats.stackexchange.com/questions/79454/softmax-layer-in-a-neural-network
                throw new InvalidOperationException("Activation is infinity");
            }
        }

        public void Stimulate(double netInput)
        {
            if (double.IsNaN(netInput))
            {
                throw new InvalidOperationException("netInput is NaN");
            }
            Properties.NetInput = netInput;
        }

        public TUnit Properties { get; set; }

        public IEnumerable<IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>> Network
        {
            get;
            set;
        }

        private int ResolveLayerIndex()
        {
            var i = 0;
            foreach (var layer in Network)
            {
                if (layer.Any(u => u.UnitActivation.Equals(this)))
                {
                    return i;
                }
                i++;
            }

            throw new InvalidOperationException("Could not resolve which layer the unit activation is on.");
        }

        public double ActivationValue { get { return Properties.ActivationValue; } }

        public double NetInput { get { return Properties.NetInput; } }

        public string Name { get { return Properties.Name; } }

        public UnitType UnitType { get { return UnitType.NormalUnit; } }
    }
}
