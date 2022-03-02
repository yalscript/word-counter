using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
