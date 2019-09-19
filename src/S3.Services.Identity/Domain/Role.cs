namespace S3.Services.Identity.Domain
{
    public static class Role
    {
        public const string SuperAdmin = "superadmin";
        public const string Admin = "admin";
        public const string SchoolAdmin = "schooladmin";
        public const string Teacher = "teacher";
        public const string Parent = "parent";
        public const string Student = "student";

        public static bool IsValid(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return false;
            }

            role = role.ToLowerInvariant();

            return role == SuperAdmin || role == Admin || role == SchoolAdmin
                || role == Teacher ||  role == Parent || role == Student;
        }
    }
}