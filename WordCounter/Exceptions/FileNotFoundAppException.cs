﻿using System;

namespace WordCounter.Exceptions
{
    /// <summary>
    /// Custom exception raised when a file is not found.
    /// </summary>
    public class FileNotFoundAppException : Exception
    {
        /// <summary>
        /// Constructor of the class. The error message needs to be passed
        /// </summary>
        /// <param name="message">Error message</param>
        public FileNotFoundAppException(string message)
            : base(message)
        {
        }
    }
}
