
namespace Infusion.Gaming.LightCycles.Extensions
{
    /// <summary>
    ///     The char type extensions.
    /// </summary>
    public static class CharExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Change character to lower case.
        /// </summary>
        /// <param name="c">
        /// The character to change.
        /// </param>
        /// <returns>
        /// The changed character.
        /// </returns>
        public static char ToLower(this char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return (char)(c + ('a' - 'A'));
            }

            return c;
        }

        /// <summary>
        /// Change character to upper case.
        /// </summary>
        /// <param name="c">
        /// The character to change.
        /// </param>
        /// <returns>
        /// The changed character.
        /// </returns>
        public static char ToUpper(this char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return (char)(c + ('A' - 'a'));
            }

            return c;
        }

        #endregion
    }
}