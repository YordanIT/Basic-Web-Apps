namespace SharedTrip.Common
{
    public static class Const
    {
        //User
        public const int UsernameMaxLength = 20;
        public const int UsernameMinLength = 5;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 20;
        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        //Trip
        public const int SeatsMinValue = 2;
        public const int SeatsMaxValue = 6;
        public const int DescriptionMaxLength = 80;
        public const int TownNameMaxLength = 200;
        public const int ImagePathMaxLength = 200;
    }
}
