using System.Threading.Tasks;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public interface IWeightChangeBackPropagator
    {
        Task UpdateAllWeightsAsync();
    }
}