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
        // 
        private static User currentUser = new User("initUser", "initUser", 3);
        private static User rootUser = new User("root", "root", 0);
        //
        private static UserList users = new UserList();
        private static List<List> lists = new List<List>();
        // 
        private static UserUtils userUtils = new UserUtils();
        private static ListsUtils listUtils = new ListsUtils();
        private static SysUtils sysUtils = new SysUtils();
        private static Helpers helpers = new Helpers();

        protected override void BeforeRun()
        {
            Console.WriteLine("NBV OS booted successfully.");
            LoginRootUser();
        }

        protected override void Run()
        {
            // Waiting for user input
            while (true)
            {
                helpers.WriteColored("help ", ConsoleColor.Yellow);
                Console.WriteLine("for getting help or ");
                helpers.WriteColored("exit ", ConsoleColor.Yellow);
                Console.WriteLine("to stop the OS");
                helpers.ShowPrompt(currentUser.GetUsername());
                string command = Console.ReadLine();

                if (command.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    helpers.showHelp();
                }
                // user management
                else if (command.Equals("create", StringComparison.OrdinalIgnoreCase))
                {
                    userUtils.CreateUsernameAndPassword(currentUser, users);
                }
                else if (command.Equals("delete", StringComparison.OrdinalIgnoreCase))
                {
                    userUtils.DeleteUser(currentUser, users);
                }
                else if (command.Equals("rename", StringComparison.OrdinalIgnoreCase))
                {
                    userUtils.RenameUser(currentUser, users);
                }
                else if (command.Equals("repass", StringComparison.OrdinalIgnoreCase))
                {
                    userUtils.ChangePassword(currentUser, users);
                } 
                else if (command.Equals("rerole", StringComparison.OrdinalIgnoreCase))
                {
                    userUtils.ChangeRole(currentUser, users);
                }
                else if (command.Equals("changeuser", StringComparison.OrdinalIgnoreCase))
                {
                    User userToChange = userUtils.ChangeUser(users);
                    if (userToChange != null)
                    {
                        currentUser = userToChange;
                        System.Console.WriteLine("User successfully switched to " + userToChange.GetUsername());
                    }
                }
                    // lists management
                    else if (command.Equals("userlist", StringComparison.OrdinalIgnoreCase))
                {
                    userUtils.DisplayUserList(users);
                }
                else if (command.Equals("createlist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists(lists);
                    listUtils.CreateList(currentUser, lists);
                }
                else if (command.Equals("deletelist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists(lists);
                    listUtils.DeleteList(currentUser, lists);
                }
                else if (command.Equals("accesslist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists(lists);
                    listUtils.AccessList(currentUser, lists);
                }
                else if (command.Equals("movelist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists(lists);
                    listUtils.MoveList(currentUser, lists);
                }
                else if (command.Equals("editlist", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists(lists);
                    listUtils.EditList(currentUser, lists);
                }
                else if (command.Equals("showlists", StringComparison.OrdinalIgnoreCase))
                {
                    listUtils.ShowLists(lists);
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
                    helpers.WriteColored("Unknown command. Please try again.", ConsoleColor.Yellow);
                }
            }
        }
        static void LoginRootUser()
        {
            System.Console.WriteLine("Please enter the password for the root user:");
            string password = sysUtils.ReadPassword();
            if (rootUser.GetPassword() == password)
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
        static void Exit()
        {
            Environment.Exit(0);
        }

        private void RebootSystem()
        {
            users.Clear();
            lists.Clear();
            System.Console.WriteLine("System will restart...");
            LoginRootUser();
        }
    }
}
