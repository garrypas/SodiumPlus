using SodiumPlus.Topology.Builder.Generic;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.Topology.Builder;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusTraining.ErrorBackPropagation.Builder
{
    public class ErrorBackPropagationBuilder
    {
        private readonly ErrorBackPropagationChainOfResponsibility _chainOfResponsibility;

        public ErrorBackPropagationBuilder(IGenericNetworkChainOfResponsibility<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining, UnitUnderTraining, ConnectionUnderTraining, InputUnitActivationTraining, BiasUnitActivationTraining> network = null)
        {
            _chainOfResponsibility = new ErrorBackPropagationChainOfResponsibility(network ?? new StandardNetworkBuilderTraining().ChainOfResponsibility);
        }

        public INetworkInputUnitCreator With
        {
            get
            {
                return _chainOfResponsibility.NetworkInputUnitCreator();
            }
        }
    }
}
