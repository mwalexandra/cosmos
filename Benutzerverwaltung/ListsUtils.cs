using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung
{
    internal class ListsUtils
    {
        Helpers helpers = new Helpers();

            public void CreateList(User currentUser, List<List> lists)
            {
                if (currentUser.GetRole() != 0 && currentUser.GetRole() != 1)
                {
                    System.Console.WriteLine("The list can only be created by Root user or an Admin.");
                    return;
                }

                System.Console.WriteLine("List name:");
                string listName = System.Console.ReadLine();
                System.Console.WriteLine("Who can open the list (1 - Admin, 2 - Manager user, 3 - User)? Input the number");
                int openRole = int.Parse(System.Console.ReadLine());
                if (!helpers.IsValidRole(openRole))
                {
                    System.Console.WriteLine("Invalid role number. Only 1 for Admin, 2 for Manager user or 3 for User are allowed.");
                    return;
                }
                System.Console.WriteLine("Who can edit the list (1 - Admin, 2 - Manager user, 3 - User)?");
                int editRole = int.Parse(System.Console.ReadLine());
                if (!helpers.IsValidRole(editRole))
                {
                    System.Console.WriteLine("Invalid role number. Only 1 for Admin, 2 for Manager user or 3 for User are allowed.");
                    return;
                }
                System.Console.WriteLine("Who can move the list (1 - Admin, 2 - Manager user, 3 - User)?");
                int moveRole = int.Parse(System.Console.ReadLine());
                if (!helpers.IsValidRole(moveRole))
                {
                    System.Console.WriteLine("Invalid role. Only 1 for Admin, 2 for Manager user or 3 for User are allowed.");
                    return;
                }
                System.Console.WriteLine("Who can delete the list (1 - Admin, 2 - Manager user, 3 - User)?");
                int deleteRole = int.Parse(System.Console.ReadLine());
                if (!helpers.IsValidRole(deleteRole))
                {
                    System.Console.WriteLine("Invalid role. Only 1 for Admin, 2 for Manager user or 3 for User are allowed.");
                    return;
                }

                lists.Add(new List(listName, openRole, editRole, moveRole, deleteRole));
                System.Console.WriteLine("List " + listName + " has been created.");
            }

            public void DeleteList(User currentUser, List<List> lists)
            {
                System.Console.WriteLine("Name of the list:");
                string listName = System.Console.ReadLine();
                var listToDelete = lists.FirstOrDefault(l => l.Name == listName);
                if (listToDelete != null)
                {
                    if (currentUser.GetRole() == 0 || currentUser.GetRole() == listToDelete.DeleteRole)
                    {
                        lists.Remove(listToDelete);
                        System.Console.WriteLine("List " + listName + " was removed.");
                    }
                    else
                    {
                        System.Console.WriteLine("Deletion failed. Insufficient permissions.");
                    }
                }
                else
                {
                    System.Console.WriteLine("List was not found.");
                }
            }

            public void AccessList(User currentUser, List<List> lists)
            {
                System.Console.WriteLine("Name of the list:");
                string listName = System.Console.ReadLine();
                var listToAccess = lists.FirstOrDefault(l => l.Name == listName);
                if (listToAccess != null)
                {
                    if (currentUser.GetRole() == 0 || currentUser.GetRole() == listToAccess.OpenRole)
                    {
                        System.Console.WriteLine("You are allowed to open the list.");
                    }
                    else
                    {
                        System.Console.WriteLine("Access denied.");
                    }
                }
                else
                {
                    System.Console.WriteLine("List was not found.");
                }
            }

            public void MoveList(User currentUser, List<List> lists)
            {
                System.Console.WriteLine("Name of the list:");
                string listName = System.Console.ReadLine();
                var listToMove = lists.FirstOrDefault(l => l.Name == listName);
                if (listToMove != null)
                {
                    if (currentUser.GetRole() == 0 || currentUser.GetRole() == listToMove.MoveRole)
                    {
                        System.Console.WriteLine("You are allowed to move the list.");
                    }
                    else
                    {
                        System.Console.WriteLine("Move denied.");
                    }
                }
                else
                {
                    System.Console.WriteLine("List not found.");
                }
            }

            public void EditList(User currentUser, List<List> lists)
            {
                System.Console.WriteLine("Name of the list:");
                string listName = System.Console.ReadLine();
                var listToEdit = lists.FirstOrDefault(l => l.Name == listName);
                if (listToEdit != null)
                {
                    if (currentUser.GetRole() == 0 || currentUser.GetRole() == listToEdit.EditRole)
                    {
                        System.Console.WriteLine("You are allowed to edit the list.");
                    }
                    else
                    {
                        System.Console.WriteLine("Edit denied.");
                    }
                }
                else
                {
                    System.Console.WriteLine("List not found.");
                }
            }

            public void ShowLists(List<List> lists)
            {
                System.Console.WriteLine("Lists:");
                foreach (var list in lists)
                {
                    System.Console.WriteLine($"{list.Name} (Open: {list.OpenRole}, Edit: {list.EditRole}, Move: {list.MoveRole}, Delete: {list.DeleteRole})");
                }
            }
    }    
}