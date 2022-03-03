using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordCounter.Exceptions;

namespace WordCounter.Services
{
    public class FileService : IFileService
    {
        public string OpenDirectoryPickerDialog()
        {
            var directoryPickerDialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true
            };

            if (directoryPickerDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return directoryPickerDialog.FileName;
            }

            return string.Empty;
        }

        public List<string> GetTextFilePaths(string directory, bool recursive)
        {
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                throw new DirectoryNotFoundAppException(string.Format("The directory [{0}] was not found.", directory));
            }

            var searchOperation = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            return Directory.GetFiles(directory, "*.txt", searchOperation).ToList();
        }

        public int CountTextCoincidences(string filePath, string textToSearch)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundAppException(string.Format("The file [{0}] was not found.", filePath));
            }

            var fileContent = File.ReadAllText(filePath);

            return Regex.Matches(fileContent, textToSearch, RegexOptions.IgnoreCase).Count;
        }
    }
}
