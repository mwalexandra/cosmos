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
            Console.WriteLine("# User management:");
            WriteColored("userlist", ConsoleColor.Yellow);
            Console.WriteLine(" to display all users.");
            WriteColored("create", ConsoleColor.Yellow);
            Console.WriteLine(" to create a user and password.");
            WriteColored("changeuser", ConsoleColor.Yellow);
            Console.WriteLine(" to change an authorized user.");
            WriteColored("delete", ConsoleColor.Yellow);
            Console.WriteLine(" to delete a user.");
            WriteColored("rename", ConsoleColor.Yellow);
            Console.WriteLine(" to rename a user.");
            WriteColored("repass", ConsoleColor.Yellow);
            Console.WriteLine(" to change a password.");
            WriteColored("rerole", ConsoleColor.Yellow);
            Console.WriteLine(" to change a role of any user.");
            
            Console.WriteLine("# Lists management:");
            WriteColored("createlist", ConsoleColor.Yellow);
            Console.WriteLine(" to create new list of users.");
            WriteColored("deletelist", ConsoleColor.Yellow);
            Console.WriteLine(" to delete any list of users.");
            WriteColored("accesslist", ConsoleColor.Yellow);
            Console.WriteLine(" to get access information about a list of users.");
            WriteColored("movelist", ConsoleColor.Yellow);
            Console.WriteLine(" to move a list of users.");
            WriteColored("editlist", ConsoleColor.Yellow);
            Console.WriteLine(" to edit a list of users.");
            WriteColored("showlists", ConsoleColor.Yellow);
            Console.WriteLine(" to show a list of users.");
            
            Console.WriteLine("# System mamagement:");
            WriteColored("exit", ConsoleColor.Yellow);
            Console.WriteLine(" to stop the program.");
            WriteColored("reboot", ConsoleColor.Yellow);
            Console.WriteLine(" to reboot the system.");
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

        public void WriteColored(string coloredText, ConsoleColor color)
        {
            // saving of the previous colour 
            var previousColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(coloredText);
            Console.ForegroundColor = previousColor;
        }

        public void ShowPrompt(string currentUserName)
        {
            WriteColored($"{currentUserName}@NBVOS ?> ", ConsoleColor.Green);
        }
        
    }
}
