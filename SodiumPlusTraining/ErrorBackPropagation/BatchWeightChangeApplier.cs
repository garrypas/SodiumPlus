using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class BatchWeightChangeApplier : IWeightChangeApplier
    {
        public void ApplyWeightChange(IConnectionUnderTraining connection, double weightChange)
        {
            connection.UncommittedWeightChange += weightChange;

            if (double.IsNegativeInfinity(connection.UncommittedWeightChange))
            {
                connection.UncommittedWeightChange = double.MinValue;
            }
            else if (double.IsPositiveInfinity(connection.UncommittedWeightChange))
            {
                connection.UncommittedWeightChange = double.MaxValue;
            }
        }
    }
}