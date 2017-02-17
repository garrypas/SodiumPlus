using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Perceptrons
{
    public class OneHotPerceptron : IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>>
    {
        private readonly IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> _perceptron;

        public OneHotPerceptron(IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> wrapped)
        {
            _perceptron = wrapped;
        }

        public OneHotPerceptron(IEnumerable<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>> inputUnits)
        {
            _perceptron = new Perceptron(inputUnits);
        }

        protected OneHotPerceptron() { _perceptron = new Perceptron(); }

        public async Task<IEnumerable<double>> FireAsync(IEnumerable<double> inputValues)
        {
            var outputs = (await _perceptron.FireAsync(inputValues)).ToList();
            var oneHot = Enumerable.Repeat(0d, outputs.Count()).ToList();
            var max = outputs.Max();
            var avgMaxIndex = outputs.IndexOf(max);
            oneHot[avgMaxIndex] = 1d;
            return oneHot;
        }

        public void CheckTopology()
        {
            _perceptron.CheckTopology();
        }

        public IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>>> Network
        {
            get { return _perceptron.Network; }
            set { _perceptron.Network = value; }
        }
    }
}
