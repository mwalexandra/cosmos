using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("Geben Sie ein Username an (4-16 Zeichen, nur lateinische Buchstaben).");
            Console.WriteLine("Geben Sie ein Passwort an (4-16 Zeichen, nur lateinische Buchstaben).");
        }

        public void HelpDeleteUser()
        {
            Console.WriteLine("Gerben Sie den zu Löschenden User ein und bestätigen Sie die eingabe");
        }


        public string EnumToString(Enum enumToString)
        {
            return enumToString.ToString();
        }

        public bool isAllowed (User userToChange, List<User> users, string action)
        {
            Console.Write("Input your username");
            string userName = Console.ReadLine();
            User user = users.Find(userItem => userItem.GetUsername().Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                int userRole = user.GetRole();
                int userToChangeRole = userToChange.GetRole();

                if (userToChangeRole == 3)
                {
                    // TODO
                    /* switch (action)
                    {
                        case ''
                    } */
                }
                else if (userToChangeRole == 2)
                {
                    switch (userRole)
                    {
                        case 0:
                        case 1:
                            return true;
                        default:
                            return false;
                    }
                }
                else if (userToChangeRole == 1)
                {
                    switch (userRole)
                    {
                        case 0:
                            return true;
                        default:
                            return false;
                    }
                }
                else if (userToChangeRole == 0)
                {
                    return userRole == 0 ? true : false;
                }
            } else
            {
                Console.Write("User was not found");
            }
            return false;
        }

    }
}
