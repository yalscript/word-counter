using System;
using System.IO;
using System.Linq;
using WordCounter.Services;
using Xunit;

namespace WordCounter.Tests
{
    public class FileServiceUnitTest
    {
        [Fact]
        public void Get_text_file_paths_from_null_directory()
        {
            var fileService = new FileService();

            Assert.Throws<DirectoryNotFoundException>(() => fileService.GetTextFilePaths(null, true));
        }

        [Fact]
        public void Get_text_file_paths_from_empty_directory()
        {
            var fileService = new FileService();

            Assert.Throws<DirectoryNotFoundException>(() => fileService.GetTextFilePaths(string.Empty, true));
        }

        [Fact]
        public void Get_text_file_paths_from_whitespaced_directory()
        {
            var fileService = new FileService();

            Assert.Throws<DirectoryNotFoundException>(() => fileService.GetTextFilePaths("   ", true));
        }

        [Fact]
        public void Get_text_file_paths_from_nonexistent_directory()
        {
            var fileService = new FileService();

            Assert.Throws<DirectoryNotFoundException>(() => fileService.GetTextFilePaths(@"C:\lkjashkjasdhfjk\kasjghjhf", true));
        }

        [Fact]
        public void Get_text_file_paths_from_directory()
        {
            var fileService = new FileService();

            var directory = Path.Combine(Environment.CurrentDirectory, "Resources");

            var test = fileService.GetTextFilePaths(directory, true);

            Assert.Contains(fileService.GetTextFilePaths(directory, true), x => Path.Combine(directory, "test.txt").Equals(x));
        }
    }
}
