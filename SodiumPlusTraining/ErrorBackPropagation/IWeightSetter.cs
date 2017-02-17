using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IWeightSetter
    {
        void SetWeight(IConnectionUnderTraining connection);
        double RangeFrom { get; set; }
        double RangeTo { get; set; }
    }
}