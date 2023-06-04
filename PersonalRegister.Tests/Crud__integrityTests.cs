using PersonalRegister.Controller;

namespace PersonalRegister.Tests
{
    public class Crud__integrityTests
    {
        [Fact]
        public void AddEmployee_Should_Add_Employee()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            Employee employee = new Employee(100, "max", 20, 2000);

            // Act
            empCrud.AddEmployee(employee);
            Employee NewEmployee = empCrud.GetEmployeeById(100);

            // Assert
            Assert.NotNull(NewEmployee);
            Assert.Equal(100, NewEmployee.GetId());
            Assert.Equal("max", NewEmployee.GetName());
            Assert.Equal(20, NewEmployee.GetAge());
            Assert.Equal(2000, NewEmployee.GetSalary());
        }

        [Fact]
        public void GetEmployeeById_Should_Return_Correct_Employee()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            Employee employee = empCrud.GetAllEmployees().First(); // Get the first employee from the list
            int idToSearch = employee.GetId();

            // Act
            Employee ExpectedEmployee = empCrud.GetEmployeeById(idToSearch);

            // Assert
            Assert.NotNull(ExpectedEmployee);
            Assert.Equal(employee, ExpectedEmployee);
        }

        [Fact]
        public void GetAllEmployees_Should_Return_All_Employees()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            List<Employee> expectedEmployees = empCrud.GetAllEmployees();

            // Act
            List<Employee> actualEmployees = empCrud.GetAllEmployees();

            // Assert
            Assert.Equal(expectedEmployees.Count, actualEmployees.Count);
            // Additional assertions to compare each employee individually if needed
            for (int i = 0; i < expectedEmployees.Count; i++)
            {
                Assert.Equal(expectedEmployees[i], actualEmployees[i]);
            }
        }

        [Fact]
        public void UpdateEmployee_Should_Update_Employee()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            Employee employeeToUpdate = empCrud.GetAllEmployees().First(); // 
            Employee updatedEmployee = new Employee(employeeToUpdate.GetId(), "Leo", 40, 8000);

            // Act
            empCrud.UpdateEmployee(updatedEmployee);
            Employee retrievedEmployee = empCrud.GetEmployeeById(updatedEmployee.GetId());

            // Assert
            Assert.NotNull(retrievedEmployee);
            Assert.Equal(updatedEmployee.GetName(), retrievedEmployee.GetName());
            Assert.Equal(updatedEmployee.GetAge(), retrievedEmployee.GetAge());
            Assert.Equal(updatedEmployee.GetSalary(), retrievedEmployee.GetSalary());
        }

        [Fact]
        public void DeleteEmployee_Should_Remove_Employee()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            int targetId = 63; // ID of the fourth employee in the TestList

            // Act
            empCrud.DeleteEmployee(targetId);
            Employee deletedEmployee = empCrud.GetEmployeeById(targetId);

            // Assert
            Assert.Null(deletedEmployee);
        }

        [Fact]
        public void AddEmployee_Should_Increase_Employee_Count()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            int initialCount = empCrud.GetAllEmployees().Count;
            Employee employee = new Employee(100, "John", 30, 5000);

            // Act
            empCrud.AddEmployee(employee);
            int updatedCount = empCrud.GetAllEmployees().Count;

            // Assert
            Assert.Equal(initialCount + 1, updatedCount);
        }

        [Fact]
        public void DeleteEmployee_Should_Decrease_Employee_Count()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            int initialCount = empCrud.GetAllEmployees().Count;
            int idToDelete = 1; // Assuming the first employee has ID 1

            // Act
            empCrud.DeleteEmployee(idToDelete);
            int updatedCount = empCrud.GetAllEmployees().Count;

            // Assert
            Assert.Equal(initialCount - 1, updatedCount);
        }

        [Fact]
        public void GetEmployeeById_Should_Return_Null_For_Nonexistent_Id()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            int nonexistentId = 999;

            // Act
            Employee retrievedEmployee = empCrud.GetEmployeeById(nonexistentId);

            // Assert
            Assert.Null(retrievedEmployee);
        }

        [Fact]
        public void UpdateEmployee_Should_Not_Modify_Other_Employees()
        {
            // Arrange
            EmployeeManagement empCrud = new EmployeeManagement();
            Employee employeeToUpdate = empCrud.GetAllEmployees().First(); // Get the first employee from the list
            Employee updatedEmployee = new Employee(employeeToUpdate.GetId(), "sholololo", 40, 8000);

            // Act
            empCrud.UpdateEmployee(updatedEmployee);
            List<Employee> allEmployees = empCrud.GetAllEmployees();

            // Assert
            foreach (Employee employee in allEmployees)
            {
                if (employee.GetId() != employeeToUpdate.GetId())
                {
                    Assert.NotEqual(updatedEmployee.GetName(), employee.GetName());
                    Assert.NotEqual(updatedEmployee.GetSalary(), employee.GetSalary());
                }
                else
                {
                    Assert.Equal(updatedEmployee.GetAge(), employee.GetAge());
                }
            }
        }
    }
}