namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using System;

    /// <summary>
    /// Game state reader from text
    /// </summary>
    public class TextGameStateReader : IGameStateReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextGameStateReader"/> class.
        /// </summary>
        /// <param name="textReader">text writer to be used</param>
        public TextGameStateReader(System.IO.TextReader textReader)
        {
            if (textReader == null)
            {
                throw new ArgumentNullException("textReader");
            }

            this.Reader = textReader;
        }

        /// <summary>
        /// Gets or sets text writer
        /// </summary>
        public System.IO.TextReader Reader { get; protected set; }

        /// <summary>
        /// Reads game state
        /// </summary>
        /// <returns>Read game state</returns>
        public IGameState Read()
        {
            throw new NotImplementedException();
        }
    }
}
