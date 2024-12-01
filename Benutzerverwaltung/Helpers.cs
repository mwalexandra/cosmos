using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung
{
    internal class Helpers
    {
        public bool IsValidInput(string input)
        {
            if ((input.Length < 4) || (input.Length > 16))
            {
                return false;
            }
            return IsAlpha(input);
        }

        public bool IsAlpha(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }


        public void HelpCreateUser()
        {
            Console.WriteLine("Enter a username (4–16 characters, Latin letters only).");
            Console.WriteLine("Enter a password (4–16 characters, Latin letters only).");
        }

        public void HelpDeleteUser()
        {
            Console.WriteLine("Enter the user to be deleted and confirm the input.");
        }

        public bool isAllowed (User userToChange, List<User> users, string action)
        {
            Console.Write("Input your password to check your permissions: ");
            string password = Console.ReadLine();
            User user = users.Find(userItem => userItem.GetPassword().Equals(password, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                int userRole = user.GetRole();
                Console.Write(userRole);
                int userToChangeRole = userToChange.GetRole();
                Console.Write(userToChangeRole);
                {
                    switch (action)
                    {
                        case "create":
                        case "delete": 
                            return CheckRights(userRole, userToChangeRole);
                        case "rerole":
                            return userRole == 0 && userToChangeRole != 0 ? true : false; // roles can be changed by root only for all user exept himself
                        case "reuser":
                        case "repass":
                            return userRole == userToChangeRole ? true : false;  // name and password can be changed by user himself only
                        default: return false;
                    } 
                }
            } else
            {
                Console.WriteLine("Input is not correct.");
            }
            return false;
        }

        static bool CheckRights(int userRole, int userToChangeRole)
        {
            if (userRole == 3) return false;
            else if (userRole == 2)
            {
                return userToChangeRole == 3 ? true : false;
            }
            else if (userRole == 1)
            {
                switch (userToChangeRole)
                {
                    case 2:
                    case 3:
                        return true;
                    default:
                        return false;
                }
            }
            else if (userRole == 0)
            {
                switch (userToChangeRole)
                {
                    case 0:
                        return false;
                    default:
                        return true;
                }
            }
            return false;
        }

    }
}
