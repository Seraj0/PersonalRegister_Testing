using Microsoft.Extensions.DependencyInjection;
using PersonalRegister;
using PersonalRegister.Controller;
using PersonalRegister.InterFace;

class Program
{
    static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }
        // Skapa en ny instans av EmployeeManagement
        var empCrud = new EmployeeManagement();

        // Registrera beroendena i DI-container
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IEmployeeManagement>(empCrud)
            .AddSingleton<IManageList, MangeEmployeeList>()
            .AddSingleton<ILoginService, LoginService>()
            .BuildServiceProvider();

        // Hämta instansen av IManageList från DI-container för admin
        var manageEmpAdmin = serviceProvider.GetService<IManageList>();

        // Hämta instansen av IEmployeeManagement från DI-container för den anställda
        var employeeInterface = serviceProvider.GetService<IEmployeeManagement>();

        // Hämta instansen av LoginService från DI-container
        var loginService = serviceProvider.GetService<ILoginService>();

        // Loopa för att hantera anställda baserat på användarinmatning
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("Välj användartyp:");
            Console.WriteLine("1. Användare");
            Console.WriteLine("2. Admin");
            Console.WriteLine("3. Avsluta");
            Console.WriteLine();

            Console.Write("Ange ditt val: ");
            string userTypeChoice = Console.ReadLine();

            switch (userTypeChoice)
            {
                case "1":
                    // Användarläge
                    Console.Write("Ange ditt ID: ");
                    int userId = int.Parse(Console.ReadLine());
                    Employee userEmployee = employeeInterface.GetEmployeeById(userId);
                    if (userEmployee != null)
                    {
                        Console.WriteLine("Din lön är: " + userEmployee.GetSalary().ToString("0.00") + '$');
                    }
                    else
                    {
                        Console.WriteLine("Ingen anställd hittades med det angivna ID:et.");
                    }
                    break;

                case "2":
                    // Adminläge
                    Console.Write("Ange användarnamn: ");
                    string username = Console.ReadLine();
                    Console.Write("Ange lösenord: ");
                    string password = Console.ReadLine();

                    if (loginService.AdminLogin(username, password))
                    {
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Välj en operation:");
                            Console.WriteLine("1. Lägg till ny anställd");
                            Console.WriteLine("2. Sök efter anställd baserat på namn");
                            Console.WriteLine("3. Sök efter anställd baserat på ID");
                            Console.WriteLine("4. Ta bort anställd baserat på ID");
                            Console.WriteLine("5. Uppdatera anställd baserat på ID");
                            Console.WriteLine("6. Visa alla anställda");
                            Console.WriteLine("7. Återgå till huvudmenyn");
                            Console.WriteLine();

                            Console.Write("Ange ditt val: ");
                            string adminChoice = Console.ReadLine();

                            switch (adminChoice)
                            {
                                case "1":
                                    Console.Write("Ange ID: ");
                                    int id = int.Parse(Console.ReadLine());
                                    Console.Write("Ange namn: ");
                                    string name = Console.ReadLine();
                                    Console.Write("Ange ålder: ");
                                    int age = int.Parse(Console.ReadLine());
                                    Console.Write("Ange lön: ");
                                    double salary = double.Parse(Console.ReadLine());

                                    manageEmpAdmin.AddNewEmp(id, name, age, salary);
                                    break;

                                case "2":
                                    Console.Write("Ange namn att söka efter: ");
                                    string searchName = Console.ReadLine();
                                    manageEmpAdmin.SearchByName(searchName);
                                    break;

                                case "3":
                                    Console.Write("Ange ID att söka efter: ");
                                    int searchId = int.Parse(Console.ReadLine());
                                    manageEmpAdmin.SearchByID(searchId);
                                    break;

                                case "4":
                                    Console.Write("Ange ID att ta bort: ");
                                    int removeId = int.Parse(Console.ReadLine());
                                    manageEmpAdmin.RemoveByID(removeId);
                                    break;

                                case "5":
                                    Console.Write("Ange ID att uppdatera: ");
                                    int updateId = int.Parse(Console.ReadLine());
                                    Console.Write("Ange nytt namn (tryck enter för att behålla befintligt namn): ");
                                    string updateName = Console.ReadLine();
                                    Console.Write("Ange ny ålder (tryck enter för att behålla befintlig ålder): ");
                                    string updateAgeStr = Console.ReadLine();
                                    int updateAge = string.IsNullOrEmpty(updateAgeStr) ? -1 : int.Parse(updateAgeStr);
                                    Console.Write("Ange ny lön (tryck enter för att behålla befintlig lön): ");
                                    string updateSalaryStr = Console.ReadLine();
                                    double updateSalary = string.IsNullOrEmpty(updateSalaryStr) ? -1 : double.Parse(updateSalaryStr);

                                    manageEmpAdmin.UpdateByID(updateId, updateName, updateAge, updateSalary);
                                    break;

                                case "6":
                                    manageEmpAdmin.ReadAll();
                                    break;

                                case "7":
                                    exit = true;
                                    break;

                                default:
                                    Console.WriteLine("Ogiltigt val. Försök igen.");
                                    break;
                            }

                            if (exit)
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt användarnamn eller lösenord. Försök igen.");
                    }
                    break;

                case "3":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }

        }

    }

}
