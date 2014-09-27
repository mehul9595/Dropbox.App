using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dropbox.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DropBoxView s = new DropBoxView();
            this.MainWindow = s;
            this.MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
