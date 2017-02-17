namespace SodiumPlusTraining.Randomness
{
    /// <summary>
    /// Used to generate random numbers
    /// </summary>
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Generates a random double floating-point number
        /// </summary>
        /// <returns>A random number</returns>
        double GenerateRandomNumber();
    }
}