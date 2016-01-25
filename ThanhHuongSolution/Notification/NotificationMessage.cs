using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhHuongSolution.Notification
{
    public class NotificationMessage
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }

        public NotificationMessage() { }

        public NotificationMessage(NotificationType type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}