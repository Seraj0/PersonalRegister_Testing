using Moq;
using PersonalRegister.Controller;
using PersonalRegister.InterFace;

namespace PersonalRegister.Tests
{
    public class FailTests
    {

        [Fact]
        public void AdminLogin_PasswordFail()
        {
            // Arrange
            var usernameTest = "admin";
            var passwordTest = "shiiiet";
            var loginService = new LoginService();

            // Act
            var result = loginService.AdminLogin(usernameTest, passwordTest);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void UpdateByID_FailByNonExistingId()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var MangeEmployeeList = new MangeEmployeeList(IEmployeeMock.Object);

            int id = 1;
            string updatedName = "John Doe";
            int updatedAge = 35;
            double updatedSalary = 5500.00;

            IEmployeeMock.Setup(e => e.GetEmployeeById(id)).Returns((Employee)null);

            // Act
            MangeEmployeeList.UpdateByID(id, updatedName, updatedAge, updatedSalary);

            // Assert
            IEmployeeMock.Verify(e => e.UpdateEmployee(It.IsAny<Employee>()), Times.Never);
        }


        [Fact]
        public void ReadAll__PrintsNoEmployees__whenListAreNull()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            IEmployeeMock.Setup(repo => repo.GetAllEmployees())
                .Returns(new List<Employee>());

            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.ReadAll();

            // Assert
            Assert.Equal("No Employees", consoleOutput.ToString().Trim());
        }


        [Fact]
        public void SearchByName_MultipleMatchingEmployees_PrintsEmployees()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var matchingEmployees = new List<Employee>
            {
                new Employee(1, "John", 30, 5000),
                new Employee(2, "John", 35, 6000),
                new Employee(3, "John", 28, 4500)
            };
            IEmployeeMock.Setup(repo => repo.GetAllEmployees())
                .Returns(matchingEmployees);

            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.SearchByName("John");

            // Assert
            var expectedOutput = "ID: 1 Name: John Age: 30 Salary: 5000,00$" + Environment.NewLine +
                                 "ID: 2 Name: John Age: 35 Salary: 6000,00$" + Environment.NewLine +
                                 "ID: 3 Name: John Age: 28 Salary: 4500,00$";
            var actualOutput = consoleOutput.ToString().Trim();
            Assert.Equal(expectedOutput, actualOutput);
        }



        [Fact]
        public void SearchByID_PrintsNoMatchFound__WhenNonExistingId()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            IEmployeeMock.Setup(repo => repo.GetEmployeeById(1))
                    .Returns((Employee)null);

            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.SearchByID(100);

            // Assert
            Assert.Equal("No Employee with the specified ID exists", consoleOutput.ToString().Trim());
        }

        [Fact]
        public void UpdateByID_NonExistingId_NoUpdateAndPrintsNoMatchFound()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var nonExistingEmployeeId = 100; // An id that does not exist in the employee list
            IEmployeeMock.Setup(repo => repo.GetEmployeeById(nonExistingEmployeeId)).Returns((Employee)null);

            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.UpdateByID(nonExistingEmployeeId, "Leo", 40, 7000);

            // Assert
            Assert.Equal("No Match Found", consoleOutput.ToString().Trim());
        }


        [Fact]
        public void AddNewEmp_ThrowsException__WhenDuplicateId()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);

            int id = 2;
            string name = "Alice";
            int age = 30;
            double salary = 5000.00;

            // Configure the employee repository mock to return an existing employee with the same ID
            IEmployeeMock.Setup(repo => repo.GetEmployeeById(id)).Returns(new Employee(id, name, age, salary));

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => manageEmp.AddNewEmp(id, name, age, salary));
        }


        [Fact]
        public void EmployeeMode_GetSalary_NonExistingId_ReturnsErrorMessage()
        {
            // Arrange
            int userId = 999; // Ange ett icke-existerande användar-ID för att testa felmeddelandet

            // Skapa en mock eller ett stubbobjekt för IEmployeeManagement-gränssnittet
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            IEmployeeMock.Setup(m => m.GetEmployeeById(userId)).Returns((Employee)null);

            // Hämta instansen av IEmployeeManagement från mock-objektet
            var employeeInterface = IEmployeeMock.Object;

            // Act
            var userEmployee = employeeInterface.GetEmployeeById(userId);

            // Assert
            Assert.Null(userEmployee);
            Console.WriteLine("Ingen anställd hittades med det angivna ID:et.");

        }

        [Fact]
        public void SearchByName_NoMatchingEmployees_PrintsNoMatchFound()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeManagement>();
            var matchingEmployees = new List<Employee>
        {
            new Employee(1, "David", 20, 20000),
            new Employee(2, "Leo", 23, 11000)
        };
            employeeRepositoryMock.Setup(repo => repo.GetAllEmployees())
                .Returns(matchingEmployees);

            var manageEmp = new MangeEmployeeList(employeeRepositoryMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.SearchByName("Alice");

            // Assert
            Assert.Equal("No Match Found", consoleOutput.ToString().Trim());
        }

        [Fact]
        public void RemoveByID_NonExistingId_NoAction()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeManagement>();
            var manageEmp = new MangeEmployeeList(employeeRepositoryMock.Object);

            int id = 100; // Ett ID som inte finns i repositoryn

            // Act
            manageEmp.RemoveByID(id);

            // Assert
            employeeRepositoryMock.Verify(repo => repo.DeleteEmployee(id), Times.Never());
            Console.WriteLine("No action was performed since the ID does not exist.");
        }




        [Fact]
        public void SearchByName_MatchingEmployees_PrintsEmployees()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeManagement>();
            var matchingEmployees = new List<Employee>
            {
                new Employee(1, "John", 39, 14533),
                new Employee(2, "David", 35, 6000)
            };
            employeeRepositoryMock.Setup(repo => repo.GetAllEmployees())
                .Returns(matchingEmployees);

            var manageEmp = new MangeEmployeeList(employeeRepositoryMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.SearchByName("John");

            // Assert
            var expectedOutput = "ID: 1 Name: John Age: 39 Salary: 14533,00$";
            var actualOutput = consoleOutput.ToString().Trim(); // Trim bort extra mellanslag eller radbrytningar
            Assert.Equal(expectedOutput, actualOutput);
        }

    }
}