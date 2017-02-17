using System.Collections.Generic;

namespace SodiumPlus.ActivationFunctions
{
    public interface ISoftmaxActivationFunction
    {
        double Activation(double netInput, IEnumerable<double> layerOutputs);
    }
}