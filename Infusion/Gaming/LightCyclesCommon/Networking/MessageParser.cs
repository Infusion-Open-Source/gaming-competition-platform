namespace Infusion.Gaming.LightCyclesCommon.Networking
{
    using System;

    /// <summary>
    /// Message parser, helps in parsing values out
    /// </summary>
    public class MessageParser
    {
        /// <summary>
        /// Try to get string when the message starts with given pattern
        /// </summary>
        /// <param name="message">message to check</param>
        /// <param name="pattern">pattern to look for</param>
        /// <param name="stringValue">outputted value</param>
        /// <returns>Whether pattern was found</returns>
        public bool TryGetString(string message, string pattern, out string stringValue)
        {
            stringValue = string.Empty;
            if (message.StartsWith(pattern))
            {
                stringValue = message.Replace(pattern, string.Empty);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to get integer when the message starts with given pattern
        /// </summary>
        /// <param name="message">message to check</param>
        /// <param name="pattern">pattern to look for</param>
        /// <param name="intValue">outputted value</param>
        /// <returns>Whether pattern was found</returns>
        public bool TryGetInt(string message, string pattern, out int intValue)
        {
            intValue = 0;
            if (message.StartsWith(pattern))
            {
                intValue = int.Parse(message.Replace(pattern, string.Empty));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to get two integers when the message starts with given pattern
        /// </summary>
        /// <param name="message">message to check</param>
        /// <param name="pattern">pattern to look for</param>
        /// <param name="separator">separator for integers</param>
        /// <param name="int1">outputted first value</param>
        /// <param name="int2">outputted second value</param>
        /// <returns>Whether pattern was found</returns>
        public bool TryGetIntInt(string message, string pattern, string separator, out int int1, out int int2)
        {
            int1 = 0;
            int2 = 0;
            if (message.StartsWith(pattern))
            {
                string[] parameters = message.Replace(pattern, string.Empty).Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                int1 = int.Parse(parameters[0]);
                int2 = int.Parse(parameters[1]);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to get two strings when the message starts with given pattern
        /// </summary>
        /// <param name="message">message to check</param>
        /// <param name="pattern">pattern to look for</param>
        /// <param name="separator">separator for integers</param>
        /// <param name="value1">outputted first value</param>
        /// <param name="value2">outputted second value</param>
        /// <returns>Whether pattern was found</returns>
        public bool TryGetStringString(string message, string pattern, string separator, out string value1, out string value2)
        {
            value1 = string.Empty;
            value2 = string.Empty;
            if (message.StartsWith(pattern))
            {
                string[] parameters = message.Replace(pattern, string.Empty).Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                value1 = parameters[0];
                value2 = parameters[1];
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to check whether message starts with given pattern
        /// </summary>
        /// <param name="message">message to check</param>
        /// <param name="pattern">pattern to look for</param>
        /// <returns>Whether pattern was found</returns>
        public bool TryGet(string message, string pattern)
        {
            return message.StartsWith(pattern);
        }
    }
}
