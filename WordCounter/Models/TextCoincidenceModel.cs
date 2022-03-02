namespace WordCounter.Models
{
    /// <summary>
    /// Model for text coincidences in a given file.
    /// </summary>
    public class TextCoincidenceModel
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the number of coincidences of a given text in the file specified in <see cref="FilePath"/>.
        /// </summary>
        public int Coincidences { get; set; }
    }
}
