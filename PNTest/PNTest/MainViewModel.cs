using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace PNTest
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region PropertyChangedNotification bits
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChangedExplicit(propertyName);
        }

        protected virtual void RaisePropertyChangedExplicit(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T property, T newValue, [CallerMemberName] string propertyName = null)
        {
            bool isSuccessful = false;

            if (!Equals(property, newValue))
            {
                property = newValue;
                RaisePropertyChangedExplicit(propertyName);
                isSuccessful = true;
            }

            return isSuccessful;
        }
        #endregion PropertyChangedNotification bits

        #region Constructors & initialisation
        public MainViewModel()
        {
            notificationHubService = DependencyService.Get<INotificationHubService>();
            HubName = Constants.NotificationHubName;
        }
        #endregion
        #region Services
        private readonly INotificationHubService notificationHubService;
        #endregion

        #region Properties
        private string _hubName;
        public string HubName { get => _hubName; set => SetProperty(ref _hubName, value); }

        private string _newTag;
        public string NewTag { get => _newTag; set => SetProperty(ref _newTag, value); }

        private string _currentTags;
        public string CurrentTags { get => _currentTags; set => SetProperty(ref _currentTags, value); }
        #endregion

        #region Commands
        private ICommand _addTagCommand = null;
        public ICommand AddTagCommand => _addTagCommand = _addTagCommand ?? new Command(DoAddTagCommand);

        private ICommand _clearTagsCommand = null;
        public ICommand ClearTagsCommand => _clearTagsCommand = _clearTagsCommand ?? new Command(DoClearTagsCommand);

        private ICommand _refreshTagsCommand = null;
        public ICommand RefreshTagsCommand => _refreshTagsCommand = _refreshTagsCommand ?? new Command(DoRefreshTagsCommand);
        #endregion

        #region Methods
        private void DoAddTagCommand()
        {
            notificationHubService.AddTag(NewTag);
            RefreshTags();
        }

        private void DoClearTagsCommand()
        {
            notificationHubService.ClearTags();
            RefreshTags();
        }

        private void DoRefreshTagsCommand()
        {
            RefreshTags();
        }

        private void RefreshTags()
        {
            List<string> tags = notificationHubService.GetTags();
            CurrentTags = string.Empty;
            if (tags?.Count > 0)
            {
                foreach (string tag in tags)
                {
                    string prefix = string.IsNullOrWhiteSpace(CurrentTags) ? string.Empty : Environment.NewLine;
                    CurrentTags = $"{prefix}{tag}";
                }
            }
        }
        #endregion

    }
}