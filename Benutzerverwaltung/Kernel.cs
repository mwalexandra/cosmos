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
        Utils utils = new Utils();
        Helpers helpers = new Helpers();

        protected override void BeforeRun()
        {
            // TODO Memory
            users.Add(new User("root", "root", 0));

            Console.WriteLine("Cosmos OS booted successfully.");
            Console.WriteLine("Type 'help' for getting help\nor 'exit' to stop the OS");
        }

        protected override void Run()
        {
            // Wartet auf Benutzereingabe
            while (true)
            {
                string command = Console.ReadLine();

                // Befehl auswerten
                if (command.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("'userlist' to display all users.");
                    Console.WriteLine("'create' to create a user and password.");
                    Console.WriteLine("'delete' to delete a user.");
                    Console.WriteLine("'rename' to rename a user.");
                    Console.WriteLine("'repass' to change a password.");
                    Console.WriteLine("'rerole' to change a role of any user.");
                    Console.WriteLine("'helpcreate' to get help.");
                    Console.WriteLine("'helpdelete' to get help.");
                    Console.WriteLine("'exit' to stop the program.");
                }
                else if (command.Equals("create", StringComparison.OrdinalIgnoreCase))
                {
                    utils.CreateUsernameAndPassword(users);
                }
                else if (command.Equals("userlist", StringComparison.OrdinalIgnoreCase))
                {
                    utils.DisplayUserList(users);
                }
                else if (command.Equals("delete", StringComparison.OrdinalIgnoreCase))
                {
                    utils.DeleteUser(users);
                }
                else if (command.Equals("rename", StringComparison.OrdinalIgnoreCase))
                {
                    utils.RenameUser(users);
                }
                else if (command.Equals("repass", StringComparison.OrdinalIgnoreCase))
                {
                    utils.ChangePassword(users);
                }
                else if (command.Equals("rerole", StringComparison.OrdinalIgnoreCase))
                {
                    utils.ChangeRole(users);
                }
                else if (command.Equals("helpcreate", StringComparison.OrdinalIgnoreCase))
                {
                    helpers.HelpCreateUser();
                }
                else if (command.Equals("helpdelete", StringComparison.OrdinalIgnoreCase))
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
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
