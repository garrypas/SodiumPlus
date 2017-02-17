using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class OnlineWeightChangeApplier : IWeightChangeApplier
    {
        public void ApplyWeightChange(IConnectionUnderTraining connection, double weightChange)
        {
            connection.Weight += weightChange;

            if (double.IsNegativeInfinity(connection.Weight))
            {
                connection.Weight = double.MinValue;
            }
            else if (double.IsPositiveInfinity(connection.Weight))
            {
                connection.Weight = double.MaxValue;
            }
        }
    }
}