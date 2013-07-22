namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Strongly typed collection of identities
    /// </summary>
    public class IdentityCollection : List<Identity>
    {
        /// <summary>
        /// Gets identifiers
        /// </summary>
        public char[] AsCharArray
        {
            get
            {
                List<char> results = new List<char>(this.Count);
                foreach (Identity identity in this)
                {
                    results.Add(identity.Identifier);
                }

                return results.ToArray();
            }
        }
    }
}
