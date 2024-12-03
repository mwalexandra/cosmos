using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung
{
    internal class List
    {
        public string Name { get; }
        public string OpenRole { get; }
        public string EditRole { get; }
        public string MoveRole { get; }
        public string DeleteRole { get; }

        public List(string name, string openRole, string editRole, string moveRole, string deleteRole)
        {
            Name = name;
            OpenRole = openRole;
            EditRole = editRole;
            MoveRole = moveRole;
            DeleteRole = deleteRole;
        }
    }
}