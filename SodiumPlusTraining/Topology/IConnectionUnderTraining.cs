using SodiumPlus.Topology;

namespace SodiumPlusTraining.Topology
{
    /// <summary>
    /// Defines a connection used during training
    /// </summary>
    public interface IConnectionUnderTraining : IConnection
    {
        double LastWeightChange { get; }

        double UncommittedWeightChange { get; set; }
    }
}
