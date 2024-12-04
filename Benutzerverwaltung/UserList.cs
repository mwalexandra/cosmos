using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung
{
    internal class UserList
    {
        private List<User> users;

        public UserList() 
        {
            users = new List<User>();
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public void SetUsers(List<User> newUsers)
        {
            users = newUsers;
        }

        public void AddUser(User user) 
        {
            users.Add(user);
        }

        public void RemoveUser(User user) 
        {
            users.Remove(user);
        }

        public User FindUserByUsername(string username)
        {
            return users.FirstOrDefault(user => user.GetUsername() == username);
        }
    }
}
