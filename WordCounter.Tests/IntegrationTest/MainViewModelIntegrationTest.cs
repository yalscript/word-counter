using System;
using System.IO;
using System.Linq;
using WordCounter.Services;
using WordCounter.ViewModels;
using Xunit;

namespace WordCounter.Tests
{
    public class MainViewModelIntegrationTest
    {
        [Fact]
        public void Search_text_in_directory()
        {
            var mainViewModel = new MainViewModel(new FileService())
            {
                SelectedDirectory = Path.Combine(Environment.CurrentDirectory, "Resources"),
                TextToSearch = "w"
            };

            mainViewModel.SearchTextInDirectoryCommand.Execute(null);

            Assert.True(mainViewModel.TotalCoincidences == 1);
        }
    }
}
