using System.Collections.Generic;
using System.Threading.Tasks;
using SodiumPlus.Topology;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IErrorValueBackPropagator
    {
        Task BackPropagateAllErrorsAsync(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> outputs, IEnumerable<double> idealValues);
        Task BackPropagateHiddenLayer(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> outputs);
        Task BackPropagateOutputLayer(IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> outputs, IEnumerable<double> idealValues);
    }
}