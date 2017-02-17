using System;
using System.Collections.Generic;
using System.Linq;
using SodiumPlus.Perceptrons;
using SodiumPlusUnitTests.Mocks;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SodiumPlusUnitTests.Perceptrons
{
    public class PerceptronTests
    {
        private Perceptron _perceptron;
        private NetworkBuilder _network;
        private List<double> _activations;

        [SetUp]
        public void SetUp()
        {
            _network = new NetworkBuilder().Setup();
            _perceptron = new Perceptron(_network.GetInputs());
            _activations = new List<double> { 1d, 2d };
        }

        [Test]
        public void PerceptronThrowsArgumentExceptionWhenNumberOfInputUnitsAndInputValuesAreDifferent()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _perceptron.FireAsync(new[] { 1d }));
        }

        [Test]
        public async Task PerceptronClampsInputs()
        {
            await _perceptron.FireAsync(_activations);
            var inputs = _network.GetInputs();

            inputs.ElementAt(0).ActivationValue.Should().Be(_activations[0], because: "Input unit value is fed forward without modification.");
            inputs.ElementAt(1).ActivationValue.Should().Be(_activations[1], because: "Input unit value is fed forward without modification.");
        }

        [Test]
        public async Task PerceptronPropagatesActivationsForwards()
        {
            var outputs = await _perceptron.FireAsync(_activations);
            var hiddenUnits = _network.GetHidden();

            var expectedHidden1Activation = _network.ConnectionInput1Hidden1.Properties.Weight * _network.Input1Activation + _network.ConnectionInput2Hidden1.Properties.Weight * _network.Input2Activation;
            var expectedHidden2Activation = _network.ConnectionInput1Hidden2.Properties.Weight * _network.Input1Activation + _network.ConnectionInput2Hidden2.Properties.Weight * _network.Input2Activation;

            hiddenUnits.ElementAt(0).UnitActivation.Properties.ActivationValue.Should().BeApproximately(expectedHidden1Activation, 0.00000001d);
            hiddenUnits.ElementAt(1).UnitActivation.Properties.ActivationValue.Should().BeApproximately(expectedHidden2Activation, 0.00000001d);

            var expectedOutput1 = _network.ConnectionHidden1Output1.Properties.Weight * expectedHidden1Activation + _network.ConnectionHidden2Output1.Properties.Weight * expectedHidden2Activation;
            var expectedOutput2 = _network.ConnectionHidden1Output2.Properties.Weight * expectedHidden1Activation + _network.ConnectionHidden2Output2.Properties.Weight * expectedHidden2Activation;

            expectedOutput1.Should().BeGreaterThan(0d, because: "Otherwise activations or weights are not being set correctly somewhere");
            expectedOutput2.Should().BeGreaterThan(0d, because: "Otherwise activations or weights are not being set correctly somewhere");

            outputs.ElementAt(0).Should().BeApproximately(expectedOutput1, 0.00000001d);
            outputs.ElementAt(1).Should().BeApproximately(expectedOutput2, 0.00000001d);
        }

        [Test]
        public async Task PerceptronWithUnorthodoxTopologyPropagatesActivationsForwards()
        {
            _network.SetupUnorthodoxStructure();
            var inputs = _network.GetInputs();
            _perceptron = new Perceptron(inputs);

            _activations.Add(0.34d);
            var outputs = await _perceptron.FireAsync(_activations);
            var hiddenUnits = _network.GetHidden();

            var expectedHidden1Activation = _network.ConnectionInput1Hidden1.Properties.Weight * _network.Input1Activation + _network.ConnectionInput2Hidden1.Properties.Weight * _network.Input2Activation
                                            + _network.InputUnorthodoxToHidden1.Properties.Weight * _network.InputUnorthodoxActivation;
            var expectedHidden2Activation = _network.ConnectionInput1Hidden2.Properties.Weight * _network.Input1Activation + _network.ConnectionInput2Hidden2.Properties.Weight * _network.Input2Activation;

            hiddenUnits.ElementAt(0).UnitActivation.Properties.ActivationValue.Should().BeApproximately(expectedHidden1Activation, 0.00000001d);
            hiddenUnits.ElementAt(1).UnitActivation.Properties.ActivationValue.Should().BeApproximately(expectedHidden2Activation, 0.00000001d);

            var expectedOutput1 = _network.ConnectionHidden1Output1.Properties.Weight * expectedHidden1Activation + _network.ConnectionHidden2Output1.Properties.Weight * expectedHidden2Activation
                                  + _network.InputUnorthodoxToOutput1.Properties.Weight * _network.InputUnorthodoxActivation;
            var expectedOutput2 = _network.ConnectionHidden1Output2.Properties.Weight * expectedHidden1Activation + _network.ConnectionHidden2Output2.Properties.Weight * expectedHidden2Activation;

            expectedOutput1.Should().BeGreaterThan(0d, because: "Otherwise activations or weights are not being set correctly somewhere");
            expectedOutput2.Should().BeGreaterThan(0d, because: "Otherwise activations or weights are not being set correctly somewhere");

            outputs.ElementAt(0).Should().BeApproximately(expectedOutput1, 0.00000001d);
            outputs.ElementAt(1).Should().BeApproximately(expectedOutput2, 0.00000001d);
        }

        [Test]
        public void PerceptronThrowsErrorWhenNumberOfInputValuesDontMatchNumberOfInputUnits()
        {
            _activations.Add(0.34d);
            
            //Note: NullReferenceException means exception wasn't thrown
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _perceptron.FireAsync(_activations));
            exception.Message.Should().Be(Perceptron.InputValuesCannotBeClampedExceptionMessage);
        }

        [Test]
        public void PerceptronThrowsErrorWhenWeightsAreAllZero()
        {
            _network.GetAllConnections().Enumerate(connection => connection.Properties.Weight = 0d);
            
            //Note: NullReferenceException means exception wasn't thrown
            var exception = Assert.Throws<InvalidOperationException>(() => _perceptron.CheckTopology());
            exception.Message.Should().Be(Perceptron.WeightsAreAllZero);
        }
    }
}
