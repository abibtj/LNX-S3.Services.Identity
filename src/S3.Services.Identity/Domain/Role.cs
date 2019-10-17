
namespace S3.Services.Identity.Domain
{
    public static class Role
    {
        public const string SuperAdmin = "super admin";
        public const string Admin = "admin";
        public const string SchoolAdmin = "school admin";
        public const string AssistantSchoolAdmin = "assistant school admin";
        public const string Teacher = "teacher";
        public const string Parent = "parent";
        public const string Student = "student";

        //public static bool IsValid(string role)
        //{
        //    if (string.IsNullOrWhiteSpace(role))
        //        return false;

        //    role = role.ToLowerInvariant();

        //    return role == SuperAdmin || role == Admin || role == SchoolAdmin
        //        || role == AssistantSchoolAdmin || role == Teacher
        //        || role == Parent || role == Student;
        //}

        public static bool IsValid(string role) // Use pattern matching
            => role.ToLowerInvariant() switch
            {
                SuperAdmin => true,
                Admin => true,
                SchoolAdmin => true,
                AssistantSchoolAdmin => true,
                Teacher => true,
                Parent => true,
                Student => true,
                _ => false
            };
    }
}


//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//namespace S3.Services.Identity.Domain
//{
//    public class Role
//    {
//        public static List<string> availableRoles = new List<string>
//        { "Super Admin", "Admin", "School Admin", "Assistant School Admin",
//         "Teacher", "Parent", "Student" };

//        public static bool IsValid(string role)
//        {
//            if (string.IsNullOrWhiteSpace(role))
//                return false;

//            return availableRoles.Contains(role);
//        }
//    }
//}