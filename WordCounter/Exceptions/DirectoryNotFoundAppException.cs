using System;

namespace WordCounter.Exceptions
{
    /// <summary>
    /// Custom exception raised when a directory is not found.
    /// </summary>
    public class DirectoryNotFoundAppException : Exception
    {
        /// <summary>
        /// Constructor of the class. The error message needs to be passed
        /// </summary>
        /// <param name="message">Error message</param>
        public DirectoryNotFoundAppException(string message)
            : base(message)
        {
        }
    }
}
