namespace PersonalRegister.Controller
{
    // Employee klassen, har innhåll av 4 olika egenskaper
    // sedan har vi lagt de i get och set för att kunna använda oss av de för olika hanteringar.
    public class Employee
    {
        private int _id;
        private string Name;
        private int _age;
        private double _salary;

        public Employee(int id, string name, int age, double salary)
        {
            _id = id;
            Name = name;
            _age = age;
            _salary = salary;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            //return _name;

            return Name; // Assuming the name property is called "Name"
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public int GetAge()
        {
            return _age;
        }

        public void SetAge(int age)
        {
            _age = age;
        }

        public double GetSalary()
        {
            return _salary;
        }

        public void SetSalary(double salary)
        {
            _salary = salary;
        }
    }
}
