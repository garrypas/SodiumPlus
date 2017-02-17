using System.Collections.Generic;
using System.Threading.Tasks;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IErrorBackPropagationTraining
    {
        Task<IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>>> TrainAsync(IEnumerable<TrainingPattern> trainingPatterns, double errorMax, int maxEpochs = 0);
    }
}
