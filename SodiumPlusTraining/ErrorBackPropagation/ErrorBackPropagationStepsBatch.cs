using System.Collections.Generic;
using System.Linq;
using SodiumPlusTraining.Perceptrons;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class ErrorBackPropagationStepsBatch : ErrorBackPropagationStepsBase
    {
        public ErrorBackPropagationStepsBatch(IWeightSetter weightSetter, IPerceptronUnderTraining perceptronUnderTraining, double learningRate, double momentum)
            : base(weightSetter, new BatchWeightChangeApplier(), perceptronUnderTraining, learningRate, momentum)
        {
        }

        public override void CompleteRun()
        {
            Perceptron.Network.Skip(1).SelectMany(u => u).SelectMany(u => u.IncomingConnections).Enumerate(connection =>
            {
                connection.Properties.Weight += connection.Properties.UncommittedWeightChange;
                connection.Properties.UncommittedWeightChange = 0d;
            });
        }
    }
}