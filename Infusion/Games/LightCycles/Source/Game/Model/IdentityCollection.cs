namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Strongly typed collection of identities
    /// </summary>
    public class IdentityCollection : List<Identity>
    {
        /// <summary>
        /// Gets unique identifiers
        /// </summary>
        public IdentityCollection Unique
        {
            get
            {
                IdentityCollection identities = new IdentityCollection();
                foreach (Identity id in this)
                {
                    if (!identities.Contains(id))
                    {
                        identities.Add(id);
                    }
                }

                return identities;
            }
        }

        /// <summary>
        /// Gets identifiers as array of characters
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

        /// <summary>
        /// Gets identifiers as string
        /// </summary>
        public string AsString
        {
            get
            {
                string result = string.Empty;
                foreach (Identity identity in this)
                {
                    result += identity.Identifier;
                }

                return result;
            }
        }
    }
}
