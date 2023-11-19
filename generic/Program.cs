using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        CustomCollection<Person> persons = new CustomCollection<Person>();
        string continueInput;

        do
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add Employee");
            Console.WriteLine("2) Find Employee by ID");
            Console.WriteLine("3) Display all Employees");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid input. Please enter a valid option (1-3).");
            }

            switch (choice)
            {
                case 1:
                    AddEmployee(persons);
                    break;
                case 2:
                    FindEmployeeById(persons);
                    break;
                case 3:
                    DisplayAllEmployees(persons);
                    break;
            }

            Console.Write("Do you want to continue? (yes/no): ");
            continueInput = Console.ReadLine().ToLower();
        } while (continueInput == "yes");
    }

    static void AddEmployee(CustomCollection<Person> persons)
    {
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age < 0)
        {
            Console.WriteLine("Invalid age. Please enter a valid non-negative integer.");
        }

        Employee newEmployee = new Employee(firstName, lastName, age);
        persons.Add(newEmployee);

        Console.WriteLine($"Employee added with ID: {newEmployee.Id}");
    }

    static void FindEmployeeById(CustomCollection<Person> persons)
    {
        Console.Write("Enter Employee ID: ");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 1)
        {
            Console.WriteLine("Invalid ID. Please enter a valid positive integer.");
        }

        Person foundPerson = persons.FindById(id);

        if (foundPerson != null)
        {
            Console.WriteLine($"Employee found: {foundPerson}");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }

    static void DisplayAllEmployees(CustomCollection<Person> persons)
    {
        Console.WriteLine("All Employees:");
        foreach (Person person in persons)
        {
            Console.WriteLine(person);
        }
    }
}

class CustomCollection<T> where T : Person
{
    private List<T> collection = new List<T>();
    private int currentId = 1;

    public void Add(T item)
    {
        item.Id = currentId++;
        collection.Add(item);
    }

    public T FindById(int id)
    {
        return collection.Find(item => item.Id == id);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return collection.GetEnumerator();
    }
}

class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {FirstName} {LastName}, Age: {Age}";
    }
}

class Employee : Person
{
    public decimal Salary { get; set; }

    public Employee(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public override string ToString()
    {
        return base.ToString() + $", Salary: {Salary:C}";
    }
}