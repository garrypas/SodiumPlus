using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SodiumPlus.Diagnostics;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class ErrorBackPropagationTraining : IErrorBackPropagationTraining
    {
        public const string ErrorMaxIsZeroException = "ErrorMax should be greater than 0.";
        public const string MaxEpochsIsZeroException = "MaxEpochs should be greater than 0.";

        private readonly IErrorBackPropagationSteps _errorBackPropagationSteps;
        private readonly INetworkErrorFunction _networkErrorFunction;

        private const string Training = "Training";
        private const string TrainingIdealValues = "TrainingIdealValues";
        private const string TrainingInputs = "TrainingInputs";
        private const string NetError = "NetError";
        private const string RunningFeedback = "Running feedback";
        private const string RanFeedback = "Ran feedback";

        public ErrorBackPropagationTraining(IErrorBackPropagationSteps errorBackPropagationSteps, INetworkErrorFunction networkErrorFunction)
        {
            _errorBackPropagationSteps = errorBackPropagationSteps;
            _networkErrorFunction = networkErrorFunction;
        }


        public async Task<IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>>> TrainAsync(IEnumerable<TrainingPattern> trainingPatterns, double errorMax, int maxEpochs = 0)
        {
            CheckErrorMax(errorMax);
            CheckMaxEpochs(maxEpochs);

            await _errorBackPropagationSteps.RunInitialization();

            double netError;
            var epochs = 0;
            do
            {
                netError = 0;
                foreach (var trainingPattern in trainingPatterns)
                {
                    EventEmitter.Log(Training, TrainingIdealValues, trainingPattern.IdealActivations);
                    EventEmitter.Log(Training, TrainingInputs, trainingPattern.InputValues);
                    
                    var actualValues = await _errorBackPropagationSteps.FeedInputs(trainingPattern.InputValues);

                    netError += _networkErrorFunction.Calculate(trainingPattern.IdealActivations, actualValues);
                    EventEmitter.Log(NetError, NetError, netError);

                    EventEmitter.Log(Training, Training, RunningFeedback);
                    await _errorBackPropagationSteps.RunFeedbackPhaseAsync(trainingPattern.IdealActivations);
                    EventEmitter.Log(Training, Training, RanFeedback);
                }
                _errorBackPropagationSteps.CompleteRun();
            }
            while (Math.Abs(netError) > errorMax && (maxEpochs < 1 || ++epochs < maxEpochs));

            return _errorBackPropagationSteps.GetPerceptron();
        }


        private static void CheckErrorMax(double errorMax)
        {
            if (errorMax == 0d)
            {
                throw new ArgumentException(ErrorMaxIsZeroException);
            }
        }

        private static void CheckMaxEpochs(int maxEpochs)
        {
            if (maxEpochs < 1)
            {
                throw new ArgumentException(MaxEpochsIsZeroException);
            }
        }
    }
}
