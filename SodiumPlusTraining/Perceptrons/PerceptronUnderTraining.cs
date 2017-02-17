using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Layering;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.Perceptrons
{
    public class PerceptronUnderTraining : IPerceptronUnderTraining
    {
        private readonly IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> _perceptron;

        public PerceptronUnderTraining(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> inputUnits, IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> perceptron = null)
        {
            _perceptron = perceptron ?? new Perceptron(inputUnits);
            Network = new LayeredUnitCollection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>(inputUnits).GetLayeredUnits();
        }

        public IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> GetOutputUnits()
        {
            return Network.Last();
        }


        public async Task<IEnumerable<double>> FireAsync(IEnumerable<double> inputValues)
        {
            return await _perceptron.FireAsync(inputValues);
        }

        public IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> GetRealPerceptron()
        {
            var cleanNetwork = new PerceptronFinalizer().Clean(Network);
            var realPerceptron = new Perceptron(cleanNetwork.First());
            return realPerceptron;
        }

        public void CheckTopology()
        {
            _perceptron.CheckTopology();
        }

        public IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> Network { get; set; }
    }
}
