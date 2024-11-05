using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung
{
    internal class User
    {
        private string Username { get; set; }
        private string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
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
    }
}
