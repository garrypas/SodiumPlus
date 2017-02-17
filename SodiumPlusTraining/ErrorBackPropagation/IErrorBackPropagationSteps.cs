using System.Collections.Generic;
using System.Threading.Tasks;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IErrorBackPropagationSteps
    {
        Task RunInitialization();

        Task<IEnumerable<double>> FeedInputs(IEnumerable<double> inputValues);

        Task RunFeedbackPhaseAsync(IEnumerable<double> desiredValues);

        IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> GetPerceptron();
        void CompleteRun();
    }
}
