using System.Linq;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Builder;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Topology.Builder
{
    public class StandardNetworkBuilderExtensionsTests
    {
        [Test]
        public void StandardNetworkBuilderExtensionsBuildsBipolarActivations()
        {
            var network = new StandardNetworkBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.BipolarActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.BipolarActivation()
                .And.Build().And.GetNetwork();

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<BipolarUnitActivation<IUnit>>();
        }

        [Test]
        public void StandardNetworkBuilderExtensionsBuildsSigmoidActivation()
        {
            var network = new StandardNetworkBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.SigmoidActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.SigmoidActivation()
                .And.Build().And.GetNetwork();

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<SigmoidUnitActivation<IUnit>>();
        }

        [Test]
        public void StandardNetworkBuilderExtensionsBuildsIdentityActivation()
        {
            var network = new StandardNetworkBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.IdentityActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.IdentityActivation()
                .And.Build().And.GetNetwork();

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<IdentityUnitActivation<IUnit>>();
        }

        [Test]
        public void StandardNetworkBuilderExtensionsBuildsHyperbolicTangentActivation()
        {
            var network = new StandardNetworkBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.HyperbolicTangentActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.HyperbolicTangentActivation()
                .And.Build().And.GetNetwork();

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<HyperbolicTangentUnitActivation<IUnit>>();
        }

        [Test]
        public void StandardNetworkBuilderExtensionsBuildsReluActivation()
        {
            var network = new StandardNetworkBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.ReluActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.ReluActivation()
                .And.Build().And.GetNetwork();

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<ReluUnitActivation<IUnit>>();

        }

        [Test]
        public void StandardNetworkBuilderExtensionsBuildsSoftmaxActivation()
        {
            var network = new StandardNetworkBuilder()
                .With.ANewLayerOfInputUnits(2)
                .ConnectedTo.ANewLayerOfHiddenUnits(2).With.SoftmaxActivation()
                .ConnectedTo.ANewLayerOfOutputUnits(2).With.SoftmaxActivation()
                .And.Build().And.GetNetwork();

            network.ElementAt(1).Select(u => u.UnitActivation).Should().AllBeOfType<SoftmaxUnitActivation<IUnit, IConnection, IUnitActivationCreatable<IUnit>>>();
        }
    }
}
