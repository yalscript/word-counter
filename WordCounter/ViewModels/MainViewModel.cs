using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
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
            var selectedDirectory = _fileService.OpenDirectoryPickerDialog();
            
            if (!string.IsNullOrWhiteSpace(selectedDirectory))
            {
                SelectedDirectory = selectedDirectory;
            }
        }

        /// <summary>
        /// Searches a certain text in the specified directory.
        /// </summary>
        private void SearchTextInDirectory()
        {
            
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
