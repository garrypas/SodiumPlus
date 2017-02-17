using System;
using System.Collections.Generic;
using System.Linq;
using SodiumPlusTraining.ErrorBackPropagation;

namespace SodiumPlusTraining.Topology
{
    public static class OptimumHiddenUnitsCalculator
    {
        public static int CalculateFor(IEnumerable<TrainingPattern> _trainingPatterns)
        {
            var numberOfCategories = DistinctCategories(_trainingPatterns.Select(t => t.IdealActivations)).Count();
            return Convert.ToInt32(Math.Ceiling(Math.Log(numberOfCategories, 2)));
        }

        private static IEnumerable<IEnumerable<double>> DistinctCategories(IEnumerable<IEnumerable<double>> idealActivations)
        {
            return idealActivations.Where(x => idealActivations.First(i => i.SequenceEqual(x)) == x);
        }
    }

    public class CategoryComparer : IEqualityComparer<IEnumerable<double>>
    {
        public bool Equals(IEnumerable<double> left, IEnumerable<double> right)
        {
            return left.SequenceEqual(right);
        }

        public int GetHashCode(IEnumerable<double> obj)
        {
            return obj.GetHashCode();
        }
    }
}
