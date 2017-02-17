using System;
using System.Collections.Generic;
using SodiumPlus.Topology;
using SodiumPlus.Topology.Namers;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    internal class InventoryCreator : IInventoryCreator
    {
        private readonly ErrorBackPropagationChainOfResponsibility _errorBackPropagationChainOfResponsibility;

        public InventoryCreator(ErrorBackPropagationChainOfResponsibility errorBackPropagationChainOfResponsibility)
        {
            _errorBackPropagationChainOfResponsibility = errorBackPropagationChainOfResponsibility;
        }

        public IInventoryAndChaining Bias(double bias)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.InventoryAndChaining().And.Bias(bias);
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining LearningRate(double learningRate)
        {
            _errorBackPropagationChainOfResponsibility.State.LearningRate = learningRate;
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining Momentum(double momentum)
        {
            _errorBackPropagationChainOfResponsibility.State.Momentum = momentum;
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining SlopeMultiplier(double slopeMultiplier)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.InventoryAndChaining().And.SlopeMultiplier(slopeMultiplier);
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining OneHot()
        {
            _errorBackPropagationChainOfResponsibility.State.OneHot = true;
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining Batch()
        {
            _errorBackPropagationChainOfResponsibility.State.Batch = true;
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public IInventoryAndChaining NetworkErrorFunction<TNetworkErrorFunction>() where TNetworkErrorFunction : INetworkErrorFunction, new()
        {
            _errorBackPropagationChainOfResponsibility.State.NetworkErrorFunction = new TNetworkErrorFunction();
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }

        public ILastStepsAndChaining SetupNetwork(Action<ICollection<ICollection<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>> networkInterceptor = null)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.InventoryCreator().Build();

            if (networkInterceptor != null)
            {
                networkInterceptor(_errorBackPropagationChainOfResponsibility.Network);
            }

            return _errorBackPropagationChainOfResponsibility.LastStepsAndChaining();
        }

        public IInventoryAndChaining NameEverything(INamer<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> namer = null)
        {
            _errorBackPropagationChainOfResponsibility.NetworkChainOfResponsibility.LastSteps().WithNamesAssignedToEverything(namer);
            return _errorBackPropagationChainOfResponsibility.InventoryAndChaining();
        }
    }
}