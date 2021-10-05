using UIKit;
using WindowsAzure.Messaging.NotificationHubs;

namespace PNTest.iOS
{
    public class AzureListener : MSNotificationHubDelegate
    {
        public override void DidReceivePushNotification(MSNotificationHub notificationHub, MSNotificationHubMessage message)
        {
            string alertTitle = message.Title ?? "Notification";
            string alertBody = message.Body;

            ShowNotification(alertTitle, alertBody);
        }

        private void ShowNotification(string title, string body)
        {
            UIAlertController alert = UIAlertController.Create(title, body, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
    }
}
