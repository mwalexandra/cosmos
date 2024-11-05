using Benutzerverwaltung;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Benutzerverwaltung
{
    public class Kernel : Sys.Kernel
    {
        // Liste zum Speichern der Benutzer
        private static List<User> users = new List<User>();

        protected override void BeforeRun()
        {
            // TODO Memory
            users.Add(new User("admin", "admin"));

            Console.WriteLine("Cosmos booted successfully.");
            Console.WriteLine("******************************************");
            Console.WriteLine("Type 'create' to create a user and password.");
            Console.WriteLine("Type 'userlist' to display all users.");
            Console.WriteLine("Type 'delete' to delete a user.");
            Console.WriteLine("Type 'rename' to rename a user.");
            Console.WriteLine("Type 'repass' to change a password.");
            Console.WriteLine("Type 'helpcreateuser' to get help.");
            Console.WriteLine("Type 'helpdeleteuser' to get help.");
            Console.WriteLine("Type 'exit' to stop the program.");
        }

        protected override void Run()
        {
            Helpers helpers = new Helpers();
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
            Helpers helper = new Helpers();

            Console.WriteLine("User- und Passworterstellung");

            string username = "";
            while (!helper.IsValidInput(username))
            {
                Console.Write("Bitte gebe einen Benutzernamen ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                username = Console.ReadLine();
                if (!helper.IsValidInput(username))
                {
                    Console.WriteLine("Ungültiger Benutzername. Versuche es nochmal.");
                }
            }

            string password = "";
            while (!helper.IsValidInput(password))
            {
                Console.Write("Bitte gebe ein Passwort ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                password = Console.ReadLine();
                if (!helper.IsValidInput(password))
                {
                    Console.WriteLine("Ungültiges Passwort. Versuche es nochmal.");
                }
            }

            // Benutzer zur Liste hinzufügen
            users.Add(new User(username, password));

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
                    Console.WriteLine($"Benutzername: {User.GetUsername()}");
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

        static void RenameUser()
        {
            Helpers helpers = new Helpers();

            Console.Write("Gebe den Benutzernamen ein, den du ändern möchtest: ");
            string currentUsername = Console.ReadLine();
            User userToRename = users.Find(user => user.GetUsername().Equals(currentUsername, StringComparison.OrdinalIgnoreCase));

            if (userToRename == null)
            {
                Console.WriteLine("Benutzer nicht gefunden.");
                return;
            }

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

        static void ChangePassword()
        {
            Helpers helpers = new Helpers();

            Console.Write("Gebe den Benutzernamen ein, dessen Passwort du ändern möchtest: ");
            string username = Console.ReadLine();
            User userToChangePassword = users.Find(user => user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

            if (userToChangePassword == null)
            {
                Console.WriteLine("Benutzer nicht gefunden.");
                return;
            }

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

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
