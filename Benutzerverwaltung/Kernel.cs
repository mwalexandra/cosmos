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
                else if (command.Equals("reuser", StringComparison.OrdinalIgnoreCase))
                {
                    RenameUser();
                }
                else if (command.Equals("repassword", StringComparison.OrdinalIgnoreCase))
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
            users.Add(new User(username, password, 3)); // default role - User

            Console.WriteLine("Benutzername und Passwort erfolgreich erstellt!");
            Console.WriteLine($"Benutzername: {username}");
            Console.WriteLine($"Passwort: {password}");
        }

        static void DisplayUserList()
        {
            Console.WriteLine("Benutzerliste:");

            if (users.Count == 0)
            {
                Console.WriteLine("Keine Benutzer vorhanden.");
            }
            else
            {
                foreach (var User in users)
                {
                    Console.WriteLine($"Benutzername: {User.GetUsername()}, {User.GetRole()}");
                    Console.WriteLine("__________________");
                }
            }
        }

        static void DeleteUser()
        {
            Console.Write("Gebe den Benutzernamen ein, den du löschen möchtest: ");
            string usernameToDelete = Console.ReadLine();
            User userToDelete = users.Find(user => user.GetUsername().Equals(usernameToDelete, StringComparison.OrdinalIgnoreCase));

            if (userToDelete == null)
            {
                Console.WriteLine("Benutzer nicht gefunden.");
                return;
            }

            if (helpers.isAllowed(userToDelete, users))
            {
                Console.Write($"Bist du sicher, dass du den Benutzer '{usernameToDelete}' löschen möchtest? (y/n): ");
                string confirmation = Console.ReadLine();

                if (confirmation.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    users.Remove(userToDelete);
                    Console.WriteLine($"Benutzer '{usernameToDelete}' wurde gelöscht.");
                }
                else
                {
                    Console.WriteLine("Löschvorgang abgebrochen.");
                }
            }
            else
                Console.Write("You have no permissions");
        }

        static void RenameUser()
        {
            Console.Write("Gebe den Benutzernamen ein, den du ändern möchtest: ");
            string currentUsername = Console.ReadLine();
            User userToRename = users.Find(user => user.GetUsername().Equals(currentUsername, StringComparison.OrdinalIgnoreCase));

            if (userToRename == null)
            {
                Console.WriteLine("Benutzer nicht gefunden.");
                return;
            }
            if (helpers.isAllowed(userToRename, users))
            {
                Console.Write("Gebe den neuen Benutzernamen ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                string newUsername = Console.ReadLine();

                if (!helpers.IsValidInput(newUsername))
                {
                    Console.WriteLine("Ungültiger neuer Benutzername.");
                    return;
                }

                userToRename.SetUsername(newUsername);
                Console.WriteLine($"Benutzername wurde zu '{newUsername}' geändert.");
            }
            else
                Console.Write("You have no permissions");
        }

        static void ChangePassword()
        {
            Console.Write("Gebe den Benutzernamen ein, dessen Passwort du ändern möchtest: ");
            string username = Console.ReadLine();
            User userToChangePassword = users.Find(user => user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

            if (userToChangePassword == null)
            {
                Console.WriteLine("Benutzer nicht gefunden.");
                return;
            }

            if (helpers.isAllowed(userToChangePassword, users))
            {
                Console.Write("Gebe das neue Passwort ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                string newPassword = Console.ReadLine();

                if (!helpers.IsValidInput(newPassword))
                {
                    Console.WriteLine("Ungültiges neues Passwort.");
                    return;
                }

                userToChangePassword.SetPassword(newPassword);
                Console.WriteLine("Passwort erfolgreich geändert.");
            }
            else
                Console.Write("You have no permissions");
        }

        static void ChangeRole()
        {
            Console.WriteLine("Gebe den Benutzername ein, dessen Rolle du ändern möchtest: ");
            string username = Console.ReadLine();
            User userToChangeRole = users.Find(user => user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

            if (userToChangeRole == null)
            {
                Console.WriteLine("Benutzer nicht gefunden.");
                return;
            }

            if (helpers.isAllowed(userToChangeRole, users))
            {
                Console.Write("Gebe die Nummer der neuen Rolle ein (fadmin - 1, fuser - 2, user - 3): ");
                string role = Console.ReadLine();
                int.TryParse(role, out int roleNum);

                Console.WriteLine(roleNum);

                if (roleNum == 1 || roleNum == 2 || roleNum == 3)
                {
                    userToChangeRole.SetRole(roleNum);
                    Console.WriteLine("Rolle erfolgreich geändert.");
                }
                else
                {
                    Console.WriteLine("falsche Eingabe");
                    Console.WriteLine("Ende des Rollentausches");
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
