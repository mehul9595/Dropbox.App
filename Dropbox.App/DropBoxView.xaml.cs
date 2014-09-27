using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dropbox.App.Model;
using Dropbox.App.ViewModels;

namespace Dropbox.App
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class DropBoxView : Window
    {
        public DropBoxViewModel DropBoxViewModel { get; set; }

        public DropBoxView()
        {
            InitializeComponent();
            DropBoxViewModel = new DropBoxViewModel();
            this.DataContext = DropBoxViewModel;
            DropBoxViewModel.ProgressChanged += ProgressChanged;
            DropBoxViewModel.TaskCompleted +=TaskCompleted;
        }

        private void TaskCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.StatusIndicator.Dispatcher.BeginInvoke(new Action(() => ProgressBar.Value = 100));

        }

        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            StatusIndicator.Dispatcher.BeginInvoke(new Action(() =>
            {
                ProgressBar.Value = e.ProgressPercentage;
            }));
        }

        public void DropBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                DropBoxViewModel.AddFiles(files);
            }
        }

        public void DropBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as ListBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public void DropBox_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }
    }
}
