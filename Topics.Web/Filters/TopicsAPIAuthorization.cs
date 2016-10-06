using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Services.Services.Interfaces;

namespace Topics.Web.Filters
{
    public class TopicsAPIAuthorization : AuthorizeAttribute
    {
        private IRoleService RoleService { get { return Ioc.AutofacConfig.Resolve<IRoleService>(); } }
        private UserDTO User { get { return SessionManager.User; } }

        public override void OnAuthorization(HttpActionContext context)
        {
            var isAuthenticated = AuthorizeCore();
            if (isAuthenticated)
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    if (!CheckRoles(User))
                    {
                        HandleUnauthorizedRequest(context);
                    }
                }
            }
            else
            {
                HandleUnauthorizedRequest(context);
            }
        }

        protected bool AuthorizeCore()
        {
            bool isAuthenticated = User != null;
            return isAuthenticated;
        }

        private bool CheckRoles(UserDTO user)
        {
            string[] roles = Roles.Split(',');

            if (roles.Length == 0)
            {
                return true;
            }
            if (user.Role == null)
            {
                return false;
            }
            return roles.Contains(user.Role.Name);
        }
    }
}
