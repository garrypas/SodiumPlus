namespace SodiumPlusTraining.ErrorBackPropagation
{

    public class WeightChangeCalculator : IWeightChangeCalculator
    {
        private readonly double _learningRate;

        public WeightChangeCalculator(double learningRate)
        {
            _learningRate = learningRate;
        }

        public double CalculateChange(double thisUnitActivation, double errorAbove)
        {
            return _learningRate * errorAbove * thisUnitActivation;
        }
    }
}