using System.Collections.Generic;
using System.Threading.Tasks;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Perceptrons
{
    /// <summary>
    /// A generic interface for multi-layer perceptrons, used for training neural networks
    /// </summary>
    public interface IPerceptron<TUnit, TConnection, TUnitActivation>
        where TUnit : IUnit
        where TConnection : IConnection
        where TUnitActivation : IUnitActivation<TUnit>
    {
        /// <summary>
        /// Fire all neurons in the network and return the results
        /// </summary>
        Task<IEnumerable<double>> FireAsync(IEnumerable<double> inputValues);

        /// <summary>
        /// Should be used to check the integrity of the network topology
        /// </summary>
        void CheckTopology();

        IEnumerable<IEnumerable<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>>> Network { get; set; }
    }
}