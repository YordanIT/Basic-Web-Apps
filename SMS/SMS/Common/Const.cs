namespace SMS.Common
{
    public static class Const
    {
        //User
        public const int UsernameMinLength = 5;
        public const int UsernameMaxLength = 20;
        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 20;
        public const int HashedPasswordMaxLength = 64;

        //Product
        public const int ProductNameMinLength = 4;
        public const int ProductNameMaxLength = 20;
        public const decimal PriceMinValue = 0.05M;
        public const decimal PriceMaxValue = 1000;
    }
}



