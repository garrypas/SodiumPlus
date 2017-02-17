using System.Collections.Generic;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlusTraining.Perceptrons;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.Perceptrons
{
    public class OneHotPerceptronUnderTrainingTests
    {
        private IPerceptronUnderTraining _oneHotPerceptronUnderTraining;
        private IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> _inputUnits;

        [SetUp]
        public void SetUp()
        {
            _inputUnits = new List<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>();
            _oneHotPerceptronUnderTraining = new OneHotPerceptronUnderTraining(_inputUnits);
        }

        [Test]
        public void OneHotPerceptronUnderTrainingCallsOneHotPerceptron()
        {
            _oneHotPerceptronUnderTraining.GetRealPerceptron().Should().BeOfType<OneHotPerceptron>();
        }
    }
}
