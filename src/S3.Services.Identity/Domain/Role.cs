
//namespace S3.Services.Identity.Domain
//{
//    public static class Role
//    {
//        public const string SuperAdmin = "Super Admin";
//        public const string Admin = "Admin";
//        public const string SchoolAdmin = "School Admin";
//        public const string AssistantSchoolAdmin = "Assistant School Admin";
//        public const string Teacher = "Teacher";
//        public const string Parent = "Parent";
//        public const string Student = "Student";
        
//        // Lower case version for comparison purpose only
//        private const string super_admin = "super admin";
//        private const string admin = "admin";
//        private const string school_admin = "school admin";
//        private const string assistant_school_admin = "assistant school admin";
//        private const string teacher = "teacher";
//        private const string parent = "parent";
//        private const string student = "student";

//        public static bool IsValid(string role) // Use pattern matching
//            => role.ToLowerInvariant() switch
//            {
//                super_admin => true,
//                admin => true,
//                school_admin => true,
//                assistant_school_admin => true,
//                teacher => true,
//                parent => true,
//                student => true,
//                _ => false
//            };
//    }
//}


////using System;
////using System.Collections.Generic;
////using System.ComponentModel.DataAnnotations;
////namespace S3.Services.Identity.Domain
////{
////    public class Role
////    {
////        public static List<string> availableRoles = new List<string>
////        { "Super Admin", "Admin", "School Admin", "Assistant School Admin",
////         "Teacher", "Parent", "Student" };

////        public static bool IsValid(string role)
////        {
////            if (string.IsNullOrWhiteSpace(role))
////                return false;

////            return availableRoles.Contains(role);
////        }
////    }
////}