using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter.Services
{
    /// <summary>
    /// Manages IO operations
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Opens the directory picker dialog.
        /// </summary>
        /// <returns>Returns the selected directory. If nothing is selected, returns an empty string.</returns>
        string OpenDirectoryPickerDialog();

        /// <summary>
        /// Gets all the text files of a given directory. If <paramref name="recursive"/> is true, it will include the text files of the subdirectories.
        /// </summary>
        /// <returns>Returns the list of file paths found.</returns>
        List<string> GetTextFilePaths(string directory, bool recursive);

        /// <summary>
        /// Counts the number of times that a given text is found in a file.
        /// </summary>
        /// <returns>Returns the number of coincidences</returns>
        int CountTextCoincidences(string filePath, string textToSearch);
    }
}
