using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Topics.Core.Constants;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services.Interfaces;

namespace Topics.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private IUserService _userService;

        public RegisterService(IUserService userService)
        {
            _userService = userService;
        }

        public Boolean RegisterUser(UserRef user, ValidationMessageList messages)
        {
            ICollection<UserDTO> users = _userService.GetUsers();
            Boolean uniqueName = true;
            foreach (UserDTO u in users)
            {
                if (u.UserName == user.UserName)
                {
                    uniqueName = false;
                }
            }
            if (uniqueName)
            {
                UserDTO newUser = new UserDTO
                {
                    UserName = user.UserName,
                    IsEnabled = true,
                    RoleID = user.RoleID,
                };
                _userService.AddUser(newUser);
                string salt = SaltManager.GetSalt();
                UserCredentialsDTO newUserCredential = new UserCredentialsDTO
                {
                    PasswordHash = user.Password + salt,
                    Salt = salt,
                    UserID = _userService.GetUserByName(user.UserName).UserID
                };
                _userService.AddUserCredentials(newUserCredential);
                return true;
            }

            messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.USERNAME_IN_USE));
            return false;
        }
    }
}
