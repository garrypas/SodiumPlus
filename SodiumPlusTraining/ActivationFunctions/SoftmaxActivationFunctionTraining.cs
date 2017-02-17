namespace SodiumPlusTraining.ActivationFunctions
{
    public class SoftmaxActivationFunctionTraining : ISoftmaxActivationFunctionTraining
    {
        public double Derivative(double unitActivationValue)
        {
           // See Andrew Senior, Google NYC 12 Dec 2013
           return unitActivationValue * (1d - unitActivationValue);
        }
    }
}