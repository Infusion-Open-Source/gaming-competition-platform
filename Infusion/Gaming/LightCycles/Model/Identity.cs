using Infusion.Gaming.LightCycles.Extensions;

namespace Infusion.Gaming.LightCycles.Model
{
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
        {
            this.Identifier = identifier[0].ToUpper();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        /// <param name="identifier"> The identifier. </param>
        public Identity(char identifier)
        {
            this.Identifier = identifier.ToUpper();
        }
        
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public char Identifier { get; protected set; }
        
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