using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class ErrorBackPropagationTrainingTests
    {
        private ErrorBackPropagationTraining _errorBackPropagation;
        private Mock<IErrorBackPropagationSteps> _errorBackPropagationStepsMock;
        private List<TrainingPattern> _trainingPatterns;
        private Mock<INetworkErrorFunction> _networkErrorFunctionMock;

        [SetUp]
        public void SetUp()
        {
            _errorBackPropagationStepsMock = new Mock<IErrorBackPropagationSteps>();
            _networkErrorFunctionMock = new Mock<INetworkErrorFunction>();
            _networkErrorFunctionMock.Setup(nef => nef.Calculate(It.IsAny<IEnumerable<double>>(), It.IsAny<IEnumerable<double>>())).Returns(1d);
            _errorBackPropagation = new ErrorBackPropagationTraining(_errorBackPropagationStepsMock.Object, _networkErrorFunctionMock.Object);
            _trainingPatterns = new List<TrainingPattern>();
        }

        [Test]
        public async Task ErrorBackPropagationTrainingRunsInitialization()
        {
            await _errorBackPropagation.TrainAsync(_trainingPatterns, 10000d, 1);
            _errorBackPropagationStepsMock.Verify(steps => steps.RunInitialization(), Times.Once());
        }

        [Test]
        public async Task ErrorBackPropagationTrainingFeedsInputs()
        {
            _trainingPatterns.Add(new TrainingPattern(new[] { 1d }, new[] { 2d }));
            await _errorBackPropagation.TrainAsync(_trainingPatterns, 10000d, 1);
            _errorBackPropagationStepsMock.Verify(steps => steps.FeedInputs(It.IsAny<IEnumerable<double>>()));
        }

        [Test]
        public async Task ErrorBackPropagationTrainingStopsWhenNetErrorIsBelowErrorMax()
        {
            _networkErrorFunctionMock.Setup(nef => nef.Calculate(It.IsAny<IEnumerable<double>>(), It.IsAny<IEnumerable<double>>())).Returns(1d);
            _trainingPatterns.Add(new TrainingPattern(new[] { 1d }, new[] { 2d }));
            await _errorBackPropagation.TrainAsync(_trainingPatterns, 10000d, 100);
            _errorBackPropagationStepsMock.Verify(steps => steps.FeedInputs(It.IsAny<IEnumerable<double>>()), Times.Once());
        }

        [Test]
        public async Task ErrorBackPropagationTrainingStopsWhenMaxIterationsAreReached()
        {
            _trainingPatterns.Add(new TrainingPattern(new [] { 1d }, new [] { 2d }));
            await _errorBackPropagation.TrainAsync(_trainingPatterns, 10000d, 1);
            _errorBackPropagationStepsMock.Verify(steps => steps.FeedInputs(It.IsAny<IEnumerable<double>>()), Times.Once());
        }

        [Test]
        public void ErrorBackPropagationTrainingThrowsExceptionIfErrorMaxIsZero()
        {
            _trainingPatterns.Add(new TrainingPattern(new[] { 1d }, new[] { 2d }));
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _errorBackPropagation.TrainAsync(_trainingPatterns, 0d, 1));
            exception.Message.Should().Be(ErrorBackPropagationTraining.ErrorMaxIsZeroException);
        }

        [Test]
        public void ErrorBackPropagationTrainingThrowsExceptionIfMaxEpochsIsZero()
        {
            _trainingPatterns.Add(new TrainingPattern(new[] { 1d }, new[] { 2d }));
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _errorBackPropagation.TrainAsync(_trainingPatterns, 100d));
            exception.Message.Should().Be(ErrorBackPropagationTraining.MaxEpochsIsZeroException);
        }
    }
}
