namespace PersonalRegister;

//interfacet som implementeras av klassen Employee Management, denna Interface används sedan i Xunit test 
// för att skapa mock objekt av, och sedan få tillgång till de existerande metoderna i testet
public interface IEmployeeManagement
{
    void AddEmployee(Employee employee);
    Employee GetEmployeeById(int id);
    List<Employee> GetAllEmployees();
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int id);
}

