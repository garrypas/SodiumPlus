using System.Collections.Generic;
using System.Threading.Tasks;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.Perceptrons
{
    /// <summary>
    /// Applies one-hot encoding on outputs, in which one output is 1 and the rest are 0, where each output unit represents a classification
    /// </summary>
    public class OneHotPerceptronUnderTraining : IPerceptronUnderTraining
    {
        private readonly IPerceptronUnderTraining _perceptronUnderTraining;

        public OneHotPerceptronUnderTraining(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> inputUnits)
        {
            _perceptronUnderTraining = new PerceptronUnderTraining(inputUnits, new OneHotPerceptron(inputUnits));
        }

        public IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> GetOutputUnits()
        {
            return _perceptronUnderTraining.GetOutputUnits();
        }

        public async Task<IEnumerable<double>> FireAsync(IEnumerable<double> inputValues)
        {
            return await _perceptronUnderTraining.FireAsync(inputValues);
        }

        public IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> GetRealPerceptron()
        {
            return new OneHotPerceptron(_perceptronUnderTraining.GetRealPerceptron());
        }

        public void CheckTopology()
        {
            _perceptronUnderTraining.CheckTopology();
        }

        public IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> Network
        {
            get { return _perceptronUnderTraining.Network; }
            set { _perceptronUnderTraining.Network = value; }
        }
    }
}
