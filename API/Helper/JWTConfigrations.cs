namespace API.Helper
{
    public class JWTConfigrations
    {
        public static string TokenSecret { get; private set; } = string.Empty;
        public static void SetTokenSecret(string key)
        {
            TokenSecret = key;
        }

    }
}
