using System.Threading.Tasks;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IWeightInitializer
    {
        Task PropagateWeightInitializationAsync();
    }
}
