namespace CarShop.Common
{
    public static class Const
    {
        //User
        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 20;
        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;
        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string Mechanic = nameof(Mechanic);
        public const string Client = nameof(Client);

        //Car
        public const int ModelMinLength = 5;
        public const int ModelMaxLength = 20;
        public const string PlateNumberRegex = @"^[A-Z]{2}\d{4}[A-Z]{2}$";
        public const int PlateNumberMaxLength = 8;
     
        //Issue
        public const int DescriptionMinLength = 5;
    }
}
