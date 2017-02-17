using System;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlus.ActivationFunctions
{
    /// <summary>
    /// Softmax functions are good for classifications with multiple outputs. Ideally use this in combination with a one-hot perceptron
    /// </summary>
    public class SoftmaxActivationFunction : ISoftmaxActivationFunction
    {
        public double Activation(double netInput, IEnumerable<double> layerOutputs)
        {
            var numerator = Math.Exp(netInput);
            var denominator = layerOutputs.Sum(oj => Math.Exp(oj));
            return numerator / denominator;
        }
    }
}