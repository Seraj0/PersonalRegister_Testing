namespace PersonalRegister.InterFace
{
    //interfacet som innehåller inloggnings funktionen som testas senare.
    public interface ILoginService
    {
        bool AdminLogin(string username, string password);
    }
}
