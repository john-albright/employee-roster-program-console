using System;
using static System.Console;

/*
 *
 * CIS 227 Final Project
 * 
 * Description: This project manages a employee roster list. The program 
 * is based on an Employee class with 8 instance variables:
 * - first name (string)
 * - last name (string)
 * - gender (char: M, F)
 * - birth date (DateTime object)
 * - hire date (DateTime object)
 * - job code (A, D, T, W)
 * - job (actor/actress, doctor, teacher, waiter/waitress)
 * - hourly rate
 * The class also contains two static methods: CalculateAge() and ToString().
 * The constructor for the class accepts all parameters except the job and 
 * automatically assigns the job value based on the job code (through the 
 * constructor and the setter). 
 * 
 * The driver adds 7 sample employees to the currentEmployees static global
 * array of Employee objects. Through the use of various methods, the user
 * can perform various operations on the employee roster list:
 * - A. Print the list
 * - B. Add an employee
 * - C. Remove an employee
 * - D. Search for employees by job code
 * 
 * The array is initialized with a size of 100 and the length of 
 * non-null values is managed with a global integer employeeCount variable.
 * If the user wants to add a user and the array has 100 employee objects,
 * the array is doubled in size. 
 * 
 * The program has the following user-created exceptions: 
 * - InvalidMonthException
 * - InvalidDayException
 * - UnreasonableYearException
 * - InvalidCurrencyException
 * These exceptions are thrown and handled along with FormatException
 * objects to ensure that the user enters valid information. The DateTime 
 * object is used throughout the application and any ArgumentOutOfRange
 * exception thrown by it is handled to make sure dates are valid. 
 * 
 * File Name: CISFinalAlbright.cs
 * Date: May 15, 2021
 *
*/ 

namespace CIS227FinalAlbright
{
    class Program
    {
        // Declare global variables for the methods of the program
        static Employee[] currentEmployees = new Employee[100];
        static int employeeCount = 0;
        static int eighteenYearsAgo = DateTime.Today.Year - 18;

        // Create the primary class for the program
        class Employee
        {
            private string firstName;
            private string lastName;
            private char gender;
            private DateTime birthDate;
            private DateTime hireDate;
            private char jobCode;
            private string job;
            private double hourlyRate;

            public string FirstName
            {
                get { return this.firstName; }
                set { this.firstName = value; }
            }

            public string LastName
            {
                get { return this.lastName; }
                set { this.lastName = value; }
            }

            public char Gender
            {
                get { return this.gender; }
                set 
                {
                    if (value == 'M' || value == 'F')
                        this.gender = value;
                    else
                        this.gender = 'X';
                }
            }

            public DateTime BirthDate
            {
                get { return this.birthDate; }
                set { this.birthDate = value; }
            }

            public DateTime HireDate
            {
                get { return this.hireDate; }
                set { this.hireDate = value; }
            }

            public char JobCode
            {
                get { return this.jobCode; }
                set 
                {
                    this.jobCode = value;



                    if (value == 'D' || value == 'T' || value == 'W' || value == 'A')
                    {
                        this.jobCode = value;

                        if (value == 'D')
                            this.job = "Doctor";
                        else if (value == 'T')
                            this.job = "Teacher";
                        else if (value == 'W')
                        {
                            if (Gender == 'M')
                                this.job = "Waiter";
                            else if (Gender == 'F')
                                this.job = "Waitress";
                            else
                                this.job = "Waiter/waitress";
                        }
                        else
                        {
                            if (Gender == 'M')
                                this.job = "Actor";
                            else if (Gender == 'F')
                                this.job = "Actress";
                            else
                                this.job = "Actor/actress";
                        }
                    }
                    else
                    {
                        this.jobCode = 'X';
                        this.job = "invalid job code";
                    }
                }
            }

            public string Job
            {
                get { return this.job; }
                set { this.job = value; }
            }

            public double HourlyRate
            {
                get { return this.hourlyRate; }
                set { this.hourlyRate = value; }
            }

            // Declare the constructor
            public Employee(string firstName, string lastName, char gender, DateTime birthDate, DateTime hireDate, char jobCode, double hourlyRate )
            {
                this.firstName = firstName;
                this.lastName = lastName;
                this.gender = gender;
                this.birthDate = birthDate;
                this.hireDate = hireDate;
                this.jobCode = jobCode;
                this.hourlyRate = hourlyRate;

                // Set the job variable to the correct job based on the job code
                // This must be done since the constructor doesn't accept 
                // a job as an argument (only a job code). 
                if (jobCode == 'D' || jobCode == 'T' || jobCode == 'W' || jobCode == 'A')
                {
                    if (jobCode == 'D')
                        this.job = "Doctor";
                    else if (jobCode == 'T')
                        this.job = "Teacher";
                    else if (jobCode == 'W')
                    {
                        if (Gender == 'M')
                            this.job = "Waiter";
                        else if (Gender == 'F')
                            this.job = "Waitress";
                        else
                            this.job = "Waiter/waitress";
                    }
                    else
                    {
                        if (Gender == 'M')
                            this.job = "Actor";
                        else if (Gender == 'F')
                            this.job = "Actress";
                        else
                            this.job = "Actor/actress";
                    }
                }
                else
                {
                    this.jobCode = 'X';
                    this.job = "invalid job code";
                }
            }

            // Static methods for the Employee class
            // 1. Method to calculate the age of the employee
            public static int CalculateAge(DateTime birthDate)
            {
                // Calculate the difference in days, months, and years
                int age = DateTime.Today.Year - birthDate.Year;
                int monthDiff = DateTime.Today.Month - birthDate.Month;
                int dayDiff = DateTime.Today.Day - birthDate.Day;

                // Check to see if the birth date has happened yet this year
                if (monthDiff < 0 || dayDiff < 0)
                    age--;

                // Return the age
                return age;
            }

            // 2. Method to format how Employee objects are printed
            public override string ToString()
            {
                return $"{FirstName} {LastName} is a " + CalculateAge(BirthDate) + $"-year old {Job.ToLower()} ({JobCode})" 
                    + $" who was hired on {HireDate.ToString("d")}.";
            }
        }

        // Create exceptions for the program
        class InvalidMonthException : Exception
        {
            private readonly static string msg = "An invalid number was entered for the month.";
            public InvalidMonthException() : base(msg) { }
            public InvalidMonthException(string message) : base(message) { }
            public InvalidMonthException(string message, Exception inner) : base(message, inner) { }
        }

        class InvalidDayException : Exception
        {
            private readonly static string msg = "An invalid number was entered for the day.";
            public InvalidDayException() : base(msg) { }
            public InvalidDayException(string message) : base(message) { }
            public InvalidDayException(string message, Exception inner) : base(message, inner) { }
        }

        class UnreasonableYearException : Exception
        {
            private readonly static string msg = "An unreasonable number was entered for the year.";
            public UnreasonableYearException() : base(msg) { }
            public UnreasonableYearException(string message) : base(message) { }
            public UnreasonableYearException(string message, Exception inner) : base(message, inner) { }
        }

        class InvalidCurrencyException : Exception
        {
            private readonly static string msg = "An invalid currency was entered.";
            public InvalidCurrencyException() : base(msg) { }
            public InvalidCurrencyException(string message) : base(message) { }
            public InvalidCurrencyException(string message, Exception inner) : base(message, inner) { }
        }

        // The driver method initializes 7 employee objects, generates the main menu,
        // and controls the triggering of the helper methods of the program. 
        static void Main(string[] args)
        {
            // Declare variables for the main method
            bool repeatMainMethod = true;
            bool validSelection = false;
            bool yesOrNoCheck;
            string userResponse = "";

            // Declare some employee objects and add them to the array 
            Employee davisIndia = new Employee("India", "Davis", 'F', new DateTime(1979, 3, 1), new DateTime(2001, 4, 5), 'D', 57.52);
            davisIndia.JobCode = 'T';
            currentEmployees[employeeCount] = davisIndia;
            ++employeeCount;

            Employee larsonKyle = new Employee("Kyle", "Larson", 'M', new DateTime(1995, 5, 13), new DateTime(2015, 12, 29), 'A', 25.18);
            currentEmployees[employeeCount] = larsonKyle;
            ++employeeCount;

            Employee suarezOwen = new Employee("Owen", "Suarez", 'M', new DateTime(1995, 5, 16), new DateTime(2018, 1, 19), 'D', 55.67);
            currentEmployees[employeeCount] = suarezOwen;
            ++employeeCount;

            Employee arellanoAlejandra = new Employee("Alejandra", "Arellano", 'F', new DateTime(2001, 11, 15), new DateTime(2020, 10, 5), 'W', 5.25);
            currentEmployees[employeeCount] = arellanoAlejandra;
            ++employeeCount;

            Employee tranSimeon = new Employee("Simeon", "Tran", 'M', new DateTime(1998, 1, 11), new DateTime(2019, 12, 2), 'W', 5.65);
            currentEmployees[employeeCount] = tranSimeon;
            ++employeeCount;

            Employee papatonisAlex = new Employee("Alexandra", "Papatonis", 'F', new DateTime(1995, 8, 10), new DateTime(2012, 6, 7), 'A', 35.89);
            currentEmployees[employeeCount] = papatonisAlex;
            ++employeeCount;

            Employee basharAaliyah = new Employee("Aaliyah", "Bashar", 'F', new DateTime(1965, 7, 25), new DateTime(1995, 4, 2), 'T', 17.95);
            currentEmployees[employeeCount] = basharAaliyah;
            ++employeeCount;

            // Print the header once
            PrintHeader();

            do
            {
                WriteLine("\nWelcome to We Do Everything & Co. Founded in 1977, our company");
                WriteLine("provides quite an array of services and is always seeking to");
                WriteLine("expand its list of available services.");

                do
                {
                    WriteLine("Please select one of the following options.");
                    WriteLine(">>> Enter A to see the full list of employees currently working.");
                    WriteLine(">>> Enter B to add a new employee to the roster.");
                    WriteLine(">>> Enter C to remove an employee from the roster");
                    WriteLine(">>> Enter D to search the employee roster based on job type.");
                    userResponse = ReadLine();

                    if (userResponse.ToUpper()[0] == 'A')
                    {
                        validSelection = true;
                        ShowListOfEmployees();
                    }
                    else if (userResponse.ToUpper()[0] == 'B')
                    {
                        validSelection = true;
                        
                        // Check if the array is full
                        // Create an array of double the size if its full
                        if (employeeCount <= currentEmployees.Length)
                        {
                            AddNewEmployee();
                        }
                        else
                        {
                            Employee[] newCurrentEmployeeList = new Employee[employeeCount * 2];

                            for (int k = 0; k < currentEmployees.Length; ++k)
                            {
                                newCurrentEmployeeList[k] = currentEmployees[k];
                            }

                            currentEmployees = newCurrentEmployeeList;
                            
                            // The employee can be added after doubling the size of the array
                            AddNewEmployee();
                        }
                    }
                    else if (userResponse.ToUpper()[0] == 'C')
                    {
                        validSelection = true;

                        if (employeeCount > 0)
                        {
                            RemoveEmployee();
                        }
                        else
                        {
                            WriteLine("No employees are on the roster! ");
                        }
                    }
                    else if (userResponse.ToUpper()[0] == 'D')
                    {
                        validSelection = true;

                        if (employeeCount > 0)
                        {
                            SearchEmployeesByJobCode();
                        }
                        else
                        {
                            WriteLine("No employees are on the roster! ");
                        }
                    }
                    else
                    {
                        validSelection = false;
                        WriteLine(">> Invalid character selection.");
                    }
                } while (!validSelection);

                // Set (and reset) the flag
                yesOrNoCheck = false;

                // Ask the user if he or she would like to go back to the main menu
                Write("\nWould you like to return to the main menu? (Y/N) ");
                userResponse = ReadLine();

                do
                {
                    if (userResponse.ToUpper()[0] == 'Y')
                    {
                        repeatMainMethod = true;
                        yesOrNoCheck = true;
                    }
                    else if (userResponse.ToUpper()[0] == 'N')
                    {
                        repeatMainMethod = false;
                        yesOrNoCheck = true;
                    }
                    else
                    {
                        WriteLine(">> Please enter Y or N: ");
                        yesOrNoCheck = false;
                    }
                } while (!yesOrNoCheck);

            } while (repeatMainMethod);

            Write("Exiting the program... ");
            ReadLine();
        }

        // Method to print the header of the console application
        public static void PrintHeader()
        {
            WriteLine("******************************************************************************");
            WriteLine("************************** CIS 227 Final Poject ******************************");
            WriteLine("********************** Programmer: John Albright *****************************");
            WriteLine("*************************** Date: May 14, 2021 *******************************");
            WriteLine("******************************************************************************");
        }

        // Method to show a table of all the employees currently working for the company 
        public static void ShowListOfEmployees()
        {
            // Print the header for the chart
            WriteLine("\n------------------------------------------------------------------------------");
            WriteLine("                        Current Employee Roster");
            WriteLine("------------------------- ----------------------------------------------------");
            WriteLine("{0,-11} | {1,-11} | {2,-10} | {3,-10} | {4,-3} | {5,-8} | {6,-8}",
                "First Name", "Last Name", "Birth Date", "Hire Date", "M/F", "Job", "Rate");
            WriteLine("------------------------------------------------------------------------------");

            // Print the values of the chart
            for (int i = 0; i < employeeCount; i++)
            {
                WriteLine("{0,-11} | {1,-11} | {2,-10} | {3,-10} | {4,-3} | {5,-8} | {6,-8}", currentEmployees[i].FirstName, 
                    currentEmployees[i].LastName, currentEmployees[i].BirthDate.ToString("d"),
                    currentEmployees[i].HireDate.ToString("d"), currentEmployees[i].Gender,
                    currentEmployees[i].Job, currentEmployees[i].HourlyRate.ToString("C"));
            }

            // Print the footer
            WriteLine("------------------------------------------------------------------------------");
        }
        
        // Method to add a new employee to the roster
        public static void AddNewEmployee()
        {
            // Declare variables to be taken from user input
            string empFirstName;
            string empLastName;
            int yearBirthDate = 1900;
            int monthBirthDate;
            int dayBirthDate;
            DateTime empBirthDate = new DateTime(1900, 1, 1);
            char empGender;
            int yearHireDate = 1900;
            int monthHireDate;
            int dayHireDate;
            DateTime empHireDate = new DateTime(1900, 1, 1);
            char empJobCode;
            string strHourlyRate;
            double empHourlyRate = 0.0;
            int periodCount = 0;
            int charCount = 0;

            // Initialize flags 
            bool validYear = false;
            bool validDate = false;
            bool validGender = false;
            bool validJobCode = false;
            bool validHourlyRate = false;

            WriteLine("\nAdding employees to the roster...");
            Write("Enter the first name of the employee: ");
            empFirstName = ReadLine();
            Write("Enter the last name of the employee: ");
            empLastName = ReadLine();

            do
            {
                Write("Enter the birth year of the employee: ");

                // Loop until the birth year is valid 
                // All hire dates must be after 1955.
                // There are no employees older than 65.
                do
                {
                    try
                    {
                        yearBirthDate = Convert.ToInt32(ReadLine());
                        if (yearBirthDate < 1955 || yearBirthDate > eighteenYearsAgo)
                            throw new UnreasonableYearException();
                        validYear = true;
                    }
                    catch (FormatException error)
                    {
                        WriteLine(" >> " + error.Message); 
                        Write(" >> Please enter a valid integer: ");
                    }
                    catch (UnreasonableYearException error)
                    {
                        WriteLine(" >> " + error.Message); 
                        Write(" >> Please enter a reasonable year between 1955 and " + eighteenYearsAgo + ": ");
                    }
                } while (!validYear);

                // Prompt for the employee's birth month
                Write("Enter the birth month of the employee: ");

                // Call a method to get a valid birth month
                monthBirthDate = GetValidMonth();

                // Prompt for the employee's birth day
                Write("Enter the birth day of the employee: ");

                // Call a method to get a valid birth day
                dayBirthDate = GetValidDay();

                // Try to create date object for the employee
                // Catch ArgumentOutOfRangeException if the constructor
                // for a DateTime object is given parameters that do not
                // coincide with a valid date.
                try
                {
                    empBirthDate = new DateTime(yearBirthDate, monthBirthDate, dayBirthDate);
                    validDate = true;
                }
                catch (ArgumentOutOfRangeException error)
                {
                    validDate = false;
                    WriteLine(" >> " + error.Message); 
                    WriteLine(" >> Please re-enter the year, month, and date for a valid date.");
                }
            } while (!validDate);

            // Prompt for the employee's gender 
            Write("Enter the gender of the employee: ");

            // Loop until a valid gender is entered
            do
            {
                empGender = ReadLine().ToUpper()[0];
                if (empGender == 'M' || empGender == 'F')
                    validGender = true;
                else
                {
                    validGender = false;
                    Write(" >> Please enter a valid gender (M, F): ");
                }
            } while (!validGender);

            // Reset the flag
            validDate = false;

            do
            {
                validYear = false;

                // Prompt for the hire year 
                Write("Enter the year of the hire date of the employee: ");

                // Loop until a valid hire year is entered:
                // -- The year must be 18 years or more before this year
                // -- The year must be 18 years or more after the employee's birth date
                do
                {
                    try
                    {
                        yearHireDate = Convert.ToInt32(ReadLine());
                        if (yearHireDate - yearBirthDate < 18 || yearHireDate > DateTime.Today.Year)
                            throw new UnreasonableYearException();
                        validYear = true;
                    }
                    catch (FormatException error)
                    {
                        WriteLine(" >> " + error.Message); 
                        Write(" >> Please enter a valid integer: ");
                    }
                    catch (UnreasonableYearException error)
                    {
                        WriteLine(" >> " + error.Message);
                        Write(" >> Please enter a valid year between " + (yearBirthDate + 18) 
                            + " and " + DateTime.Today.Year + ": ");
                    }
                } while (!validYear);

                // Prompt for the hire month
                Write("Enter the hire month of the employee: ");

                // Call a method to get a valid hire month
                monthHireDate = GetValidMonth();

                // Prompt for the employee's birth day
                Write("Enter the hire day of the employee: ");

                // Call a method to get a valid hire day
                dayHireDate = GetValidDay();

                // Try to create date object for the employee
                // Catch ArgumentOutOfRangeException if the constructor
                // for a DateTime object is given parameters that do not
                // coincide with a valid date.
                try
                {
                    // Set date object for the hire date
                    empHireDate = new DateTime(yearHireDate, monthHireDate, dayHireDate);
                    validDate = true;
                }
                catch (ArgumentOutOfRangeException error)
                {
                    WriteLine(" >> " + error.Message);
                    Write(" >> Please re-enter the year, month, and date for a valid date: ");
                }
            } while (!validDate);

            // Prompt for the job code
            Write("Enter the job code of the employee: ");

            do
            {
                empJobCode = ReadLine().ToUpper()[0];
                if (empJobCode == 'W' || empJobCode == 'A' || empJobCode == 'T' || empJobCode == 'D')
                    validJobCode = true;
                else
                {
                    validJobCode = false;
                    WriteLine(" >> Please enter a valid job code");
                    Write(" >> A: actor, D : doctor, T : teacher, W : waiter ");
                }
            } while (!validJobCode);

            // Prompt for the hourly rate
            Write("Enter the hourly rate of the employee: ");

            // Loop until a valid double is entered
            do
            {
                // Reset the counters
                periodCount = 0;
                charCount = 0;
                
                try
                {
                    strHourlyRate = ReadLine();
                    empHourlyRate = Convert.ToDouble(strHourlyRate);
                    
                    foreach (char character in strHourlyRate)
                    {
                        if (character == '.')
                            periodCount++;
                        else if (periodCount == 1)
                        {
                            charCount++;
                            if (charCount > 2)
                                throw new InvalidCurrencyException();
                        }
                            
                    }
                    validHourlyRate = true;
                }
                catch (FormatException error)
                {
                    WriteLine(" >> " + error.Message); 
                    Write(" >> Please enter a valid number: ");
                }
                catch (InvalidCurrencyException error)
                {
                    WriteLine(" >> " + error.Message);
                    Write(" >> Please enter a valid currency: ");
                }
            } while (!validHourlyRate);

            // Instantiate employee object
            Employee newEmployee = new Employee(empFirstName, empLastName, empGender, empBirthDate, empHireDate, empJobCode, empHourlyRate);

            // Add the employee to the array
            currentEmployees[employeeCount] = newEmployee;

            // Increment the employee count
            ++employeeCount;
        }

        // Method to get a valid month from the user
        // It handles its exceptions (FormatException, InvalidMonthException)
        public static int GetValidMonth()
        {
            // Declare the flag and the int to be returned 
            bool validMonth = false;
            int monthBirthDate = 1;

            // Repeat asking for an integer until it's between 1 and 12 (inclusive)
            do
            {
                try
                {
                    monthBirthDate = Convert.ToInt32(ReadLine());
                    if (monthBirthDate < 1 || monthBirthDate > 12)
                        throw new InvalidMonthException();
                    validMonth = true;
                }
                catch (FormatException error)
                {
                    WriteLine(" >> " + error.Message);
                    Write(" Please enter a valid integer: ");
                }
                catch (InvalidMonthException error)
                {
                    WriteLine(" >> " + error.Message); 
                    Write(" >> Please enter a valid month: ");
                }
            } while (!validMonth);

            // Return the value obtained
            return monthBirthDate;
        }

        // Method to get a valid birth day from the user 
        // It handles its exceptions (FormatException, InvalidDayException)
        public static int GetValidDay()
        {
            // Declare the flag and the int to be returned 
            bool validDay = false;
            int dayBirthDate = 1;

            // Repeat asking for an integer until it's between 1 and 31 (inclusive)
            do
            {
                try
                {
                    dayBirthDate = Convert.ToInt32(ReadLine());
                    if (dayBirthDate < 1 || dayBirthDate > 31)
                        throw new InvalidDayException();
                    validDay = true;
                }
                catch (FormatException error)
                {
                    WriteLine(" >> " + error.Message); 
                    Write(" >> Please enter a valid integer: ");
                }
                catch (InvalidDayException error)
                {
                    WriteLine(" >> " + error.Message); 
                    Write(" >> Please enter a valid day: ");
                }
            } while (!validDay);

            // Return the day obtained 
            return dayBirthDate;
        }

        // Method to remove employee from the currentEmployees array
        public static void RemoveEmployee()
        {
            // Declare variables for the method
            bool yesNoVerify = false;
            bool hireDateCheck = false;
            bool birthDateCheck = false;
            bool employeeFound = false;
            char seeListResponse;
            string employeeLastName;
            int employeeHireDateYear = 1900;
            int employeeBirthDateYear = 1900;
            int indexOfEmployee = employeeCount + 1;
            
            WriteLine("\nRemoving employees from the roster...");
            Write("Would you like to see the list of employees first? (Y/N) ");

            // Loop until the user enters Y or N
            do
            {
                seeListResponse = ReadLine().ToUpper()[0];

                if (seeListResponse == 'Y')
                    yesNoVerify = true;
                else if (seeListResponse == 'N')
                    yesNoVerify = true;
                else
                {
                    yesNoVerify = false;
                    Write(" >> Please enter a valid character (Y or N): ");
                }

            } while (!yesNoVerify);

            // Show the whole list of employees if the user entered Y
            if (seeListResponse == 'Y')
                ShowListOfEmployees();
            do
            {
                // Prompt the user for the last name 
                Write("Enter the last name of the employee to remove: ");
                employeeLastName = ReadLine();

                // Prompt the user for the year of birth
                Write("Enter the year of birth of the employee to remove: ");

                // Repeat until a valid year is entered 
                do
                {
                    try
                    {
                        employeeBirthDateYear = Convert.ToInt32(ReadLine());
                        if (employeeBirthDateYear < 1955 || employeeBirthDateYear > eighteenYearsAgo)
                            throw new UnreasonableYearException();
                        birthDateCheck = true;
                    }
                    catch (FormatException error)
                    {
                        WriteLine(" >> " + error.Message);
                        Write(" >> Please enter a valid integer: ");
                    }
                    catch (UnreasonableYearException error)
                    {
                        WriteLine(" >> " + error.Message);
                        Write(" >> Please enter a reasonable year between 1955 and " + eighteenYearsAgo + ": ");
                    }
                } while (!birthDateCheck);

                // Prompt the user for the year of the hire date
                Write("Enter the year of the hire date of the employee to remove: ");

                // Repeat until a valid year is entered 
                do
                {
                    try
                    {
                        employeeHireDateYear = Convert.ToInt32(ReadLine());
                        if (employeeHireDateYear - employeeBirthDateYear < 18 || employeeHireDateYear > DateTime.Today.Year)
                            throw new UnreasonableYearException();
                        hireDateCheck = true;
                    }
                    catch (FormatException error)
                    {
                        WriteLine(" >> " + error.Message);
                        Write(" >> Please enter a valid year: ");
                    }
                    catch (UnreasonableYearException error)
                    {
                        WriteLine(" >> " + error.Message);
                        Write(" >> Please enter a valid year between " + (employeeBirthDateYear + 18)
                            + " and " + DateTime.Today.Year + ": ");
                    }
                } while (!hireDateCheck);

                // Loop through the employees array to find the employee
                for (int l = 0; l < employeeCount; ++l)
                {
                    if (currentEmployees[l].LastName.Equals(employeeLastName)
                        && currentEmployees[l].BirthDate.Year == employeeBirthDateYear
                        && currentEmployees[l].HireDate.Year == employeeHireDateYear)
                    {
                        WriteLine("Removing employee " + currentEmployees[l].LastName + " born in "
                            + currentEmployees[l].BirthDate.Year + " hired in " + currentEmployees[l].HireDate.Year + "...");
                        currentEmployees[l] = null;
                        indexOfEmployee = l;
                        employeeFound = true;
                    }
                } 

                if (!employeeFound)
                {
                    Write("The employee was not found. Would you like to try again? (Y/N) ");
                    
                    // Loop until the user enters Y or N
                    do
                    {
                        seeListResponse = ReadLine().ToUpper()[0];

                        if (seeListResponse == 'Y')
                            yesNoVerify = true;
                        else if (seeListResponse == 'N')
                            return;
                        else
                        {
                            yesNoVerify = false;
                            Write(" >> Please enter a valid character (Y or N): ");
                        }

                    } while (!yesNoVerify);
                }

            } while (!employeeFound);

            // Shift all the members after the employee back
            for (int j = indexOfEmployee; j < employeeCount - 1; ++j)
            {
                currentEmployees[j] = currentEmployees[j + 1];
            }

            // Eliminate the final employee (double) and update the employee count
            currentEmployees[employeeCount] = null;
            --employeeCount;
        }

        // Method to see all employees with a specific job code
        public static void SearchEmployeesByJobCode()
        {
            // Declare the variables to be used in this method
            bool validJobCode = false;
            char userJobCodeInput;

            // Print the title for the method
            WriteLine("\nSearching for employees...");

            // Prompt the user for a job code
            Write("Please enter a job code: ");
            
            // Loop until a valid job code is entered 
            do
            {
                userJobCodeInput = ReadLine().ToUpper()[0];

                if (userJobCodeInput == 'A' || userJobCodeInput == 'D' || userJobCodeInput == 'T' || userJobCodeInput == 'W')
                    validJobCode = true;
                else
                {
                    WriteLine(" >> Please enter a valid job code");
                    Write(" >> A: actor, D : doctor, T : teacher, W : waiter ");
                }
            } while (!validJobCode);

            WriteLine("Employees with the job code " + userJobCodeInput + ":");

            // Print out the members of currentEmployees array that have the job code
            for (int h = 0; h < employeeCount; ++h)
            {
                if (currentEmployees[h].JobCode == userJobCodeInput)
                {
                    WriteLine(" > " + currentEmployees[h]);
                }
            }
        }
    }
}