global using Xunit;
using Moq;
using PersonalRegister.Controller;
using PersonalRegister.InterFace;


namespace PersonalRegister.Tests
{
    public class SuccessTests
    {
        [Fact]
        public void AdminLogin_Success()
        {
            // I Arrange så tilldelar vi värde som vi vill kunna använda i testet.
            var username = "admin";
            var password = "PASS";
            var loginService = new LoginService(); //lägger in klassen i en variable för att kuna anropa metoden för testet.

            // Act tilldelade värdet som funktionen ska testa
            var result = loginService.AdminLogin(username, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveByID_Success()
        {
            // Arrange vi skapar mock test av Interface som innehåller remove metoden
            var employeeMock = new Mock<IEmployeeManagement>();
            var manageEmp = new MangeEmployeeList(employeeMock.Object);

            //Angivna ID:et som ska tas bort
            int id = 2;

            // konfigurerar bettendet som vi vill testa genom att anropa Get metoden med angivna ID:et som argument
            employeeMock.Setup(repo => repo.GetEmployeeById(id)).Returns(new Employee(id, "Alexander", 20, 5000.00));

            // Act = genomförandet, anrop för att ta brt ID;et
            manageEmp.RemoveByID(id);

            // Assert kontrellerar att metoden anropas en gång med angivna ID
            employeeMock.Verify(repo => repo.DeleteEmployee(id), Times.Once());
            Console.WriteLine("Removed Successfully", id);
        }

        [Fact]
        public void AddNewEmp_Success()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            //nya värdet som ska läggas till 
            int id = 25;
            string name = "Owen";
            int age = 20;
            double salary = 5000.00;

            // Act genomförandet med mockade objektet och nya väre
            manageEmp.AddNewEmp(id, name, age, salary);

            // Assert Verify kollar att nya värderna existerar en gång
            IEmployeeMock.Verify(e => e.AddEmployee(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public void AddedEmployee__ToList_Success()
        {
            // Arrange
            var empCrud = new EmployeeManagement();
            var employee = new Employee(1, "David", 25, 5000.00);

            // Act
            empCrud.AddEmployee(employee);

            // Assert
            Assert.Contains(employee, empCrud.GetAllEmployees());
        }

        [Fact]
        public void UpdateByID_Success()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);

            int id = 1;
            string name = "John";
            int age = 30;
            double salary = 5000.00;

            var employee = new Employee(id, name, age, salary);
            IEmployeeMock.Setup(e => e.GetEmployeeById(id)).Returns(employee);

            string updatedName = "John nya";
            int updatedAge = 35;
            double updatedSalary = 5500.00;

            // Act
            manageEmp.UpdateByID(id, updatedName, updatedAge, updatedSalary);

            // Assert
            IEmployeeMock.Verify(e => e.UpdateEmployee(employee), Times.Once);
            Assert.Equal(updatedName, employee.GetName());
            Assert.Equal(updatedAge, employee.GetAge());
            Assert.Equal(updatedSalary, employee.GetSalary());
        }


        [Fact]
        public void SearchByID_GetsEmployee_Success()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var employee = new Employee(1, "John", 30, 5000);
            IEmployeeMock.Setup(repo => repo.GetEmployeeById(1))
                .Returns(employee);

            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.SearchByID(1);

            // Assert
            var expectedOutput = "ID: 1 Name: John Age: 30 Salary: 5000,00$" + Environment.NewLine;
            Assert.Equal(expectedOutput, consoleOutput.ToString());
        }


        [Fact]
        public void ReadAll_Sucess()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var employees = new List<Employee>
            {
                new Employee(24, "Harper", 48, 19558),
                new Employee(25, "Daniel", 56, 16561)

            };
            IEmployeeMock.Setup(repo => repo.GetAllEmployees())
                    .Returns(employees);

            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            manageEmp.ReadAll();

            // Assert
            var expectedOutput = "ID: 24 Name: Harper Age: 48 Salary: 19558,00$" + Environment.NewLine +
                                "ID: 25 Name: Daniel Age: 56 Salary: 16561,00$" + Environment.NewLine;
            Assert.Equal(expectedOutput, consoleOutput.ToString());
        }

        [Fact]
        public void EmployeeUserMode__ReturnSalaryById__Success()
        {
            // Arrange
            int userId = 3; // Anger ID:et för den anställde vars lön vi vill hämta.
            double expectedSalary = 5000; // Anger förväntad lönevärdet för den angivna ID:et.

            // Skapa ett mock-objekt för gränssnittet IEmployeeManagement.
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            // setup för att konfigurera beteendet som vi vill testa med förväntade värdet
            IEmployeeMock.Setup(m => m.GetEmployeeById(userId)).Returns(new Employee(userId, "David", 30, expectedSalary));

            // lägger in den mockade objektet som ska testas i en variabel
            var employeeInterface = IEmployeeMock.Object;
            // Hämta den anställde genom att anropa GetEmployeeById med det angivna ID:et.
            var userEmployee = employeeInterface.GetEmployeeById(userId);
            // Hämta den faktiska lönen för den anställde.
            double actualSalary = userEmployee.GetSalary();


            // Jämför den förväntade lönen med den faktiska lönen med hjälp av Assert.Equal.
            Assert.Equal(expectedSalary, actualSalary);

        }
    }
}