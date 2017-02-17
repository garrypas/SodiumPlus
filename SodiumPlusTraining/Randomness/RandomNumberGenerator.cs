using System;
using System.Security.Cryptography;

namespace SodiumPlusTraining.Randomness
{
    /// <summary>
    /// A wrapper around the .NET RNGCryptoServiceProvider
    /// </summary>
    internal class RandomNumberGenerator : IRandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        public double GenerateRandomNumber()
        {
            var data = new byte[4];
            Rand.GetBytes(data);
            return BitConverter.ToUInt32(data, 0) / 4294967295d;
        }
    }
}