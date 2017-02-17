using System.Linq;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Builder;
using FluentAssertions;
using NUnit.Framework;
using SodiumPlusTraining.ErrorBackPropagation.Builder;
using System.Collections.Generic;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
namespace SodiumPlusUnitTests.Topology.Builder
{
    using TNetwork = ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>;
    public class ErrorBackPropagationBuilderExtensionsTests
    {
        [Test]
        public void ErrorBackPropagationBuilderExtensionsBuildsBipolarActivations()
        {
            TNetwork network = null;
            new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.BipolarActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.BipolarActivation()
                .And.SetupNetwork(n => network = n);

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<BipolarUnitActivationTraining>();
        }

        [Test]
        public void ErrorBackPropagationBuilderExtensionsBuildsSigmoidActivation()
        {
            TNetwork network = null;
            new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.SigmoidActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.SigmoidActivation()
                .And.SetupNetwork(n => network = n);

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<SigmoidUnitActivationTraining>();
        }

        [Test]
        public void ErrorBackPropagationBuilderExtensionsBuildsIdentityActivation()
        {
            TNetwork network = null;
            new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.IdentityActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.IdentityActivation()
                .And.SetupNetwork(n => network = n);

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<IdentityUnitActivationTraining>();
        }

        [Test]
        public void ErrorBackPropagationBuilderExtensionsBuildsHyperbolicTangentActivation()
        {
            TNetwork network = null;
            new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.HyperbolicTangentActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.HyperbolicTangentActivation()
                .And.SetupNetwork(n => network = n);

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<HyperbolicTangentUnitActivationTraining>();
        }

        [Test]
        public void ErrorBackPropagationBuilderExtensionsBuildsReluActivation()
        {
            TNetwork network = null;
            new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.ReluActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.ReluActivation()
                .And.SetupNetwork(n => network = n);

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<ReluUnitActivationTraining>();

        }

        [Test]
        public void ErrorBackPropagationBuilderExtensionsBuildsSoftmaxActivation()
        {
            TNetwork network = null;
            new ErrorBackPropagationBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.SoftmaxActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.SoftmaxActivation()
                .And.SetupNetwork(n => network = n);

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<SoftmaxUnitActivationTraining>();
        }
    }
}
