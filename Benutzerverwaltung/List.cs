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
        public int OpenRole { get; }
        public int EditRole { get; }
        public int MoveRole { get; }
        public int DeleteRole { get; }

        public List(string name, int openRole, int editRole, int moveRole, int deleteRole)
        {
            Name = name;
            OpenRole = openRole;
            EditRole = editRole;
            MoveRole = moveRole;
            DeleteRole = deleteRole;
        }
    }
}