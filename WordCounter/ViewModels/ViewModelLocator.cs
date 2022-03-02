using Microsoft.Extensions.DependencyInjection;

namespace WordCounter.ViewModels
{
    /// <summary>
    /// Maps ViewModels to properties so a certain View can specify which ViewModel it should use.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// ViewModel of the view <see cref="MainView"/>.
        /// </summary>
        public static MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
    }
}
