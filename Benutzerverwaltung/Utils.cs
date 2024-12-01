using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung
{
    internal class Utils
    {
        static Helpers helpers = new Helpers();

        public void CreateUsernameAndPassword(List<User> users)
        {
            Console.WriteLine("Username and password creation:");

            string username = "";
            while (!helpers.IsValidInput(username))
            {
                Console.Write("Please enter a username (at least 4 characters, Latin letters only): ");
                username = Console.ReadLine();
                if (!helpers.IsValidInput(username))
                {
                    Console.WriteLine("Invalid username. Please try again.");
                }
            }

            string password = "";
            while (!helpers.IsValidInput(password))
            {
                Console.Write("Please enter a password (at least 4 characters, Latin letters only): ");
                password = Console.ReadLine();
                if (!helpers.IsValidInput(password))
                {
                    Console.WriteLine("Invalid password. Please try again.");
                }
            }


            // Benutzer zur Liste hinzufügen
            User newUser = new User(username, password, 3); // default role - User
            if (helpers.isAllowed(newUser, users, "create"))
            {
                users.Add(newUser);
                Console.WriteLine("Username and password were created successfully");
                Console.WriteLine($"Username: {username}");
                Console.WriteLine($"Password: {password}");
            }
            else
            {
                Console.WriteLine("You have not a permissions.");
                Console.WriteLine("New user was not created.");
            }
        }

        public void DisplayUserList(List<User> users)
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
                    Console.WriteLine($"User: {User.GetUsername()}, role: {User.RoleToString(User.GetRole())}");
                }
            }
        }

        public void DeleteUser(List<User> users)
        {
            Console.Write("Enter the username you want to delete: ");
            string usernameToDelete = Console.ReadLine();
            User userToDelete = users.Find(user => user.GetUsername().Equals(usernameToDelete, StringComparison.OrdinalIgnoreCase));

            if (userToDelete == null)
            {
                Console.WriteLine("User was not found.");
                return;
            }

            if (helpers.isAllowed(userToDelete, users, "delete"))
            {
                Console.WriteLine($"Are you sure you want to delete the user '{usernameToDelete}'? (y/n): ");
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
                Console.WriteLine("You have no permissions.");
        }

        public void RenameUser(List<User> users)
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

        public void ChangePassword(List<User> users)
        {
            Console.Write("Enter the username whose password you want to change: ");
            string username = Console.ReadLine();
            User userToChangePassword = users.Find(user => user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

            if (userToChangePassword == null)
            {
                Console.WriteLine("User was not found.");
                return;
            }

            if (helpers.isAllowed(userToChangePassword, users, "repass"))
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

        public void ChangeRole(List<User> users)
        {
            Console.WriteLine("Enter the username whose role you want to change: ");
            string username = Console.ReadLine();
            User userToChangeRole = users.Find(user => user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

            if (userToChangeRole == null)
            {
                Console.WriteLine("User was not found.");
                return;
            }

            if (helpers.isAllowed(userToChangeRole, users, "rerole"))
            {
                Console.Write("Enter the number of the new role (Fachadmin - 1, Fachuser - 2, User - 3): ");
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
                    Console.WriteLine("Invalid input.");
                }
            }
            else
                Console.Write("You have no permissions.");
        }
    }
}
