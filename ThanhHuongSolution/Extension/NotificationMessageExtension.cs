using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.Notification;

namespace ThanhHuongSolution.Extension
{
    public static class NotificationMessageExtension
    {
        private const string NOTIFICATION_COLLECTION = "NOTIFICATION_COLLECTION";

        private static NotificationCollection AllNotification(TempDataDictionary dictionary)
        {
            NotificationCollection notifications;

            if (!dictionary.ContainsKey(NOTIFICATION_COLLECTION))
            {
                notifications = new NotificationCollection();
                dictionary.Add(NOTIFICATION_COLLECTION, notifications);
            }
            else {
                notifications = dictionary[NOTIFICATION_COLLECTION] as NotificationCollection;
            }

            return notifications;
        }

        public static void AddNotification(this TempDataDictionary dictionary, NotificationType type, string message)
        {
            AllNotification(dictionary).Add(new NotificationMessage(type, message));
        }

        public static NotificationCollection Notifications(this TempDataDictionary dictionary)
        {
            return AllNotification(dictionary);
        }
    }
}