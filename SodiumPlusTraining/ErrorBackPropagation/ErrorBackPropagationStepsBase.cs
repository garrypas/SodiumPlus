using System;
using System.Collections.Generic;
using SodiumPlus.Perceptrons;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Perceptrons;
using System.Threading.Tasks;
using SodiumPlus.Topology;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public abstract class ErrorBackPropagationStepsBase : IErrorBackPropagationSteps
    {
        private readonly IErrorValueBackPropagator _errorValueBackPropagator;
        private readonly IWeightChangeBackPropagator _weightChangeBackPropagator;
        private readonly IWeightInitializer _weightInitializer;
        protected readonly IPerceptronUnderTraining Perceptron;
        public const string LearningRateShouldNotBeZero = "LearningRate should not be 0.";

        protected ErrorBackPropagationStepsBase(IWeightSetter weightSetter, IWeightChangeApplier weightChangeApplier, IPerceptronUnderTraining perceptronUnderTraining, double learningRate, double momentum)
        {
            CheckLearningRate(learningRate);
            Perceptron = perceptronUnderTraining;
            _weightInitializer = new WeightInitializer(Perceptron.Network, weightSetter: weightSetter);
            _errorValueBackPropagator = new ErrorValueBackPropagator(Perceptron.Network);
            _weightChangeBackPropagator = new WeightChangeBackPropagator(weightChangeApplier, Perceptron.Network, learningRate, momentum);
        }

        public async Task RunInitialization()
        {
            await _weightInitializer.PropagateWeightInitializationAsync();
            Perceptron.CheckTopology();
        }

        public async Task<IEnumerable<double>> FeedInputs(IEnumerable<double> inputValues)
        {
            return await Perceptron.FireAsync(inputValues);
        }

        public async Task RunFeedbackPhaseAsync(IEnumerable<double> idealValues)
        {
            await _errorValueBackPropagator.BackPropagateAllErrorsAsync(Perceptron.GetOutputUnits(), idealValues);

            // Most authors give examples that update weights from the inputs forward, but the end result is no different when we work from outputs backwards
            // and from an implementation point of view it is simpler to start with our outputs which we needed for the error calculations
            await _weightChangeBackPropagator.UpdateAllWeightsAsync();
        }

        public IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> GetPerceptron()
        {
            return Perceptron.GetRealPerceptron();
        }

        public virtual void CompleteRun() { }

        private static void CheckLearningRate(double learningRate)
        {
            if (learningRate == 0d)
            {
                throw new ArgumentException(LearningRateShouldNotBeZero);
            }
        }
    }
}
