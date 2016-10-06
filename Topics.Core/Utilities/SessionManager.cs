using System.Web;
using System.Web.SessionState;
using Topics.Core.Models;

namespace Topics.Core.Utilities
{
    public class SessionManager
    {
        public static UserDTO User
        {
            get { return ((Session["User"] is UserDTO) ? (UserDTO)Session["User"] : null); }
            set { Session["User"] = value; }
        }

        private static HttpSessionState Session
        {
            get { return (HttpContext.Current == null) ? null : HttpContext.Current.Session; }
        }

        public static void Abanndon()
        {
            Session.Abandon();
        }
    }
}
