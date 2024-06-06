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
        public List<Observator> ObservatorsList = new List<Observator>();
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

            if (ObservatorsList.Count == 0)
            {
                Observator newObs = new Observator() { month = userInput[0], obsID = userInput[2] };
                newObs.ReadDataOfDay(userInput[1]);
                ObservatorsList.Add(newObs);
                newObs.ShowDataOfDay(userInput[1]);
            }
            else 
            { 
                bool obsFind = false;
                foreach (Observator observator in ObservatorsList)
                {
                    if(observator.month == userInput[0] && observator.obsID == userInput[2])
                    {   
                        obsFind = true;
                        bool findFlg = false;
                        foreach (Day currentDay in observator.days)
                        {
                            if (currentDay.dayID == int.Parse(userInput[1]))
                            {
                                observator.ShowDataOfDay(userInput[1]);
                                Console.WriteLine("Day already in the list");
                                findFlg = true;
                                break;
                            }
                        }
                        if (findFlg == false) 
                        {
                            observator.ReadDataOfDay(userInput[1]);
                            observator.ShowDataOfDay(userInput[1]);
                        }
                        break;
                    }

                }
                if (obsFind == false) 
                {
                    Observator newObs = new Observator() { month = userInput[0], obsID = userInput[2] };
                    newObs.ReadDataOfDay(userInput[1]);
                    ObservatorsList.Add(newObs);
                    newObs.ShowDataOfDay(userInput[1]);
                }

            }
        }

        private void CompareData()
        {
            Console.WriteLine("Chose first data to compare: ");
            List<string> userInput1 = GetUserInput();
            Console.WriteLine("Chose second data to compare: ");
            List<string> userInput2 = GetUserInput();

            Day newDay1 = new Day { dayID = int.Parse(userInput1[1]) };
            Day newDay2 = new Day { dayID = int.Parse(userInput2[1]) };

            Observator newObs1 = new Observator()
            {
                month = userInput1[0],
                obsID = userInput1[2],
            };
            DataReader.ReadData(ref newDay1, newObs1.month, newObs1.obsID, userInput1[1]);

            Observator newObs2 = new Observator()
            {
                month = userInput2[0],
                obsID = userInput2[2],
            };
            DataReader.ReadData(ref newDay2, newObs2.month, newObs2.obsID, userInput2[1]);

            string info1 = "Observator: " + userInput1[2] + ", Day: " + userInput1[1];
            string info2 = "Observator: " + userInput2[2] + ", Day: " + userInput2[1];


            DataCompare.Compare_two_days_as_Table(info1, newDay1, info2, newDay2);

        }

        static void Main(string[] args)
        {
            
            //MENU
            //1)Show Data
            //-> Take from the user: Month, Obs, Day
            //-> Use object of type Observatori -> to Read Data and populate the fields Month/Observatory/Day/values
            //-> Call a method to WriteDataToConsole 
            Observator currentObservator = new Observator();
            string[] inputThing = { "July", "3", "20" };
            //currentObservator.month = inputThing[0];
            //currentObservator.obsID = inputThing[1];
            //currentObservator.ReadDataOfDay(inputThing[2]);
            //ObservatorsList.Add(currentObservator);

            //string[] inputThing2 = { "June", "4", "21" };
            //currentObservator.month = inputThing2[0];
            //currentObservator.obsID = inputThing2[1];
            //currentObservator.ReadDataOfDay(inputThing2[2]);
            //ObservatorsList.Add(currentObservator);


            //Console.WriteLine("\n" + ObservatorsList[1].month + " obs" + ObservatorsList[1].obsID);
            //DataReader.WriteDataConsole(ObservatorsList[1].days[0]);
            //ObservatorsList[1].days[0].MakePredictions();
            //DataReader.WriteDataConsole(ObservatorsList[1].days[0]);

            //foreach (Day currentDay in ObservatorsList[0].days)
            //{
            //    if (currentDay.dayID == 20)
            //        Console.WriteLine($"From month {ObservatorsList[0].month}, observatory obs{ObservatorsList[0].obsID}, day {currentDay.dayID} temperature at hour 10 is: {currentDay.GetTemperature(10)}");
            //}

            //Example of usage
            //DataCompare.Compare_two_days_as_Table("info1", ObservatorsList[0].days[0], "info2", ObservatorsList[1].days[1]);

            //2)Compare Data
            //-> Take from the user info about first data: Month, Obs, Day
            //-> Use object of type Observatori -> to Read Data and populate the fields Month/Observatory/Day/values
            //-> Take from the user info about second data: Month, Obs, Day
            //-> Use object of type Observatori -> to Read Data and populate the fields Month/Observatory/Day/values
            
            //3)Get average from a day for a field -> temperature/humidity


            CommandCenter commandCenter = new CommandCenter();
            commandCenter.Start();
        }
    }
}
