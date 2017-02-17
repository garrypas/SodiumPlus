namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IWeightChangeCalculator
    {
        double CalculateChange(double thisUnitActivation, double errorAbove);
    }
}