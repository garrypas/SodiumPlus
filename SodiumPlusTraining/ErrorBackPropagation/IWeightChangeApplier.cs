using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IWeightChangeApplier
    {
        void ApplyWeightChange(IConnectionUnderTraining connection, double weightChange);
    }
}
