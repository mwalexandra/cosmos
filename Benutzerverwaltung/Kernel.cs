using Benutzerverwaltung;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Sys = Cosmos.System;

namespace Benutzerverwaltung
{
    public class Kernel : Sys.Kernel
    {
        // Liste zum Speichern der Benutzer
        private static List<User> users = new List<User>();
        static Helpers helpers = new Helpers();

        protected override void BeforeRun()
        {
            // TODO Memory
            users.Add(new User("root", "root", 0));

            Console.WriteLine("Cosmos booted successfully.");
            Console.WriteLine("******************************************");
            Console.WriteLine("Type 'createuser' to create a user and password.");
            Console.WriteLine("Type 'userlist' to display all users.");
            Console.WriteLine("Type 'delete' to delete a user.");
            Console.WriteLine("Type 'rename' to rename a user.");
            Console.WriteLine("Type 'repass' to change a password.");
            Console.WriteLine("Type 'changerole' to change a role of any user.");
            Console.WriteLine("Type 'helpcreateuser' to get help.");
            Console.WriteLine("Type 'helpdeleteuser' to get help.");
            Console.WriteLine("Type 'exit' to stop the program.");
        }

        protected override void Run()
        {
            // Wartet auf Benutzereingabe
            while (true)
            {
                string command = Console.ReadLine();

                // Befehl auswerten
                if (command.Equals("createuser", StringComparison.OrdinalIgnoreCase))
                {
                    CreateUsernameAndPassword();
                }
                else if (command.Equals("userlist", StringComparison.OrdinalIgnoreCase))
                {
                    DisplayUserList();
                }
                else if (command.Equals("deleteuser", StringComparison.OrdinalIgnoreCase))
                {
                    DeleteUser();
                }
                else if (command.Equals("rename", StringComparison.OrdinalIgnoreCase))
                {
                    RenameUser();
                }
                else if (command.Equals("repass", StringComparison.OrdinalIgnoreCase))
                {
                    ChangePassword();
                }
                else if (command.Equals("changerole", StringComparison.OrdinalIgnoreCase))
                {
                    ChangeRole();
                }
                else if (command.Equals("helpcreateuser", StringComparison.OrdinalIgnoreCase))
                {
                    helpers.HelpCreateUser();
                }
                else if (command.Equals("helpdeleteuser", StringComparison.OrdinalIgnoreCase))
                {
                    helpers.HelpDeleteUser();
                }
                else if (command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Exit();
                }
                else
                {
                    Console.WriteLine("Unbekannter Befehl. Bitte versuche es erneut.");
                }
            }
        }

        static void CreateUsernameAndPassword()
        {
            Console.WriteLine("User- und Passworterstellung");

            string username = "";
            while (!helpers.IsValidInput(username))
            {
                Console.Write("Bitte gebe einen Benutzernamen ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                username = Console.ReadLine();
                if (!helpers.IsValidInput(username))
                {
                    Console.WriteLine("Ungültiger Benutzername. Versuche es nochmal.");
                }
            }

            string password = "";
            while (!helpers.IsValidInput(password))
            {
                Console.Write("Bitte gebe ein Passwort ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                password = Console.ReadLine();
                if (!helpers.IsValidInput(password))
                {
                    Console.WriteLine("Ungültiges Passwort. Versuche es nochmal.");
                }
            }

        
            // Benutzer zur Liste hinzufügen
            User newUser = new User(username, password, 3); // default role - User
            if (helpers.isAllowed(newUser, users, "createuser"))
            {
                users.Add(newUser);
                Console.WriteLine("Username and password were created successfully");
                Console.WriteLine($"Username: {username}");
                Console.WriteLine($"Password: {password}");
            } else
            {
                Console.WriteLine("You have not a permissions");
                Console.WriteLine("New user didn't create");
            }
        }

        static void DisplayUserList()
        {
            Console.WriteLine("User list:");

            if (users.Count == 0)
            {
                Console.WriteLine("User list is empty");
            }
            else
            {
                foreach (var User in users)
                {
                    Console.WriteLine($"User name: {User.GetUsername()}, role: {User.GetRole()}");
                    Console.WriteLine("__________________");
                }
            }
        }

        static void DeleteUser()
        {
            Console.Write("Enter the username you want to delete: ");
            string usernameToDelete = Console.ReadLine();
            User userToDelete = users.Find(user => user.GetUsername().Equals(usernameToDelete, StringComparison.OrdinalIgnoreCase));

            if (userToDelete == null)
            {
                Console.WriteLine("User was not found.");
                return;
            }

            if (helpers.isAllowed(userToDelete, users, "deleteuser"))
            {
                Console.Write($"Are you sure you want to delete the user '{usernameToDelete}'? (y/n): ");
                string confirmation = Console.ReadLine();

                if (confirmation.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    users.Remove(userToDelete);
                    Console.WriteLine($"User '{usernameToDelete}' was deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Deletion process aborted.");
                }
            }
            else
                Console.Write("You have no permissions");
        }

        static void RenameUser()
        {
            Console.Write("Enter the username you want to modify: ");
            string currentUsername = Console.ReadLine();
            User userToRename = users.Find(user => user.GetUsername().Equals(currentUsername, StringComparison.OrdinalIgnoreCase));

            if (userToRename == null)
            {
                Console.WriteLine("User was not found.");
                return;
            }
            if (helpers.isAllowed(userToRename, users, "reuser"))
            {
                Console.Write("Enter the new username (at least 4 characters, only Latin letters): ");
                string newUsername = Console.ReadLine();

                if (!helpers.IsValidInput(newUsername))
                {
                    Console.WriteLine("Invalid new username.");
                    return;
                }

                userToRename.SetUsername(newUsername);
                Console.WriteLine($"Username was to '{newUsername}' changed.");
            }
            else
                Console.Write("You have no permissions");
        }

        static void ChangePassword()
        {
            Console.Write("Enter the username whose password you want to change: ");
            string username = Console.ReadLine();
            User userToChangePassword = users.Find(user => user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

            if (userToChangePassword == null)
            {
                Console.WriteLine("User was not found.");
                return;
            }

            if (helpers.isAllowed(userToChangePassword, users, "repassword"))
            {
                Console.Write("Enter the new password (at least 4 characters, only Latin letters): ");
                string newPassword = Console.ReadLine();

                if (!helpers.IsValidInput(newPassword))
                {
                    Console.WriteLine("Invalid new password.");
                    return;
                }

                userToChangePassword.SetPassword(newPassword);
                Console.WriteLine("Password was changed successfully.");
            }
            else
                Console.Write("You have no permissions");
        }

        static void ChangeRole()
        {
            Console.WriteLine("Enter the username whose role you want to change: ");
            string username = Console.ReadLine();
            User userToChangeRole = users.Find(user => user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

            if (userToChangeRole == null)
            {
                Console.WriteLine("User was not found.");
                return;
            }

            if (helpers.isAllowed(userToChangeRole, users, "changerole"))
            {
                Console.Write("Enter the number of the new role (admin - 1, fuser - 2, user - 3): ");
                string role = Console.ReadLine();
                int.TryParse(role, out int roleNum);

                Console.WriteLine(roleNum);

                if (roleNum == 1 || roleNum == 2 || roleNum == 3)
                {
                    userToChangeRole.SetRole(roleNum);
                    Console.WriteLine("Role was changed succesfully.");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            else
                Console.Write("You have no permissions");
        }

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
