using System;
using System.Text;

namespace ENG.WMOCodes.Decoders.Internal
{
    /// <summary>
    /// Exception thrown when decoding failed.
    /// </summary>
    public class DecodeException : Exception
    {
        /// <summary>
        /// Gets the description. Description should describe location where error occured.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodeException"/> class.
        /// </summary>
        /// <param name="decoderDescription">The decoder description. Description should describe location where error occured.</param>
        /// <param name="inner">The inner.</param>
        public DecodeException(string decoderDescription, Exception inner)
          : base("", inner)
        {
            Description = decoderDescription;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
        public override string Message => GenerateMessage();

        /// <summary>
        /// Generates the message.
        /// </summary>
        /// <returns></returns>
        private string GenerateMessage()
        {
            StringBuilder tree = new StringBuilder();
            DecodeException curr = this;

            tree.Append("Decoding failed at ");

            while (true)
            {
                tree.Append("->" + curr.Description);
                var exception = curr.InnerException as DecodeException;
                if (exception != null)
                    curr = exception;
                else
                    break;
            }

            tree.Append(". Reason:");

            Exception ex = curr.InnerException;
            while (ex != null)
            {
                tree.Append(" >> " + curr.InnerException.Message);
                ex = ex.InnerException;
            }

            return tree.ToString();
        }
    }
}
