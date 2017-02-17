using System.Collections.Generic;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.Perceptrons
{
    public interface IPerceptronUnderTraining : IPerceptron<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>
    {
        IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> GetOutputUnits();

        /// <summary>
        /// Converts the perceptron to a "real" perceptron once training is finished
        /// </summary>
        /// <returns>Returns the completed perceptron</returns>
        IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> GetRealPerceptron();
    }
}