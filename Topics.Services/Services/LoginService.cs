using System;
using System.Collections.Generic;
using Topics.Core.Constants;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services.Interfaces;

namespace Topics.Services.Services
{
    public class LoginService : ILoginService
    {
        private IUserService _userService;

        public LoginService(IUserService userService)
        {
            _userService = userService;
        }

        public Boolean LoginUser(UserRef user, ValidationMessageList messages)
        {
            ICollection<UserDTO> users = _userService.GetUsers();
            UserDTO loggedInUser = null;
            foreach (UserDTO u in users)
            {
                if (u.UserName == user.UserName)
                {
                    loggedInUser = u;
                }
            }
            if (loggedInUser == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_USER));
                return false;
            }
            if (loggedInUser.IsEnabled == false)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.INACTIVE));
                return false;
            }
            else
            {
                UserCredentialsDTO credentials = _userService.GetUserCredentialsByUserID(loggedInUser.UserID);
                if (user.Password + credentials.Salt == credentials.PasswordHash)
                {
                    SessionManager.User = loggedInUser;
                    return true;
                }
                else
                {
                    messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.WRONG_PASSWORD));
                    return false;
                }
            }
        }

        public void Logoff()
        {
            if (SessionManager.User != null)
            {
                SessionManager.Abanndon();
            }
        }
    }
}
