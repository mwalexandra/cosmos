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
        // List for storing users
        private static List users = new UserList();
        private static User currentUser;
        private static User rootUser("root", "root", "Root");
        private static List<List> lists = new List<List>();

        private static Utils utils = new Utils();
        private static ListUtils listUtils = new ListUtils();
        private static Helpers helpers = new Helpers();

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos OS booted successfully.");
        }

        private void LoginRootUser()
        {
            System.Console.WriteLine("Please enter the password for the root user:");
            string password = System.Console.ReadLine();
            if (rootUser.Password == password)
            {
                currentUser = rootUser;
                System.Console.WriteLine("Root user successfully logged in.");
            }
            else
            {
                System.Console.WriteLine("Incorrect password.");
                LoginRootUser();
            }
        }

        protected override void Run()
        {
            // Waiting for user input
            while (true)
            {
                System.Console.WriteLine($"{currentUser.Username}, Please enter a command.\n'help' for getting help\nor 'exit' to stop the OS");
                string command = Console.ReadLine();

                if (command.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    helpers.showHelp();
                }
                // user management
                else if (command.Equals("create", StringComparison.OrdinalIgnoreCase))
                {
                    utils.CreateUsernameAndPassword(currentUser, users);
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
                else if (command.Equals("changeuser", StringComparison.OrdinalIgnoreCase))
                {
                    utils.ChangeUser(users);
                }
                // lists management
                else if (command.Equals("userlist", StringComparison.OrdinalIgnoreCase))
                {
                    utils.DisplayUserList(users);
                }
                else if (command.Equals("createlist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists();
                    listUtils.CreateList(currentUser);
                }
                else if (command.Equals("deletelist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists();
                    listUtils.DeleteList();
                }
                else if (command.Equals("accesslist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists();
                    listUtils.AccessList();
                }
                else if (command.Equals("movelist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists();
                    listUtils.MoveList();
                }
                else if (command.Equals("editlist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists();
                    listUtils.EditList();
                }
                else if (command.Equals("showlists", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists();
                }
                else if (command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Exit();
                }
                else if (command == "reboot")
                {
                    RebootSystem();
                }
                else
                {
                    Console.WriteLine("Unknown command. Please try again.");
                }
            }
        }
        static void Exit()
        {
            Environment.Exit(0);
        }

        private void RebootSystem()
        {
            users.Clear();
            lists.Clear();
            System.Console.WriteLine("System wird neu gestartet...");
        }
    }
}
