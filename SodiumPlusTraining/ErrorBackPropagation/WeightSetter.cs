using System;
using SodiumPlusTraining.Randomness;
using SodiumPlusTraining.Topology;

namespace SodiumPlusTraining.ErrorBackPropagation
{
    public class WeightSetter : IWeightSetter
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private double _rangeFrom;
        private double _rangeTo;

        public WeightSetter(IRandomNumberGenerator randomNumberGenerator = null, double rangeFrom = 0d, double rangeTo = 1d)
        {
            _randomNumberGenerator = randomNumberGenerator ?? new RandomNumberGenerator();
            _rangeFrom = rangeFrom;
            _rangeTo = rangeTo;
        }

        public double RangeFrom
        {
            get { return _rangeFrom; }
            set
            {
                CheckRange(value, RangeTo);
                _rangeFrom = value;
            }
        }

        public double RangeTo
        {
            get { return _rangeTo; }
            set
            {
                CheckRange(_rangeFrom, value);
                _rangeTo = value;
            }
        }

        private static void CheckRange(double rangeFrom, double rangeTo)
        {
            if (rangeFrom >= rangeTo)
            {
                throw new ArgumentException("Could not create WeightInitializer. rangeFrom must be less than rangeTo");
            }
        }

        public void SetWeight(IConnectionUnderTraining connection)
        {
            var randomNumber = _randomNumberGenerator.GenerateRandomNumber();
            randomNumber = (randomNumber * Math.Abs(RangeTo - RangeFrom)) + RangeFrom;
            connection.Weight = randomNumber;
        }
    }
}