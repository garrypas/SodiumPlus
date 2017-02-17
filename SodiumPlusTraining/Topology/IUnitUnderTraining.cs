using SodiumPlus.Topology;

namespace SodiumPlusTraining.Topology
{
    public interface IUnitUnderTraining : IUnit
    {
        double Error { get; set; }
        IUnit Unwrap();
    }
}
