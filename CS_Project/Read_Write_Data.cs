using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Project_Air_Quality_App
{
    internal class DataReader
    {
        //Ussage: Parse the csv file based on the month/obs/day and populate the values in the private list fields from Class Day
        //input:  ref Day targetDay -> used as a container to save the values readed from csv file
        //        string month, obsID, dayID -> used to indentify the csv file from where to read
        //        if one values is empty in the csv file, the default will be -1
        public static void ReadData(ref Day targetDay, string month, string obsID, string dayID)
        {
            //Parse the folder/files and populate the strings array with the values 
            string path = $"..\\..\\..\\data_missing\\{month}\\obs{obsID}\\observation_{dayID}.csv";
            StreamReader readFile = new StreamReader(path);
            string[] sReadfile= { };
            try
            {
                sReadfile = System.IO.File.ReadAllLines(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception from read lines: {ex.Message}");
            }
            //Goes line by line
            foreach(string s in sReadfile)
            {
                string[] splitString = s.Split(',');
                switch(splitString[0])
                {
                    case "temperature":
                        for(int index = 1; index <= splitString.Length - 1; index++)
                        {
                            if (splitString[index] == " ")
                            {
                                targetDay.AddTemperature(-1);
                            }
                            else
                            {
                                try
                                {
                                    if (double.TryParse(splitString[index], out double value))
                                    {
                                        targetDay.AddTemperature(value);
                                    }
                                }
                                catch (Exception e)
                                {

                                    Console.WriteLine($"Exception from add temperature: {e.Message}");
                                }
                            }
                        }
                        break;
                    case "humidity":
                        for (int index = 1; index <= splitString.Length - 1; index++)
                        {
                            if (splitString[index] == " ")
                            {
                                targetDay.AddHumidity(-1);
                            }
                            else
                            {
                                try
                                {
                                    if (double.TryParse(splitString[index], out double value))
                                    {
                                        targetDay.AddHumidity(value);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Exception from add humidity: {e.Message}");
                                }
                            }
                        }
                        break;
                    case "clouds_prob":
                        for (int index = 1; index <= splitString.Length - 1; index++)
                        {
                            if (splitString[index] == " ")
                            {
                                targetDay.AddCloudsProb(-1);
                            }
                            else
                            {
                                try
                                {
                                    if (double.TryParse(splitString[index], out double value))
                                    {
                                        targetDay.AddCloudsProb(value);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Exception from add clouds_prob: {e.Message}");
                                }
                            }
                        }
                        break;
                    case "no_cars":
                        for (int index = 1; index <= splitString.Length - 1; index++)
                        {
                            if (splitString[index] == " ")
                            {
                                targetDay.AddNoCars(-1);
                            }
                            else
                            {
                                try
                                {
                                    if (int.TryParse(splitString[index], out int value))
                                    {
                                        targetDay.AddNoCars(value);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Exception from add no_cars: {e.Message}");
                                }
                            }
                        }
                        break;
                    case "no_flights":
                        for (int index = 1; index <= splitString.Length - 1; index++)
                        {
                            if (splitString[index] == " ")
                            {
                                targetDay.AddNoFlights(-1);
                            }
                            else
                            {
                                try
                                {
                                    if (int.TryParse(splitString[index], out int value))
                                    {
                                        targetDay.AddNoFlights(value);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Exception from add no_flights: {e.Message}");
                                }
                            }
                        }
                        break;
                    case "factories":
                        for (int index = 1; index <= splitString.Length - 1; index++)
                        {
                            if (splitString[index] == " ")
                            {
                                targetDay.AddFactories(-1);
                            }
                            else
                            {
                                try
                                {
                                    if (int.TryParse(splitString[index], out int value))
                                    {
                                        targetDay.AddFactories(value);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Exception from add factories: {e.Message}");

                                }
                            }
                        }
                        break;
                    case "label":
                        for (int index = 1; index <= splitString.Length - 1; index++)
                        {
                            try
                            {
                                if (splitString[index] != null)
                                    if (splitString[index].Trim() == "Low" || splitString[index].Trim() == "Medium" || splitString[index].Trim() == "High" || splitString[index].Trim() == "")
                                        targetDay.AddLabel(splitString[index]);
                                    else
                                        throw new Exception("Wrong value for \"label\" field in csv file for add label method");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Exception from add labels: {e.Message}");
                            }
                        }
                        break;
                    default:
                            throw new Exception("Unexpected value for first field in csv file!");
                }
            }
        }
        //Usage: Used to print to console as a table all the private fields from Day object
        public static void WriteDataConsole(Day targetDay)
        {
            Console.Write("Day:" + targetDay.dayID);
            string[] headers = { " 6:00", " 7:00", " 8:00", " 9:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00" };
            foreach(string hour in headers)
            {
                Console.Write(" | " + hour);
            }
            Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------------------------------");
            Console.Write("Temp:  ");
            for ( int hour = 6; hour<22;hour++)
            {
                string str = targetDay.GetTemperature(hour).ToString("F2");
                if (double.Parse(str) != -1)
                    Console.Write("| "+str+ " ");
                else
                    Console.Write("|MissInf");
            }
            Console.Write("\nHumid: ");
            for (int hour = 6; hour < 22; hour++)
            {
                string str = targetDay.GetHumidity(hour).ToString("00");
                if (int.Parse(str) != -1)
                    Console.Write("|  " + str + "   ");
                else
                    Console.Write("|MissInf");
            }
            Console.Write("\nCloud: ");
            for (int hour = 6; hour < 22; hour++)
            {
                string str = targetDay.GetCloudsProb(hour).ToString("00");
                if (int.Parse(str) != -1)
                    Console.Write("|  " + str + "   ");
                else
                    Console.Write("|MissInf");
            }
            Console.Write("\nNoCar: ");
            for (int hour = 6; hour < 22; hour++)
            {
                string str = targetDay.GetNoCars(hour).ToString("000");
                if (int.Parse(str) != -1)
                    Console.Write("| " + str + "  ");
                else
                    Console.Write("|MissInf");
            }
            Console.Write("\nNFligh:");
            for (int hour = 6; hour < 22; hour++)
            {
                string str = targetDay.GetNoFlights(hour).ToString("00");
                if (int.Parse(str) != -1)
                    Console.Write("|  " + str + "   ");
                else
                    Console.Write("|MissInf");
            }
            Console.Write("\nFact:  ");
            for (int hour = 6; hour < 22; hour++)
            {
                string str = targetDay.GetFactories(hour).ToString("00");
                if (int.Parse(str) != -1)
                    Console.Write("|  " + str + "   ");
                else
                    Console.Write("|MissInf");
            }
            Console.WriteLine();
        }
        //Usage: Used to write to a file as a table all the private fields from Day object
        public static void WriteDataFile(Day targetDay)
        {

        }
    }
    internal class DataCompare
    {
        //Usage: Used to compare 2 days in a graphical way
        //Parameters:
        //      - day_info = string with details about the day: Month, Observatory
        //      - day = object of type Day

        public static void Compare_two_days_as_Table(string day1_info, Day day1, string day2_info, Day day2)
        {
            Console.WriteLine("\n"+day1_info);
            DataReader.WriteDataConsole(day1);
            Console.WriteLine("\n"+day2_info);
            DataReader.WriteDataConsole(day2);
        }
    }
    
    internal class DataWriter
    {

    }
}
