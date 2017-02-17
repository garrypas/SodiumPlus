using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.Topology;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace SodiumPlusUnitTests.Training.Topology
{
    public class OptimumHiddenUnitsCalculatorTests
    {
        [TestCase(5, 3)]
        [TestCase(20, 5)]
        public void OptimumHiddenUnitsCalculatorCalculatesOptimumNumberOfUnitsForTrainingPattern(int numberOfClassifications, int expectedResult) //Log2(distinctClassifications)
        {
            var trainingPatterns = new List<TrainingPattern>
            {
                new TrainingPattern(new double [] { }, new[] { 1d, 0d }),
                new TrainingPattern(new double [] { }, new[] { 1d, 0d }),
                new TrainingPattern(new double [] { }, new[] { 1d, 0d }),
                new TrainingPattern(new double [] { }, new[] { 1d, 0d }),
                new TrainingPattern(new double [] { }, new[] { 1d, 0d }),
            };

            for(var c = 1; c < numberOfClassifications; c++)
            {
                trainingPatterns.Add(new TrainingPattern(new double[] { }, new[] { 1.1d * c, 1d }));
            }

            var optimumNumberOfUnits = OptimumHiddenUnitsCalculator.CalculateFor(trainingPatterns);
            optimumNumberOfUnits.Should().Be(expectedResult);
        }
    }
}
