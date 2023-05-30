namespace PersonalRegister
{
    public class LoginService : ILoginService
    {
        // metoden för att kontrollera att Admin matar in rätta inloggnings uppgifter vid inloggningen till hanterings listan.
        // denna klass används av ILoginService som vi använder för att kunna testa Adminlogin funktionen.
        public bool AdminLogin(string username, string password)
        {
            // Check if the provided username and password match the admin credentials
            string adminUsername = "admin";
            string adminPassword = "PASS";

            if (username == adminUsername && password == adminPassword)
            {
                // Login successful
                return true;
            }
            else
            {
                // Login failed
                return false;
            }
        }
    }
}
