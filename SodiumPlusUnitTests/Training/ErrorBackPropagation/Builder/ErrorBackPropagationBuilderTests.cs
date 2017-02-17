using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.ErrorBackPropagation.Builder;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation.Builder
{
    public class ErrorBackPropagationBuilderTests
    {
        private Mock<IErrorBackPropagationDependencyFactory> _errorBackPropagationDependencyFactory;
        private ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> _network;
        private const double LearningRate = 0.777d;
        private const double Bias = 0.666d;
        private const double SlopeMultiplier = 0.555d;
        private const double Momentum = 0.444d;


        [SetUp]
        public void SetUp()
        {
            _errorBackPropagationDependencyFactory = new Mock<IErrorBackPropagationDependencyFactory>();
            new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(3)
                .ConnectedTo.ANewLayerOfHiddenUnits(2)
                .With.UnitActivation<IdentityUnitActivationTraining>()
                .ConnectedTo.ANewLayerOfOutputUnits(1)
                .With.OutputUnitActivation<IdentityUnitActivationTraining>()
                .And.LearningRate(LearningRate)
                .And.Bias(Bias)
                .And.SlopeMultiplier(SlopeMultiplier)
                .And.Momentum(Momentum)
                .And.SetupNetwork(n => _network = n)
                .And.ReadyForTraining(_errorBackPropagationDependencyFactory.Object);
        }

        [Test]
        public void ErrorBackPropagationBuilderPassesOnLearningRate()
        {
            _errorBackPropagationDependencyFactory.Verify(factory => factory.CreateSteps(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<bool>(), It.IsAny<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>(), LearningRate, It.IsAny<double>(), It.IsAny<bool>()));
        }

        [Test]
        public void ErrorBackPropagationBuilderPassesOnMomentum()
        {
            _errorBackPropagationDependencyFactory.Verify(factory => factory.CreateSteps(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<bool>(), It.IsAny<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>(), It.IsAny<double>(), Momentum, It.IsAny<bool>()));
        }

        [Test]
        public void ErrorBackPropagationBuilderPassesOnBias()
        {
            var biasUnits = new List<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>();
            foreach (var layer in _network.TakeWhile(l => l.Equals(_network.Last()) == false))
            {
                var biasUnit = layer.Single(u => u.UnitActivation.UnitType == UnitType.BiasUnit);
                biasUnits.Add(biasUnit);
            }
            biasUnits.Should().HaveCount(_network.Count - 1);

            biasUnits.Select(u => u.UnitActivation.NetInput).ShouldAllBeEquivalentTo(Bias);
        }

        [Test]
        public void ErrorBackPropagationBuilderPassesOnSlopeMultiplier()
        {
            _network.SelectMany(u => u).Where(u => u.UnitActivation.UnitType == UnitType.NormalUnit).Select(u => u.UnitActivation.Properties.SlopeMultiplier).ShouldAllBeEquivalentTo(SlopeMultiplier);
        }

        [TestCase(typeof(Perceptron))]
        [TestCase(typeof(OneHotPerceptron))]
        public async Task ErrorBackPropagationCorrectPerceptronUsed(Type expectedPerceptronType)
        {
            var oneHot = typeof (OneHotPerceptron) == expectedPerceptronType;

            var chain = new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(3)
                .ConnectedTo.ANewLayerOfHiddenUnits(2)
                .With.UnitActivation<IdentityUnitActivationTraining>()
                .ConnectedTo.ANewLayerOfOutputUnits(1)
                .With.OutputUnitActivation<IdentityUnitActivationTraining>()
                .And.LearningRate(LearningRate)
                .And.Bias(Bias)
                .And.SlopeMultiplier(SlopeMultiplier)
                .And.Momentum(Momentum);

            if (oneHot)
            {
                chain = chain.And.OneHot();
            }
            var perceptron = await chain
                .And.SetupNetwork(n => _network = n)
                .And.ReadyForTraining().TrainAsync(new List<TrainingPattern>(), 999d, 1);

            perceptron.GetType().Should().Be(expectedPerceptronType);
        }
    }
}
