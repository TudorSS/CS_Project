using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CS_Project_Air_Quality_App
{
    internal class CommandCenter
    {
        private void DisplayMenu()
        {
            Console.WriteLine("1. Visualize Data");
            Console.WriteLine("2. Compare data");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Chose an option: ");
        }
        public void Start()
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewData();
                        
                        break;
                    case "2":
                        CompareData();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private List<string> GetUserInput()
        {
            List<string> userInput = new List<string>();

            string month = "";
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Enter the month: ");
                string inputMonth = Console.ReadLine().ToLower();

                switch (inputMonth)
                {
                    case "may":
                        month = "May";
                        flag = false;
                        break;
                    case "june":
                        month = "June";
                        flag = false;
                        break;
                    case "july":
                        month = "July";
                        flag = false;
                        break;
                    case "august":
                        month = "August";
                        flag = false;
                        break;
                    case "september":
                        month = "September";
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid month. Please enter a month between May and September.");
                        break; // Add break to prevent adding month to userInput when invalid
                }
            }
            userInput.Add(month); // Add the selected month to the userInput list

            bool isValidInput = false;
            int day;

            while (!isValidInput)
            {
                Console.WriteLine("Enter the day (1-31): ");
                string inputDay = Console.ReadLine();

                try
                {
                    day = int.Parse(inputDay);

                    if (day >= 1 && day <= 31)
                    {
                        isValidInput = true; 
                        userInput.Add(inputDay); 
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (FormatException)
                {
                    // Handle invalid input (non-numeric input)
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Handle invalid input (number out of range)
                    Console.WriteLine("Invalid input. Please enter a day between 1 and 31.");
                }
            }

            isValidInput = false;
            int observatory;

            while (!isValidInput)
            {
                Console.WriteLine("Enter an observatory (1-5): ");
                string inputObservatory = Console.ReadLine();

                try
                {
                    observatory = int.Parse(inputObservatory);

                    if (observatory >= 1 && observatory <= 5)
                    {
                        userInput.Add(inputObservatory);
                        isValidInput = true;                        
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (FormatException)
                {
                    // Handle invalid input (non-numeric input)
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Handle invalid input (number out of range)
                    Console.WriteLine("Invalid input. Please enter a day between 1 and 5.");
                }
            }

            return userInput;
        }

        private void ViewData()
        {
            List<string> userInput = GetUserInput();

            //Debug 

        }

        private void CompareData()
        {
            Console.WriteLine("Chose first data to compare: ");
            List<string> userInput1 = GetUserInput();
            Console.WriteLine("Chose second data to compare: ");
            List<string> userInput2 = GetUserInput();
        }

        static void Main(string[] args)
        {
            Observator myObs = new Observator();
            //MENU
            //1)Show Data
            //-> Take from the user: Month, Obs, Day
            //-> Use object of type Observatori -> to Read Data and populate the fields Month/Observatory/Day/values
            //-> Call a method to WriteDataToConsole 
            string[] inputThing = { "July", "3", "20" };
            myObs.month = inputThing[0];
            myObs.obsID = inputThing[1];
            myObs.ReadDataOfDay(inputThing[2]);
            string[] inputThing2 = { "July", "3", "21" };
            myObs.ReadDataOfDay(inputThing2[2]);
            foreach (Day currentDay in myObs.days)
            {
                if (currentDay.dayID == 20)
                    Console.WriteLine($"From month {myObs.month}, observatory obs{myObs.obsID}, day {currentDay.dayID} temperature at hour 10 is: {currentDay.GetTemperature(10)}");
            }

            //Example of usage
            DataCompare.Compare_two_days_as_Table("info1", myObs.days[0], "info2", myObs.days[1]);

            //2)Compare Data
            //-> Take from the user info about first data: Month, Obs, Day
            //-> Use object of type Observatori -> to Read Data and populate the fields Month/Observatory/Day/values
            //-> Take from the user info about second data: Month, Obs, Day
            //-> Use object of type Observatori -> to Read Data and populate the fields Month/Observatory/Day/values
            
            //3)Get average from a day for a field -> temperature/humidity


            //CommandCenter commandCenter = new CommandCenter();
            //commandCenter.Start();
        }
    }
}
