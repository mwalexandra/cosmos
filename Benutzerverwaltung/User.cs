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
        private int Role { get; set; }   // 0 - Root user, 1 - Admin, 2 - Fachanwender, 3 - Anwender (default)


        public User(string username, string password, int initialRole)
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

        public int GetRole()
        {
            return Role;
        }

        public void SetRole (int newRole)
        {
            Role = newRole;
        }

        public string RoleToString(int role) 
        {
            switch (role)
            {
                case 0:
                    return "Root user";
                case 1:
                    return "Manager admin";
                case 2:
                    return "Manager user";
                case 3:
                    return "User";
                default:
                    return "Wrong input";
            }
        }
    }
}
