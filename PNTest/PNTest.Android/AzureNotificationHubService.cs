using System.Collections.Generic;
using PNTest.Droid;
using WindowsAzure.Messaging.NotificationHubs;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureNotificationHubService))]
namespace PNTest.Droid
{
    public class AzureNotificationHubService : INotificationHubService
    {
        #region Constructors & initialisation
        public AzureNotificationHubService()
        {
        }
        #endregion

        #region Methods
        public void AddTag(string tag)
        {
            NotificationHub.AddTag(tag);
        }

        public void ClearTags()
        {
            NotificationHub.ClearTags();
        }

        public List<string> GetTags()
        {
            List<string> allTags = new List<string>();

            Java.Util.IIterator iterator = NotificationHub.Tags.Iterator();

            while (iterator.HasNext)
            {
                allTags.Add((string)iterator.Next());
            }

            return allTags;
        }
        #endregion
    }
}