namespace Employee_hub_new.Models
{
    public static class RoleConstants
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Employee = "Employee";

        public static readonly string[] AllRoles = new[] { Admin, Manager, Employee };

        public static class Permissions
        {
            public static class Users
            {
                public const string View = "Users.View";
                public const string Create = "Users.Create";
                public const string Edit = "Users.Edit";
                public const string Delete = "Users.Delete";
            }

            public static class Messages
            {
                public const string Send = "Messages.Send";
                public const string ViewAll = "Messages.ViewAll";
                public const string ViewOwn = "Messages.ViewOwn";
            }

            public static class Employees
            {
                public const string ViewAll = "Employees.ViewAll";
                public const string ViewOwn = "Employees.ViewOwn";
                public const string Manage = "Employees.Manage";
            }
        }

        public static Dictionary<string, string[]> RolePermissions = new()
        {
            {
                Admin, new[]
                {
                    Permissions.Users.View,
                    Permissions.Users.Create,
                    Permissions.Users.Edit,
                    Permissions.Users.Delete,
                    Permissions.Messages.Send,
                    Permissions.Messages.ViewAll,
                    Permissions.Messages.ViewOwn,
                    Permissions.Employees.ViewAll,
                    Permissions.Employees.ViewOwn,
                    Permissions.Employees.Manage
                }
            },
            {
                Manager, new[]
                {
                    Permissions.Messages.Send,
                    Permissions.Messages.ViewOwn,
                    Permissions.Employees.ViewAll,
                    Permissions.Employees.ViewOwn
                }
            },
            {
                Employee, new[]
                {
                    Permissions.Messages.Send,
                    Permissions.Messages.ViewOwn,
                    Permissions.Employees.ViewOwn
                }
            }
        };
    }
} 