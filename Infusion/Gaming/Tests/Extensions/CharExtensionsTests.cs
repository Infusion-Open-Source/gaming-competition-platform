
using Infusion.Gaming.LightCycles.Extensions;
using NUnit.Framework;

namespace Infusion.Gaming.LightCycles.Tests.Extensions
{
    /// <summary>
    ///     The char extensions tests.
    /// </summary>
    [TestFixture]
    public class CharExtensionsTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     To lower checks.
        /// </summary>
        [Test]
        public void ToLowerChecks()
        {
            Assert.AreEqual('a', 'a'.ToLower());
            Assert.AreEqual('a', 'A'.ToLower());

            Assert.AreEqual('z', 'z'.ToLower());
            Assert.AreEqual('z', 'Z'.ToLower());

            Assert.AreEqual('1', '1'.ToLower());
            Assert.AreEqual('!', '!'.ToLower());
        }

        /// <summary>
        ///     To upper checks.
        /// </summary>
        [Test]
        public void ToUpperChecks()
        {
            Assert.AreEqual('A', 'a'.ToUpper());
            Assert.AreEqual('A', 'A'.ToUpper());

            Assert.AreEqual('Z', 'z'.ToUpper());
            Assert.AreEqual('Z', 'Z'.ToUpper());

            Assert.AreEqual('1', '1'.ToUpper());
            Assert.AreEqual('!', '!'.ToUpper());
        }

        #endregion
    }
}