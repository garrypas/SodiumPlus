using System.Linq;
using System.Threading.Tasks;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusUnitTests.Mocks;
using Moq;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class WeightInitializerTests
    {
        private WeightInitializer _weightInitializer;
        private NetworkBuilder _network;
        private Mock<IWeightSetter> _weightSetterMock;

        [SetUp]
        public void SetUp()
        {
            _weightSetterMock = new Mock<IWeightSetter>();
            _network = new NetworkBuilder().Setup();
            _weightInitializer = new WeightInitializer(_network.GetNetwork(), weightSetter: _weightSetterMock.Object);
        }

        [Test]
        public async Task WeightInitializerInitializerWeightsOnAllConnections()
        {
            await _weightInitializer.PropagateWeightInitializationAsync();
            var allConnections = _network.GetAllConnections();
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(0).Properties), Times.Once());
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(1).Properties), Times.Once());
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(2).Properties), Times.Once());
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(3).Properties), Times.Once());
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(4).Properties), Times.Once());
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(5).Properties), Times.Once());
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(6).Properties), Times.Once());
            _weightSetterMock.Verify(r => r.SetWeight(allConnections.ElementAt(7).Properties), Times.Once());
        }
    }
}
