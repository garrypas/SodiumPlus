using SodiumPlusTraining.Perceptrons;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class ErrorBackPropagationStepsOnline : ErrorBackPropagationStepsBase
    {
        public ErrorBackPropagationStepsOnline(IWeightSetter weightSetter, IPerceptronUnderTraining perceptronUnderTraining, double learningRate, double momentum)
            : base(weightSetter, new OnlineWeightChangeApplier(), perceptronUnderTraining, learningRate, momentum)
        {
        }
    }
}
