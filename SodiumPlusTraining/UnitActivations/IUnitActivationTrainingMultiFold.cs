using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.UnitActivations
{
    public interface IUnitActivationTrainingMultiFold : IUnitActivationMultiFold<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>, IUnitActivationTraining
    {
    }
}