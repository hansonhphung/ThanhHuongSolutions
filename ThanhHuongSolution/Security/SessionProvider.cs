using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhHuongSolution.Security
{
    public class SessionProvider
    {
        static string USER_NAME = "username";

        public static string UserName
        {
            get {
                if (HttpContext.Current == null)
                    return string.Empty;

                var username = HttpContext.Current.Session[USER_NAME];
                if (username != null)
                    return username as string;

                return null;
            }

            set {
                HttpContext.Current.Session[USER_NAME] = value;
            }
        }
    }
}