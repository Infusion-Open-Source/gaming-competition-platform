namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using Infusion.Gaming.LightCycles.Extensions;

    /// <summary>
    /// Identity class - carries identifier.
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        /// <param name="identifier"> The identifier. </param>
        public Identity(string identifier)
            : this(identifier[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        /// <param name="identifier"> The identifier. </param>
        public Identity(char identifier)
        {
            identifier = identifier.ToUpper();
            if (identifier < 'A' || identifier > 'Z')
            {
                throw new ArgumentOutOfRangeException("identifier");
            }

            this.Identifier = identifier;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public char Identifier { get; protected set; }

        /// <summary>
        /// Returns a System.String that represents the current System.Object.
        /// </summary>
        /// <returns>A System.String that represents the current System.Object.</returns>
        public override string ToString()
        {
            return this.Identifier.ToString();
        }

        /// <summary>
        /// Check if equals.
        /// </summary>
        /// <param name="obj"> The object to compare to. </param>
        /// <returns> The result of comparison. </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return obj.GetHashCode() == this.GetHashCode();
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns> The hash code. </returns>
        public override int GetHashCode()
        {
            return this.Identifier;
        }
    }
}