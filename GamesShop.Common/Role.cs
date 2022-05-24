namespace GamesShop.Common
{
    public static class Role
    {
        public const string UserRole = "User";
        public const string AdministratorRole = "Administrator";
        private static IEnumerable<string> roles =>
            new[]
            {
                UserRole,
                AdministratorRole
            };

        public static async Task<IEnumerable<string>> GetRolesAsync()
        {
            return roles;
        }
    }
}