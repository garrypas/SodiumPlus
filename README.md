Sodium2<sup>+</sup> - A Powerful Neural Network Library for .NET
==
Training
=
Example of training using error backpropagation to solve an XOr problem

    var trainingPatterns = new List<TrainingPattern>
    {
        new TrainingPattern(new [] { 0d, 0d }, new [] { 0d, 1d }), // 0d, 1d == false
        new TrainingPattern(new [] { 1d, 0d }, new [] { 1d, 0d }), // 0d, 1d == true
        new TrainingPattern(new [] { 0d, 1d }, new [] { 1d, 0d }), // 0d, 1d == true
        new TrainingPattern(new [] { 1d, 1d }, new [] { 0d, 1d }), // 0d, 1d == false
    };

    var perceptron = await new ErrorBackPropagationBuilder()
        .With.ANewLayerOfInputUnits(2)
        .ConnectedTo.ANewLayerOfHiddenUnits(3).With.SigmoidActivation()
        .ConnectedTo.ANewLayerOfOutputUnits(2).With.SoftmaxActivation()
        .And.NetworkErrorFunction<CrossEntropyErrorFunction>()
        .And.Bias(0.5)
        .And.LearningRate(1d)
        .And.Momentum(0d)
        .And.SlopeMultiplier(1d)
        .And.NetworkErrorFunction<DifferenceErrorFunction>()
        .And.OneHot()
        .And.SetupNetwork()
        .And.NameEverything()
        .And.ReadyForTraining()
        .TrainAsync(trainingPatterns, 0.1d, 1000);

Classification
=

Now use the perceptron to classify inputs

        var result1 = await perceptron.FireAsync(new [] { 0d, 0d });
        Console.WriteLine("(0, 0) -> (" + string.Join(",", result1) + ")");

        var result2 = await perceptron.FireAsync(new[] { 1d, 0d });
        Console.WriteLine("(1, 0) -> (" + string.Join(",", result2) + ")");

        var result3 = await perceptron.FireAsync(new[] { 0d, 1d });
        Console.WriteLine("(0, 1) -> (" + string.Join(",", result3) + ")");

        var result4 = await perceptron.FireAsync(new[] { 1d, 1d });
        Console.WriteLine("(1, 1) -> (" + string.Join(",", result4) + ")");

Saving/Loading
=

Store the perceptron for later use using the Serialization library...

        File.WriteAllText("perceptron.txt", new PerceptronSerializer().SerializeJson(perceptron));

...and then reload it...

        var perceptron = new PerceptronSerializer().DeserializeJson(File.ReadAllText("perceptron.txt"));