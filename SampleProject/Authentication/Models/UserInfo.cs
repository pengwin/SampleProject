using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using SampleProject.Models.UserModels;

namespace SampleProject.Authentication.Models
{
    /// <summary>
    /// User info that is inside the authentication cookie.
    /// It is easily converted to string and back.
    /// </summary>
    public class UserInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string[] Roles { get; set; }
        public string ApiKey { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserInfo()
        {
            Roles = new string[0];
        }

        /// <summary>
        /// Returns the string that can be used to feed as UserData into the FromsAuthentificationTicket.
        /// </summary>
        /// <returns>String serialization.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}|{1}|{2}|{3}|{4}|", UserId, Username, Email, FullName,ApiKey);
            foreach (var role in Roles)
            {
                builder.AppendFormat("{0};",role);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Creates a UserInfo class from the User class.
        /// </summary>
        /// <param name="user">User who's data is used to create the UserInfo.</param>
        /// <returns>UserInfo created from User.</returns>
        public static UserInfo FromUser(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null);

            var roles = new List<string>();
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    roles.Add(role.RoleName);
                }
            }

            var result = new UserInfo
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Roles = roles.ToArray(),
                ApiKey = user.ApiKey.ToString()
            };
            return result;
        }

        /// <summary>
        /// Creates a UserInfo class from the string.
        /// </summary>
        /// <param name="user">Serialized string.</param>
        /// <returns>UserInfo created from the provided string.</returns>
        /// <exception cref="ArgumentException">When string is not a serialized UserInfo string.</exception>
        public static UserInfo FromString(string user)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(user));

            var data = user.Split('|');
            if (data.Length != 6)
                throw new ArgumentException("This string is not a serialized UserInfo class.");

            var result = new UserInfo
            {
                UserId = int.Parse(data[0]),
                Username = data[1],
                Email = data[2],
                FullName = data[3],
                ApiKey = data[4]
            };

            var rolesData = data[5];
            var roles = rolesData.Split(new [] {';'},StringSplitOptions.RemoveEmptyEntries);
            result.Roles = roles;

            return result;
        }
    }
}