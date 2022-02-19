namespace SMS.Models.Errors
{
    public class ViewError
    {
        public string ErrorMessage { get; init; }

        public ViewError(string message)
        {
            ErrorMessage = message;
        }
    }
}
