using System.Windows;
using System.Windows.Threading;

namespace Dropbox.App
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var s = new View.DropBoxView();
            MainWindow = s;
            MainWindow.Show();

            DispatcherUnhandledException += App_DispatcherUnhandledException;
            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Application  Error!!.", MessageBoxButton.OK);
        }
    }
}