using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Dropbox.App.Annotations;
using Dropbox.App.Commands;
using Dropbox.App.Model;
using DropNet.Exceptions;
using Microsoft.Win32;

namespace Dropbox.App.ViewModels
{
    public class DropBoxViewModel : INotifyPropertyChanged
    {
        #region PROPERTIES

        private readonly BackgroundWorker _worker;
        private bool _isAuthorized;
        private string _statusText = "Ready!!!";

        public ObservableCollection<string> Files { get; set; }
        public DropboxHelper DropboxHelper { get; set; }
        public ICommand UploadCommand { get; set; }
        public ICommand BrowseDialogCommand { get; set; }
        public ICommand AuthorizeCommand { get; set; }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public bool IsAuthorized
        {
            get { return _isAuthorized; }
            set
            {
                _isAuthorized = value;
                OnPropertyChanged("IsAuthorized");
            }
        }

        #endregion PROPERTIES

        #region EVENTS

        //event
        public event PropertyChangedEventHandler PropertyChanged;

        public event ProgressChangedEventHandler ProgressChanged
        {
            add { _worker.ProgressChanged += value; }
            remove { _worker.ProgressChanged -= value; }
        }

        public event RunWorkerCompletedEventHandler TaskCompleted
        {
            add { _worker.RunWorkerCompleted += value; }
            remove { _worker.RunWorkerCompleted -= value; }
        }

        #endregion EVENTS

        #region CONSTRUCTOR

        public DropBoxViewModel()
        {
            Files = new ObservableCollection<string>();
            DropboxHelper = new DropboxHelper();
            UploadCommand = new DelegateCommand(UploadFiles, o => { return Files.Count > 0 && IsAuthorized; });

            BrowseDialogCommand = new DelegateCommand(BrowseDialog, o => true);
            AuthorizeCommand = new DelegateCommand(OnAuthorizeCommand, o => !IsAuthorized);

            //configure worker
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            TaskCompleted += OnTaskCompleted;

            IsAuthorized = DropboxHelper.Client.UserLogin != null;
            string status = IsAuthorized ? "Ready!!!" : "Allow this App to use Dropbox.";
            ThreadSafeUpdateStatus(status);
        }

        #endregion CONSTRUCTOR

        #region PUBLIC METHODS

        public void UploadFiles()
        {
            VerifyAuthorization();

            _worker.DoWork -= OnWorkerOnDoWork; //safely remove subscribers
            _worker.DoWork += OnWorkerOnDoWork;

            _worker.RunWorkerAsync();
        }

        public void AddFiles(string[] files)
        {
            foreach (string file in files)
            {
                //Check if file path is directory, if directory then loop through all the sub paths.
                FileAttributes attributes = File.GetAttributes(file);
                if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string[] paths = Directory.GetFiles(file, "*.*", SearchOption.AllDirectories);

                    foreach (var path in paths)
                    {
                        if (!Files.Contains(path))
                        {
                            Files.Add(path);
                        }
                    }
                }
                else
                {
                    if (!Files.Contains(file))
                    {
                        Files.Add(file);
                    }
                }
            }
            ((DelegateCommand)UploadCommand).RaiseCanExecuteChanged();
        }

        public void BrowseDialog()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            fileDialog.Multiselect = true;

            bool? result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                AddFiles(fileDialog.FileNames);
            }
        }

        #endregion PUBLIC METHODS

        #region PRIVATE METHODS

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ThreadSafeUpdateStatus(string status)
        {
            Dispatcher.CurrentDispatcher.Invoke(
                DispatcherPriority.Background, new Action(delegate { StatusText = status; }));
        }

        private void OnAuthorizeCommand()
        {
            VerifyAuthorization();
        }

        private void OnTaskCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action(() => { StatusText = "Files uploaded successfully!!!"; }));
        }

        private void VerifyAuthorization()
        {
            string url = DropboxHelper.GetAuthorizationUrl();
            if (!string.IsNullOrEmpty(url))
            {
                MessageBoxResult result =
                    MessageBox.Show(
                        string.Format(
                            "The app needs permission to use dropbox. Click 'Ok' to open this url: {0} in browser.", url),
                        "Authorize App", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    Process.Start(url);

                    MessageBoxResult r = MessageBox.Show("Press 'Ok' once you Authorize the app to use dropbox.",
                        "Wait!!!",
                        MessageBoxButton.OK);

                    if (r == MessageBoxResult.OK)
                    {
                        IsAuthorized = DropboxHelper.GetAccessToken();
                        if (IsAuthorized)
                        {
                            ThreadSafeUpdateStatus("Ready!!!");
                        }
                        else
                        {
                            ThreadSafeUpdateStatus("Allow this App to use Dropbox.");
                        }
                    }
                    else
                    {
                        ThreadSafeUpdateStatus("Allow this App to use Dropbox.");
                    }
                }
                else
                {
                    ThreadSafeUpdateStatus("Allow this App to use Dropbox.");
                }
            }
        }

        private void OnWorkerOnDoWork(object sender, DoWorkEventArgs args)
        {
            UploadToDropbox();
        }

        private void UploadToDropbox()
        {
            foreach (string file in Files)
            {
                string file1 = file;
                try
                {
                    var fileInfo = new FileInfo(file1);

                    int index = Files.IndexOf(file1) + 1;
                    int percentage = (index * 100) / Files.Count;
                    string root = string.Format(@"dropbox/{0}/", fileInfo.Directory.Name);

                    var metaData = DropboxHelper.Client.UploadFile(root, fileInfo.Name, File.ReadAllBytes(file));
                   
                    _worker.ReportProgress((int)Math.Round((double)percentage));

                    ThreadSafeUpdateStatus(string.Format("Uploaded {0} of {1} files.", index, Files.Count));
                }
                catch (DropboxException de)
                {
                    if (de.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        ThreadSafeUpdateStatus("Please Authorize this App to use Dropbox.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Current.Shutdown();
                }
            }
        }

        #endregion PRIVATE METHODS
    }
}