using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class BuilderState
    {
        private INetworkErrorFunction _networkErrorFunction;

        public double LearningRate { get; internal set; }
        public double Momentum { get; internal set; }
        public double WeightRangeFrom { get; internal set; }
        public double WeightRangeTo { get; internal set; }
        public INetworkErrorFunction NetworkErrorFunction
        {
            get { return _networkErrorFunction ?? (_networkErrorFunction = new DifferenceErrorFunction()); }
            set { _networkErrorFunction = value; }
        }

        public bool OneHot { get; set; }
        public bool Batch { get; set; }

        public BuilderState()
        {
            WeightRangeFrom = 0;
            WeightRangeTo = 1d;
        }
    }
}
