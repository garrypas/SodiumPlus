using SodiumPlus.Topology;
using SodiumPlus.Topology.Builder.Generic;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using System.Collections.Generic;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class ErrorBackPropagationChainOfResponsibility : IErrorBackPropagationChainOfResponsibility
    {
        private readonly InventoryAndChaining _inventoryAndChaining;
        private readonly InventoryCreator _inventoryCreator;
        private readonly NetworkInputUnitCreator _networkInputUnitCreator;
        private readonly NetworkInputUnitCreatorConnectionChaining _networkInputUnitCreatorConnectionChaining;
        private readonly NetworkUnitCreator _networkUnitCreator;
        private readonly NetworkUnitCreatorConnectionChaining _networkUnitCreatorConnectionChaining;
        private readonly OutputUnitActivationCreatorAndChaining _outputUnitActivationCreatorAndChaining;
        private readonly OutputUnitActivationCreator _outputUnitActivationCreator;
        private readonly UnitActivationCreatorAndChaining _unitActivationCreatorAndChaining;
        private readonly UnitActivationCreator _unitActivationCreator;
        private readonly LastStepsAndChaining _lastStepsAndChaining;
        private readonly LastSteps _lastSteps;

        public IGenericNetworkChainOfResponsibility<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining, UnitUnderTraining, ConnectionUnderTraining, InputUnitActivationTraining, BiasUnitActivationTraining> NetworkChainOfResponsibility
        {
            get;
            private set;
        }

        public ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> Network
        {
            get { return NetworkChainOfResponsibility.LastSteps().GetNetwork(); }
        }

        public ErrorBackPropagationChainOfResponsibility(IGenericNetworkChainOfResponsibility<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining, UnitUnderTraining, ConnectionUnderTraining, InputUnitActivationTraining, BiasUnitActivationTraining> network)
        {
            NetworkChainOfResponsibility = network;

            State = new BuilderState();

            _inventoryCreator = new InventoryCreator(this);
            _inventoryAndChaining = new InventoryAndChaining(this);
            _networkInputUnitCreatorConnectionChaining = new NetworkInputUnitCreatorConnectionChaining(this);
            _networkInputUnitCreator = new NetworkInputUnitCreator(this);
            _networkUnitCreatorConnectionChaining = new NetworkUnitCreatorConnectionChaining(this);
            _networkUnitCreator = new NetworkUnitCreator(this);
            _outputUnitActivationCreatorAndChaining = new OutputUnitActivationCreatorAndChaining(this);
            _outputUnitActivationCreator = new OutputUnitActivationCreator(this);
            _unitActivationCreatorAndChaining = new UnitActivationCreatorAndChaining(this);
            _unitActivationCreator = new UnitActivationCreator(this);
            _lastSteps = new LastSteps(this);
            _lastStepsAndChaining = new LastStepsAndChaining(this);
        }

        internal BuilderState State { get; set; }

        public IInventoryAndChaining InventoryAndChaining()
        {
            return _inventoryAndChaining;
        }

        public IInventoryCreator InventoryCreator()
        {
            return _inventoryCreator;
        }

        public INetworkInputUnitCreatorConnectionChaining NetworkInputUnitCreatorConnectionChaining()
        {
            return _networkInputUnitCreatorConnectionChaining;
        }

        public INetworkInputUnitCreator NetworkInputUnitCreator()
        {
            return _networkInputUnitCreator;
        }

        public INetworkUnitCreatorConnectionChaining NetworkUnitCreatorConnectionChaining()
        {
            return _networkUnitCreatorConnectionChaining;
        }

        public INetworkUnitCreator NetworkUnitCreator()
        {
            return _networkUnitCreator;
        }

        public IOutputUnitActivationCreatorAndChaining OutputUnitActivationCreatorAndChaining()
        {
            return _outputUnitActivationCreatorAndChaining;
        }

        public IOutputUnitActivationCreator OutputUnitActivationCreator()
        {
            return _outputUnitActivationCreator;
        }

        public IUnitActivationCreatorAndChaining UnitActivationCreatorAndChaining()
        {
            return _unitActivationCreatorAndChaining;
        }

        public IUnitActivationCreator UnitActivationCreator()
        {
            return _unitActivationCreator;
        }

        public ILastStepsAndChaining LastStepsAndChaining()
        {
            return _lastStepsAndChaining;
        }

        public ILastSteps LastSteps()
        {
            return _lastSteps;
        }
    }
}
