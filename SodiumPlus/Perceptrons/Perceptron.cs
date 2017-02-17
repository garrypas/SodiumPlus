using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SodiumPlus.Diagnostics;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Layering;
using SodiumPlus.UnitActivations;

namespace SodiumPlus.Perceptrons
{
    public class Perceptron : IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>>
    {
        private const string NetInput = "NetInput";
        private const string ActivationValue = "ActivationValue";

        public const string WeightsAreAllZero = "Weights are all zero. Probable misconfiguration.";
        public const string InputValuesCannotBeClampedExceptionMessage = "Cannot clamp input values. Number of input units and number of input values does not match";

        protected internal Perceptron() { }

        public Perceptron(IEnumerable<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>> inputUnits)
        {
            Network = new LayeredUnitCollection<IUnit, IConnection, IUnitActivation<IUnit>>(inputUnits).GetLayeredUnits();
        }

        private IEnumerable<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>> InputUnits
        {
            get { return Network.First().Where(i => i.UnitActivation.UnitType == UnitType.InputUnit); }
        }

        public async Task<IEnumerable<double>> FireAsync(IEnumerable<double> inputValues)
        {
            CheckInputValuesCanBeClamped(inputValues);
            ClampInputs(inputValues);

            foreach (var layeredUnit in Network)
            {
                 await Task.Run(() => FeedActivationsForward(layeredUnit));
            }
            return Network.Last().Select(u => u.ActivationValue);
        }

        public void CheckTopology()
        {
            CheckWeightsAreNotAllZero();
        }

        public IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>>> Network { get; set; }

        private void CheckInputValuesCanBeClamped(IEnumerable<double> inputValues)
        {
            if (InputUnits.Count() != inputValues.Count())
            {
                throw new ArgumentException(InputValuesCannotBeClampedExceptionMessage);
            }
        }

        private void ClampInputs(IEnumerable<double> inputValues)
        {
            var i = 0;
            foreach (var inputValue in inputValues)
            {
                InputUnits.ElementAt(i).UnitActivation.Properties.NetInput = inputValue;
                i++;
            }
        }

        private static void FeedActivationsForward(IEnumerable<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>> units)
        {
            units.Enumerate(LoadNetInput);
            units.Enumerate(ActivateUnit);
        }

        private static void LoadNetInput(ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>> traversableUnit)
        {
            traversableUnit.LoadNetInput();
            EventEmitter.Log(NetInput, traversableUnit.UnitActivation.Name, traversableUnit.UnitActivation.NetInput);
        }

        private static void ActivateUnit(ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>> traversableUnit)
        {
            traversableUnit.UnitActivation.Activate();
            EventEmitter.Log(ActivationValue, traversableUnit.UnitActivation.Name, traversableUnit.UnitActivation.ActivationValue);
        }

        private void CheckWeightsAreNotAllZero()
        {
            if (Network
                .Skip(1)
                .SelectMany(x => x)
                .SelectMany(u => u.IncomingConnections)
                .All(connection => connection.Properties.Weight == 0d))
            {
                throw new InvalidOperationException(WeightsAreAllZero);
            }
        }
    }
}
