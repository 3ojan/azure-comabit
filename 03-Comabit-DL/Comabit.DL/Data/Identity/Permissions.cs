using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Infrastructure
{
    public class Permissions
    {
        // Allgemein / System:

        public const string SystemSettings = "system.settings";

        // Benutzer / Rollen:

        public const string UserList = "user.list";
        public const string UserCreate = "user.create";
        public const string UserEdit = "user.edit";
        public const string UserDelete = "user.delete";

        public const string RoleList = "role.list";
        public const string RoleCreate = "role.create";
        public const string RoleEdit = "role.edit";
        public const string RoleDelete = "role.delete";

        public const string CompanyList = "company.list";
        public const string CompanyDelete = "company.delete";
        public const string CompanyEdit = "company.edit";

        public const string PermissionList = "permission.list";
        public const string PermissionEdit = "permission.edit";

        public const string LogedIn = "logedIn";

        // Allgemein Bereiche:

        public const string AreaBackend = "area.backend";

        private static List<Permission> _allPermissions = null;

        public static List<Permission> AllPermissions
        {
            get
            {
                if (_allPermissions == null)
                {
                    _allPermissions = new List<Permission>()
                    {
                        new Permission(SystemSettings, "System Einstellungen", "System"),
                        
                        new Permission(UserList, "Benutzer Liste", "Authentifizierung"),
                        new Permission(UserCreate, "Benutzer erstellen", "Authentifizierung"),
                        new Permission(UserEdit, "Benutzer bearbeiten", "Authentifizierung"),
                        new Permission(UserDelete, "Benutzer löschen", "Authentifizierung"),

                        new Permission(RoleList, "Rollen Liste", "Authentifizierung"),
                        new Permission(RoleCreate, "Rollen erstellen", "Authentifizierung"),
                        new Permission(RoleEdit, "Rollen bearbeiten", "Authentifizierung"),
                        new Permission(RoleDelete, "Rollen löschen", "Authentifizierung"),

                        new Permission(PermissionList, "Berechtigungen Liste", "Authentifizierung"),
                        new Permission(PermissionEdit, "Berechtigungen bearbeiten", "Authentifizierung"),

                        new Permission(CompanyList, "Firmen Liste", "Firmen"),
                        new Permission(CompanyDelete, "Firmen löschen", "Firmen"),
                        new Permission(CompanyEdit, "Firmen bearbeiten", "Firmen"),

                        new Permission(AreaBackend, "Zugang Backend (Startseite)", "Authentifizierung"),

                        new Permission(LogedIn, "Kein Besucher", "Authentifizierung"),
                    };
                }

                return _allPermissions;
            }
        }

        private static string GetPermission(string value)
        {
            return AllPermissions.Where(p => p.Value == value).FirstOrDefault()?.Value;
        }

        public static List<string> GetPermissionGroups()
        {
            return AllPermissions.Select(p => p.GroupName).Distinct().OrderBy(g => g).ToList();
        }
    }

    public class Permission
    {
        public string Value { get; set; }

        public string Description { get; set; }

        public string GroupName { get; set; }

        public Permission(string value, string description, string groupName)
        {
            Value = value;
            Description = description;
            GroupName = groupName;
        }
    }
}