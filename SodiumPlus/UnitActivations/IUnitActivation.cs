namespace SodiumPlus.UnitActivations
{
    public interface IUnitActivation<out TUnit>
    {
        void Activate();

        /// <summary>
        /// Applies some activation on the net input and records the activation value
        /// </summary>
        void Stimulate(double netInput);

        /// <summary>
        /// Returns the last activation value
        /// </summary>
        double ActivationValue { get; }

        /// <summary>
        /// Returns the net input that was passed when the unit was stimulated
        /// </summary>
        double NetInput { get; }

        TUnit Properties { get; }

        string Name { get; }

        /// <summary>
        /// A flag that defines how this unit is to be used by the perceptron
        /// </summary>
        UnitType UnitType { get; }
    }
}