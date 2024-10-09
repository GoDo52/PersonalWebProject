using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Utility
{
    public static class SD
    {
        private const int V = 3;
        public const string AdminRole = "Admin";
        public static string[] RoleNames = { "Admin", "Manager", "User" };
        public static int DefaultRoleId = V;
        //public static Dictionary<int, string> RoleNames1 = new Dictionary<int, string>();
    }
}
