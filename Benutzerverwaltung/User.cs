using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung
{

    internal class User
    {
        private string Username { get; set; }
        private string Password { get; set; }
        private string Role { get; set; }


        public User(string username, string password, string initialRole)
        {
            Username = username;
            Password = password;
            Role = initialRole;
        }

        public string GetUsername() 
        {
            return Username;
        }

        public string GetPassword()
        {
            return Password;
        }

        public void SetUsername(string username)
        {
            Username = username;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public string GetRole()
        {
            return Role;
        }

        public void SetRole (string newRole)
        {
            Role = newRole;
        }
    }
}
