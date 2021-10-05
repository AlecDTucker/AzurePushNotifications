using System;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using WindowsAzure.Messaging.NotificationHubs;
using Xamarin.Forms;

namespace PNTest.Droid
{
    public class AzureListener : Java.Lang.Object, INotificationListener
    {
        public AzureListener()
        {
        }

        public void OnPushNotificationReceived(Context context, INotificationMessage message)
        {
            Intent intent = new Intent(context, typeof(MainActivity));
            intent.PutExtra("TitleKey", message.Title);
            intent.PutExtra("MessageKey", message.Body);

            PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(context, MainActivity.CHANNEL_ID);

            notificationBuilder
                .SetContentTitle(message.Title)
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetContentText(message.Body)
                .SetAutoCancel(true)
                .SetShowWhen(false)
                .SetContentIntent(pendingIntent);

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(0, notificationBuilder.Build());
        }

    }
}
