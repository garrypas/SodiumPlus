namespace SodiumPlusTraining.ActivationFunctions
{
    public interface ISoftmaxActivationFunctionTraining
    {
        double Derivative(double unitActivationValue);
    }
}