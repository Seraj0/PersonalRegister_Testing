using PersonalRegister.Controller;
using PersonalRegister.InterFace;

namespace PersonalRegister
{
    // huvud klassen som hanterar crud operationer, Denna klass kan användas för att kopplas till en databas
    // implmenterar sedan metoderna i IEmployeeManagement Interfacet.
    public class EmployeeManagement : IEmployeeManagement
    {
        private List<Employee> _employeesList;
        private List<Employee> _testlist;

        public EmployeeManagement()
        {
            _employeesList = new List<Employee>();
            _testlist = new List<Employee>();
            GenerateRandomEmployees();
            TestList();
        }

        public void AddEmployee(Employee employee)
        {
            _employeesList.Add(employee);
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = _employeesList.Find(e => e.GetId() == id);
            if (employee == null)
            {
                Console.WriteLine("No Match Found");
            }
            return employee;
        }

        public List<Employee> GetAllEmployees()
        {
            if (_employeesList.Count == 0)
            {
                Console.WriteLine("No employees found.");
            }

            return _employeesList;
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee existingEmployee = GetEmployeeById(employee.GetId());
            if (existingEmployee != null)
            {
                existingEmployee.SetName(employee.GetName());
                existingEmployee.SetAge(employee.GetAge());
                existingEmployee.SetSalary(employee.GetSalary());
            }
            else
            {
                Console.WriteLine("No Match Found");
            }
        }

        public void DeleteEmployee(int id)
        {
            Employee employee = GetEmployeeById(id);
            if (employee != null)
            {
                _employeesList.Remove(employee);
            }
            else
            {
                Console.WriteLine("No Match Found");
            }
        }

        private void GenerateRandomEmployees()
        {
            Random random = new Random();
            List<string> names = new List<string>()
            {
                "John", "Alice", "David", "Emma", "Michael", "Olivia", "Daniel", "Sophia", "Matthew", "Emily",
                "Andrew", "Mia", "James", "Ava", "William", "Isabella", "Joseph", "Charlotte", "Benjamin", "Amelia",
                "Samuel", "Abigail", "Elijah", "Harper", "Daniel", "Elizabeth", "Alexander", "Evelyn", "Henry", "Ella",
                "David", "Grace", "Christopher", "Victoria", "Jackson", "Chloe", "Sebastian", "Penelope", "Jack", "Lily",
                "Gabriel", "Hannah", "Anthony", "Sofia", "Logan", "Zoe", "Carter", "Avery", "Owen", "Scarlett"
            };

            int nameIndex = 0;

            for (int i = 1; i <= 50; i++)
            {
                string name = names[nameIndex];
                int age = random.Next(20, 60);
                double salary = random.Next(1000, 20000);

                _employeesList.Add(new Employee(i, name, age, salary));
                _testlist.Add(new Employee(i, name, age, salary)); // Add employee to the testlist

                // Increase name index to choose the next name in the list
                nameIndex = (nameIndex + 1) % names.Count;
            }
        }

        private void TestList()
        {
            List<string> testNames = new List<string>()
    {
        "Seraj", "Dyari", "Cey", "Elon", "Nikola",
        "Khaled", "Nathan", "Sophie", "Christopher", "Eleanor"
    };

            int[] testAges = { 26, 31, 36, 41, 46, 51, 56, 61, 66, 71 };

            double[] testSalaries = { 5100, 5600, 6100, 6600, 7100, 7600, 8100, 8600, 9100, 9600 };

            for (int i = 0; i < testNames.Count; i++)
            {
                string name = testNames[i];
                int age = testAges[i];
                double salary = testSalaries[i];
                int id = i + 60; // Start the ID from 60

                _testlist.Add(new Employee(id, name, age, salary)); // Add employee to the testlist
            }
        }

    }
}


