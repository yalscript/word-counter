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
    }
}
