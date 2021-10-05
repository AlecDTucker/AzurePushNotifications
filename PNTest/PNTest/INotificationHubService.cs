using System.Collections.Generic;

namespace PNTest
{
    public interface INotificationHubService
    {
        void AddTag(string tag);
        void ClearTags();
        List<string> GetTags();
    }
}