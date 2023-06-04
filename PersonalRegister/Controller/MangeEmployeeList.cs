using PersonalRegister.InterFace;
namespace PersonalRegister.Controller
{
    //Denna klass hanterar de olika Crud operationer för listan,
    //Med MangeEmployeeList klassen så kan man hantera det som är även lagrat i ram minnet, ingen koppling till en DB behövs.
    public class MangeEmployeeList : IManageList
    {
        private IEmployeeManagement _Iemployee;
        private List<Employee> _employeesList;

        public MangeEmployeeList(IEmployeeManagement EmployeeManagement)
        {
            _employeesList = new List<Employee>();
            _Iemployee = EmployeeManagement;
        }

        public void AddNewEmp(int id, string name, int age, double salary)
        {
            if (_Iemployee.GetEmployeeById(id) != null)
            {
                throw new InvalidOperationException("An employee with the same ID already exists.");
            }

            Employee employee = new Employee(id, name, age, salary);
            _Iemployee.AddEmployee(employee);
            Console.WriteLine("New Employee Added ->");
            Console.WriteLine("ID: " + id.ToString() + " Name: " + name + " Age: " + age.ToString() + " Salary: " + salary.ToString("0.00") + '$');
        }


        public void SearchByName(string name)
        {
            List<Employee> matchingEmployees = _Iemployee.GetAllEmployees().FindAll(e => e.GetName().Contains(name));
            if (matchingEmployees.Count != 0)
            {
                foreach (Employee employee in matchingEmployees)
                {
                    Console.WriteLine("ID: " + employee.GetId().ToString() + " Name: " + employee.GetName() + " Age: " + employee.GetAge().ToString() + " Salary: " + employee.GetSalary().ToString("0.00") + '$');
                }
            }
            else
            {
                Console.WriteLine("No Match Found");
            }
        }

        public void SearchByID(int id)
        {
            Employee employee = _Iemployee.GetEmployeeById(id);
            if (employee != null)
            {
                Console.WriteLine("ID: " + employee.GetId().ToString() + " Name: " + employee.GetName() + " Age: "
                    + employee.GetAge().ToString() + " Salary: " + employee.GetSalary().ToString("0.00") + '$');
            }
            else
            {
                Console.WriteLine("No Employee with the specified ID exists");
            }
        }

        public void RemoveByID(int id)
        {
            var employee = _Iemployee.GetEmployeeById(id);
            if (employee != null)
            {
                _Iemployee.DeleteEmployee(id);
                Console.WriteLine("Removed Successfully");
            }
            else
            {
                Console.WriteLine("No Employee with the specified ID exists.");
            }
        }

        public void UpdateByID(int id, string name = null, int age = -1, double salary = -1)
        {
            Employee employee = _Iemployee.GetEmployeeById(id);
            if (employee != null)
            {
                employee.SetName(name ?? employee.GetName());
                employee.SetAge(age == -1 ? employee.GetAge() : age);
                employee.SetSalary(salary == -1 ? employee.GetSalary() : salary);
                _Iemployee.UpdateEmployee(employee);
                Console.WriteLine("Updated Successfully");
            }
            else
            {
                Console.WriteLine("No Match Found");
            }
        }

        public void ReadAll()
        {
            List<Employee> employees = _Iemployee.GetAllEmployees();
            if (employees.Count != 0)
            {
                foreach (Employee employee in employees)
                {
                    Console.WriteLine("ID: " + employee.GetId().ToString() + " Name: " + employee.GetName() + " Age: " + employee.GetAge().ToString() + " Salary: " + employee.GetSalary().ToString("0.00") + '$');
                }
            }
            else
            {
                Console.WriteLine("No Employees");
            }
        }
    }
}
