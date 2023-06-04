namespace PersonalRegister.InterFace
{   // Denna klass hanterar de Crud operationer för listan, denna klass kan hantera det som är lagrat i ram minnet med, ingen koppling till en DB behövs.
    //IManage Emp interfacet innehåller olika Crud operations metoder.
    // med hjälp av InterFacet kan administratören använda sig av olika metoder för att hantera en anställd,
    // antingen via ID eller namn.
    public interface IManageList
    {
        void AddNewEmp(int id, string name, int age, double salary);
        void SearchByName(string name);
        void SearchByID(int id);
        void RemoveByID(int id); // Lägg till RemoveByID-metoden
        void UpdateByID(int id, string name, int age, double salary);
        void ReadAll();
    }

}
