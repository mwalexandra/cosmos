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

        public void showHelp()
        {
            Console.WriteLine("* User management:");
            Console.WriteLine("'userlist' to display all users.");
            Console.WriteLine("'create' to create a user and password.");
            Console.WriteLine("'delete' to delete a user.");
            Console.WriteLine("'rename' to rename a user.");
            Console.WriteLine("'repass' to change a password.");
            Console.WriteLine("'rerole' to change a role of any user.");
            Console.WriteLine("* Lists management:");
            Console.WriteLine("'createlist' to create new list of users.");
            Console.WriteLine("'deletelist' to stop the program.");
            Console.WriteLine("'accesslist' to reboot the system.");
            Console.WriteLine("'movelist' to stop the program.");
            Console.WriteLine("'editlist' to reboot the system.");
            Console.WriteLine("'showlist' to reboot the system.");
            Console.WriteLine("'changeuser' to change user.");
            Console.WriteLine("* System mamagement:");
            Console.WriteLine("'exit' to stop the program.");
            Console.WriteLine("'reboot' to reboot the system.");
        }

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

        public bool isAllowed (User currentUser, User userToChange, UserList users, string action)
        {
            if (currentUser != null)
            {
                int userRole = currentUser.GetRole();
                int userToChangeRole = userToChange.GetRole();
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

        static bool CheckRights(int currentUserRole, int userToChangeRole)
        {
            if (currentUserRole == 3) return false;
            else if (currentUserRole == 2)
            {
                return userToChangeRole == 3 ? true : false;
            }
            else if (currentUserRole == 1)
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
            else if (currentUserRole == 0)
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

        public bool IsValidRole(int role)
        {
            return role == 1 || role == 2 || role == 3;
        }

    }
}
