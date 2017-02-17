using System.Collections.Generic;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;

namespace SodiumPlusUnitTests.Mocks
{
    public class NetworkBuilder
    {
        private bool _hasBias;
        private bool _hasUnorthodox;
        //private readonly TopologyUnderTrainingFactory _connectionCreator;

        private UnitActivationTrainingFake _input1UnitActivationTrainingFake;
        private UnitActivationTrainingFake _input2UnitActivationTrainingFake;

        private UnitActivationTrainingFake _hidden1UnitActivationTrainingFake;
        private UnitActivationTrainingFake _hidden2UnitActivationTrainingFake;

        private UnitActivationTrainingFake _output1UnitActivationTrainingFake;
        private UnitActivationTrainingFake _output2UnitActivationTrainingFake;
        private UnitActivationTrainingFake _inputUnorthodoxUnitActivationTrainingFake;

        private ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> CreateConnection(ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> input, ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> output)
        {
            return TraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateConnection<ConnectionUnderTraining>(input, output);
        }

        private static ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> CreateUnit(UnitActivationTrainingFake unitActivationTraining)
        {
            return TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, UnitActivationTrainingFake>(unitActivationTraining);
        }

        private static ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> CreateBiasUnit()
        {
            return TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, BiasUnitActivationTraining>();
        }

        public NetworkBuilder Setup()
        {
            _input1UnitActivationTrainingFake = new UnitActivationTrainingFake(UnitType.InputUnit)
            {
                IsInput = true
            };
            Input1 = CreateUnit(_input1UnitActivationTrainingFake);
            Input1.UnitActivation.Properties.Name = "Input1";

            _input2UnitActivationTrainingFake = new UnitActivationTrainingFake(UnitType.InputUnit)
            {
                IsInput = true
            };
            Input2 = CreateUnit(_input2UnitActivationTrainingFake);
            Input2.UnitActivation.Properties.Name = "Input2";

            _hidden1UnitActivationTrainingFake = new UnitActivationTrainingFake(UnitType.NormalUnit);
            Hidden1 = CreateUnit(_hidden1UnitActivationTrainingFake);
            Hidden1.UnitActivation.Properties.Name = "Hidden1";

            _hidden2UnitActivationTrainingFake = new UnitActivationTrainingFake(UnitType.NormalUnit);
            Hidden2 = CreateUnit(_hidden2UnitActivationTrainingFake);
            Hidden2.UnitActivation.Properties.Name = "Hidden2";

            _output1UnitActivationTrainingFake = new UnitActivationTrainingFake(UnitType.NormalUnit);
            Output1 = CreateUnit(_output1UnitActivationTrainingFake);
            Output1.UnitActivation.Properties.Name = "Output1";

            _output2UnitActivationTrainingFake = new UnitActivationTrainingFake(UnitType.NormalUnit);
            Output2 = CreateUnit(_output2UnitActivationTrainingFake);
            Output2.UnitActivation.Properties.Name = "Output2";

            ConnectionInput1Hidden1 = CreateConnection(Input1, Hidden1);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionInput1Hidden1";

            ConnectionInput1Hidden2 = CreateConnection(Input1, Hidden2);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionInput1Hidden2";

            ConnectionInput2Hidden1 = CreateConnection(Input2, Hidden1);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionInput2Hidden1";

            ConnectionInput2Hidden2 = CreateConnection(Input2, Hidden2);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionInput2Hidden2";

            ConnectionHidden1Output1 = CreateConnection(Hidden1, Output1);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionHidden1Output1";

            ConnectionHidden1Output2 = CreateConnection(Hidden1, Output2);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionHidden1Output2";

            ConnectionHidden2Output1 = CreateConnection(Hidden2, Output1);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionHidden2Output1";

            ConnectionHidden2Output2 = CreateConnection(Hidden2, Output2);
            ConnectionInput1Hidden1.Properties.Name = "ConnectionHidden2Output2";

            Output1Activation = 0.9d;
            Output2Activation = 0.7d;
            Hidden1Activation = 1.2d;
            Hidden2Activation = 1.4d;
            Input1Activation = 0.2d;
            Input2Activation = 0.3d;

            Input1.UnitActivation.Properties.NetInput = Input1Activation;
            Input2.UnitActivation.Properties.NetInput = Input2Activation;

            ConnectionInput1Hidden1.Properties.Weight = 0.1;
            ConnectionInput1Hidden2.Properties.Weight = 0.2;
            ConnectionInput2Hidden1.Properties.Weight = 0.3;
            ConnectionInput2Hidden2.Properties.Weight = 0.4;

            ConnectionHidden1Output1.Properties.Weight = 0.5;
            ConnectionHidden1Output2.Properties.Weight = 0.6;
            ConnectionHidden2Output1.Properties.Weight = 0.7;
            ConnectionHidden2Output2.Properties.Weight = 0.8;

            Output1Error = 1.7d;
            Output2Error = 1.6d;
            Hidden1Error = 1.2d;
            Hidden2Error = 1.3d;

            Output1Derivative = 2d;
            Output2Derivative = 3d;
            Hidden1Derivative = 0.6d;
            Hidden2Derivative = 0.7d;

            return this;
        }

        public double Hidden1Derivative
        {
            get { return _hidden1UnitActivationTrainingFake.DerivativeValue; }
            set { _hidden1UnitActivationTrainingFake.DerivativeValue = value; }
        }

        public double Hidden2Derivative
        {
            get { return _hidden2UnitActivationTrainingFake.DerivativeValue; }
            set { _hidden2UnitActivationTrainingFake.DerivativeValue = value; }
        }

        public double Output1Derivative
        {
            get { return _output1UnitActivationTrainingFake.DerivativeValue; }
            set { _output1UnitActivationTrainingFake.DerivativeValue = value; }
        }

        public double Output2Derivative
        {
            get { return _output2UnitActivationTrainingFake.DerivativeValue; }
            set { _output2UnitActivationTrainingFake.DerivativeValue = value; }
        }

        public double Hidden1Error
        {
            get { return Hidden1.UnitActivation.Error; }
            set { Hidden1.UnitActivation.Properties.Error = value; }
        }

        public double Hidden2Error
        {
            get { return Hidden2.UnitActivation.Error; }
            set { Hidden2.UnitActivation.Properties.Error = value; }
        }

        public double Output1Error
        {
            get { return Output1.UnitActivation.Error; }
            set { Output1.UnitActivation.Properties.Error = value; }
        }

        public double Output2Error
        {
            get { return Output2.UnitActivation.Error; }
            set { Output2.UnitActivation.Properties.Error = value; }
        }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionHidden2Output2 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionHidden2Output1 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionHidden1Output2 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionHidden1Output1 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionInput2Hidden2 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionInput2Hidden1 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionInput1Hidden2 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> ConnectionInput1Hidden1 { get; set; }
        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> InputUnorthodoxToOutput1 { get; set; }

        public double Output1Activation
        {
            get { return Output1.UnitActivation.Properties.ActivationValue; }
            set { Output1.UnitActivation.Properties.ActivationValue = value; }
        }

        public double Output2Activation
        {
            get { return Output2.UnitActivation.Properties.ActivationValue; }
            set { Output2.UnitActivation.Properties.ActivationValue = value; }
        }

        public double Hidden1Activation
        {
            get { return Hidden1.UnitActivation.Properties.ActivationValue; }
            set { Hidden1.UnitActivation.Properties.ActivationValue = value; }
        }

        public double Hidden2Activation
        {
            get { return Hidden2.UnitActivation.Properties.ActivationValue; }
            set { Hidden2.UnitActivation.Properties.ActivationValue = value; }
        }

        public double Input1Activation
        {
            get { return Input1.UnitActivation.Properties.ActivationValue; }
            set { Input1.UnitActivation.Properties.ActivationValue = value; }
        }

        public double Input2Activation
        {
            get { return Input2.UnitActivation.Properties.ActivationValue; }
            set { Input2.UnitActivation.Properties.ActivationValue = value; }
        }

        public double InputUnorthodoxActivation
        {
            get { return InputUnorthodox.UnitActivation.Properties.ActivationValue; }
            set { InputUnorthodox.UnitActivation.Properties.ActivationValue = value; }
        }

        public double HiddenBiasActivation
        {
            get { return HiddenBias.UnitActivation.Properties.ActivationValue; }
            set { HiddenBias.UnitActivation.Properties.ActivationValue = value; }
        }

        public double OutputBiasActivation
        {
            get { return OutputBias.UnitActivation.Properties.ActivationValue; }
            set { OutputBias.UnitActivation.Properties.ActivationValue = value; }
        }

        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> Input1 { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> Input2 { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> Hidden1 { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> Hidden2 { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> Output1 { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> Output2 { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> InputUnorthodox { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> HiddenBias { get; set; }
        public ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> OutputBias { get; set; }


        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> InputUnorthodoxToHidden1 { get; set; }

        public IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> GetOutputs()
        {
            return new List<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> { Output1, Output2 };
        }

        public IEnumerable<IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> GetNetwork()
        {
            return new List<IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>
            {
                GetInputs(),
                GetHidden(),
                GetOutputs()
            };
        }

        public IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> GetInputs()
        {
            yield return Input1;
            yield return Input2;
            if (_hasUnorthodox)
            {
                yield return InputUnorthodox;
            }
        }

        public IEnumerable<ITraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> GetHidden()
        {
            yield return Hidden1;
            yield return Hidden2;
        }

        public NetworkBuilder SetupUnorthodoxStructure()
        {
            _hasUnorthodox = true;
            _inputUnorthodoxUnitActivationTrainingFake = new UnitActivationTrainingFake(UnitType.InputUnit);
            InputUnorthodox = CreateUnit(_inputUnorthodoxUnitActivationTrainingFake);
            InputUnorthodox.UnitActivation.Properties.Name = "InputUnorthodox";

            InputUnorthodoxActivation = 0.5d;

            InputUnorthodoxToOutput1 = CreateConnection(InputUnorthodox, Output1);
            InputUnorthodoxToOutput1.Properties.Name = "InputUnorthodoxToOutput1";
            InputUnorthodoxToOutput1.Properties.Weight = 0.432d;

            InputUnorthodoxToHidden1 = CreateConnection(InputUnorthodox, Hidden1);
            InputUnorthodoxToHidden1.Properties.Name = "InputUnorthodoxToHidden1";
            InputUnorthodoxToHidden1.Properties.Weight = 0.21d;

            return this;
        }

        public NetworkBuilder AddBias()
        {
            _hasBias = true;
            HiddenBias = CreateBiasUnit();
            HiddenBiasActivation = 0.5d;

            HiddenBiasToHidden1 = CreateConnection(HiddenBias, Hidden1);
            HiddenBiasToHidden1.Properties.Name = "HiddenBiasToHidden1";
            HiddenBiasToHidden1.Properties.Weight = 0.3d;

            HiddenBiasToHidden2 = CreateConnection(HiddenBias, Hidden2);
            HiddenBiasToHidden2.Properties.Name = "HiddenBiasToHidden2";
            HiddenBiasToHidden2.Properties.Weight = 0.2d;

            OutputBias = CreateBiasUnit();

            OutputBiasActivation = 0.5d;

            OutputBiasToOutput1 = CreateConnection(OutputBias, Output1);
            OutputBiasToOutput1.Properties.Name = "OutputBiasToOutput1";
            OutputBiasToOutput1.Properties.Weight = 0.7d;

            OutputBiasToOutput2 = CreateConnection(OutputBias, Output2);
            OutputBiasToOutput2.Properties.Name = "OutputBiasToOutput2";
            OutputBiasToOutput2.Properties.Weight = 0.8d;

            return this;
        }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> OutputBiasToOutput1 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> OutputBiasToOutput2 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> HiddenBiasToHidden2 { get; set; }

        public ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining> HiddenBiasToHidden1 { get; set; }

        public IEnumerable<ITraversableConnection<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> GetAllConnections()
        {
            yield return ConnectionInput1Hidden1;
            yield return ConnectionInput1Hidden2;
            yield return ConnectionInput2Hidden1;
            yield return ConnectionInput2Hidden2;

            yield return ConnectionHidden1Output1;
            yield return ConnectionHidden1Output2;
            yield return ConnectionHidden2Output1;
            yield return ConnectionHidden2Output2;

            if (_hasBias)
            {
                yield return HiddenBiasToHidden1;
                yield return HiddenBiasToHidden2;
                yield return OutputBiasToOutput1;
                yield return OutputBiasToOutput2;
            }

            if (_hasUnorthodox)
            {
                yield return InputUnorthodoxToHidden1;
                yield return InputUnorthodoxToOutput1;
            }
        }
    }
}
