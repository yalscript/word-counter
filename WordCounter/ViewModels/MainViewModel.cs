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
            set { SetProperty(ref _selectedDirectory, value); }
        }

        /// <summary>
        /// Gets the selected directory command to open the directory picker dialog.
        /// </summary>
        public ICommand SelectDirectoryCommand { get; }

        /// <summary>
        /// Constructor of the class. The <see cref="IFileService"/> is needed.
        /// </summary>
        /// <param name="fileService">File service for IO operations.</param>
        public MainViewModel(IFileService fileService)
        {
            _fileService = fileService;

            SelectDirectoryCommand = new RelayCommand(SelectDirectory);
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
    }
}
