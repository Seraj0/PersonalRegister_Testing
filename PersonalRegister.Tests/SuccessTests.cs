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
            // I Arrange s� tilldelar vi v�rde som vi vill kunna anv�nda i testet.
            var username = "admin";
            var password = "PASS";
            var loginService = new LoginService(); //l�gger in klassen i en variable f�r att kuna anropa metoden f�r testet.

            // Act tilldelade v�rdet som funktionen ska testa
            var result = loginService.AdminLogin(username, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveByID_Success()
        {
            // Arrange vi skapar mock test av Interface som inneh�ller remove metoden
            var employeeMock = new Mock<IEmployeeManagement>();
            var manageEmp = new MangeEmployeeList(employeeMock.Object);

            //Angivna ID:et som ska tas bort
            int id = 2;

            // konfigurerar bettendet som vi vill testa genom att anropa Get metoden med angivna ID:et som argument
            employeeMock.Setup(repo => repo.GetEmployeeById(id)).Returns(new Employee(id, "Alexander", 20, 5000.00));

            // Act = genomf�randet, anrop f�r att ta brt ID;et
            manageEmp.RemoveByID(id);

            // Assert kontrellerar att metoden anropas en g�ng med angivna ID
            employeeMock.Verify(repo => repo.DeleteEmployee(id), Times.Once());
            Console.WriteLine("Removed Successfully", id);
        }

        [Fact]
        public void AddNewEmp_Success()
        {
            // Arrange
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            var manageEmp = new MangeEmployeeList(IEmployeeMock.Object);
            //nya v�rdet som ska l�ggas till 
            int id = 25;
            string name = "Owen";
            int age = 20;
            double salary = 5000.00;

            // Act genomf�randet med mockade objektet och nya v�re
            manageEmp.AddNewEmp(id, name, age, salary);

            // Assert Verify kollar att nya v�rderna existerar en g�ng
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
            int userId = 3; // Anger ID:et f�r den anst�llde vars l�n vi vill h�mta.
            double expectedSalary = 5000; // Anger f�rv�ntad l�nev�rdet f�r den angivna ID:et.

            // Skapa ett mock-objekt f�r gr�nssnittet IEmployeeManagement.
            var IEmployeeMock = new Mock<IEmployeeManagement>();
            // setup f�r att konfigurera beteendet som vi vill testa med f�rv�ntade v�rdet
            IEmployeeMock.Setup(m => m.GetEmployeeById(userId)).Returns(new Employee(userId, "David", 30, expectedSalary));

            // l�gger in den mockade objektet som ska testas i en variabel
            var employeeInterface = IEmployeeMock.Object;
            // H�mta den anst�llde genom att anropa GetEmployeeById med det angivna ID:et.
            var userEmployee = employeeInterface.GetEmployeeById(userId);
            // H�mta den faktiska l�nen f�r den anst�llde.
            double actualSalary = userEmployee.GetSalary();


            // J�mf�r den f�rv�ntade l�nen med den faktiska l�nen med hj�lp av Assert.Equal.
            Assert.Equal(expectedSalary, actualSalary);

        }
    }
}