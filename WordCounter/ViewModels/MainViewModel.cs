using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WordCounter.Exceptions;
using WordCounter.Models;
using WordCounter.Services;

namespace WordCounter.ViewModels
{
    /// <summary>
    /// ViewModel of the view <see cref="Views.MainView"/>.
    /// </summary>
    public class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Instance of the <see cref="IFileService"/> for IO operations.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Stores the user's selected directory.
        /// </summary>
        private string _selectedDirectory;

        /// <summary>
        /// Gets or sets the user's selected directory. Updates the UI when set.
        /// </summary>
        public string SelectedDirectory
        {
            get { return _selectedDirectory; }
            set
            {
                SetProperty(ref _selectedDirectory, value);
                SearchTextInDirectoryCommand.NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// Stores the text that the user wants to search
        /// </summary>
        private string _textToSearch;

        /// <summary>
        /// Gets or sets the text that the user wants to search. Updates the UI when set.
        /// </summary>
        public string TextToSearch
        {
            get { return _textToSearch; }
            set
            {
                SetProperty(ref _textToSearch, value);
                SearchTextInDirectoryCommand.NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets the list of files where a text coincidence have occurred.
        /// </summary>
        public ObservableCollection<TextCoincidenceModel> TextCoincidences { get; } = new ObservableCollection<TextCoincidenceModel>();

        /// <summary>
        /// Gets the number of times that a certain text has been found in all the text files of a directory (and its directories).
        /// </summary>
        public int TotalCoincidences
        {
            get
            {
                return TextCoincidences.Sum(x => x.Coincidences);
            }
        }

        /// <summary>
        /// Gets the selected directory command to open the directory picker dialog.
        /// </summary>
        public RelayCommand SelectDirectoryCommand { get; }

        /// <summary>
        /// Gets the command for searching the given text in the given directory.
        /// </summary>
        public RelayCommand SearchTextInDirectoryCommand { get; }

        /// <summary>
        /// Constructor of the class. The <see cref="IFileService"/> is needed.
        /// </summary>
        /// <param name="fileService">File service for IO operations.</param>
        public MainViewModel(IFileService fileService)
        {
            _fileService = fileService;

            SelectDirectoryCommand = new RelayCommand(SelectDirectory);
            SearchTextInDirectoryCommand = new RelayCommand(SearchTextInDirectory, SearchTextInDirectoryCommandCanExecute);
        }

        /// <summary>
        /// Opens the directory picker dialog to the user.
        /// </summary>
        private void SelectDirectory()
        {
            try
            {
                var selectedDirectory = _fileService.OpenDirectoryPickerDialog();

                if (!string.IsNullOrWhiteSpace(selectedDirectory))
                {
                    SelectedDirectory = selectedDirectory;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error has occurred", "Error");
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Searches a certain text in the specified directory.
        /// </summary>
        private void SearchTextInDirectory()
        {
            try
            {
                TextCoincidences.Clear();

                foreach (string filePath in _fileService.GetTextFilePaths(SelectedDirectory, true))
                {
                    var coincidences = _fileService.CountTextCoincidences(filePath, TextToSearch);
                    if (coincidences <= 0)
                    {
                        continue;
                    }

                    TextCoincidences.Add(new TextCoincidenceModel()
                    {
                        FilePath = filePath,
                        Coincidences = coincidences
                    });
                }

                OnPropertyChanged(nameof(TotalCoincidences));

                if (!TextCoincidences.Any())
                {
                    MessageBox.Show(string.Format("We have not found any file containing the text [{0}]", TextToSearch), "Information");
                }
            } 
            catch (DirectoryNotFoundAppException ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Trace.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error has occurred", "Error");
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Checks if the search operation can be executed
        /// </summary>
        /// <returns>Returns whether the user has filled the form or not</returns>
        private bool SearchTextInDirectoryCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(SelectedDirectory) && !string.IsNullOrWhiteSpace(TextToSearch);
        }
    }
}
