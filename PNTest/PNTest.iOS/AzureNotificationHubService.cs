using System.Collections.Generic;
using PNTest.iOS;
using WindowsAzure.Messaging.NotificationHubs;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureNotificationHubService))]
namespace PNTest.iOS
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
            MSNotificationHub.AddTag(tag);
        }

        public void ClearTags()
        {
            MSNotificationHub.ClearTags();
        }

        public List<string> GetTags()
        {
            List<string> allTags = new List<string>();

            Foundation.NSArray<Foundation.NSString> tags = MSNotificationHub.GetTags();

            if (tags?.Count > 0)
            {
                foreach (string tag in tags)
                {
                    allTags.Add(tag);
                }
            }

            return allTags;
        }
        #endregion
    }
}
