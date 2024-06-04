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
            Console.Write("\nLabel: ");
            for (int hour = 6; hour < 22; hour++)
            {
                string str = targetDay.GetLabel(hour);
                if (str == "Low") { Console.Write("|  Low   "); }
                else if (str == "Medium") { Console.Write("| Medium "); }
                else if (str == "High") { Console.Write("| High  "); }
                else Console.Write("|   -   ");


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

        public static void Compare_two_days_as_Table(string day1_info, Day Day1, string day2_info, Day Day2)
        {
            Console.WriteLine("\n" + day1_info);
            DataReader.WriteDataConsole(Day1);
            Console.WriteLine("\n" + day2_info);
            DataReader.WriteDataConsole(Day2);

            Console.WriteLine("\n======================================================================================================================================\n");
            Console.WriteLine("Statistics:\n");

            int number_of_missing_info = 0;
            string[] data_to_print = { "Temperature", "Humidity", "NoCars", "NoFlights", "Factories", "CloudsProb" };
            foreach (string crt_data in data_to_print)
            {
                number_of_missing_info = DataCompare.Print_Statistics(Day1, Day2, crt_data, number_of_missing_info);
            }
                

            Console.WriteLine("\nThe number of missing info is: " + number_of_missing_info +"\n");
        }

        public static int Print_Statistics(Day Day1, Day Day2,string statistics_about, int nr_of_missing_info)
        {
            // min max
            double min_crt_val = double.MaxValue;
            double max_crt_val = double.MinValue;
            int max_crt_val_day = 0;
            int min_crt_val_day = 0;
            int min_day_hour = 0;
            int max_day_hour = 0;
            // mean
            int nr_to_divide = 0;
            double sum_of_values = 0;
            for (int hour = 6; hour < 22; hour++)
            {
                string str_1;
                string str_2;
                switch (statistics_about)
                {
                    case "Temperature":
                        str_1 = Day1.GetTemperature(hour).ToString("F2");
                        str_2 = Day2.GetTemperature(hour).ToString("F2");
                        break;
                    case "Humidity":
                        str_1 = Day1.GetHumidity(hour).ToString("F2");
                        str_2 = Day2.GetHumidity(hour).ToString("F2");
                        break;
                    case "NoCars":
                        str_1 = Day1.GetNoCars(hour).ToString("F2");
                        str_2 = Day2.GetNoCars(hour).ToString("F2");
                        break;
                    case "NoFlights":
                        str_1 = Day1.GetNoFlights(hour).ToString("F2");
                        str_2 = Day2.GetNoFlights(hour).ToString("F2");
                        break;
                    case "Factories":
                        str_1 = Day1.GetFactories(hour).ToString("F2");
                        str_2 = Day2.GetFactories(hour).ToString("F2");
                        break;
                    case "CloudsProb":
                        str_1 = Day1.GetCloudsProb(hour).ToString("F2");
                        str_2 = Day2.GetCloudsProb(hour).ToString("F2");
                        break;
                    default:
                        str_1 = Day1.GetTemperature(hour).ToString("F2");
                        str_2 = Day2.GetTemperature(hour).ToString("F2");
                        break;

                }
                // min val
                if (double.Parse(str_1) < min_crt_val && double.Parse(str_1) != -1) { min_crt_val = double.Parse(str_1); min_crt_val_day = Day1.dayID; min_day_hour = hour; }
                if (double.Parse(str_2) < min_crt_val && double.Parse(str_2) != -1) { min_crt_val = double.Parse(str_2); min_crt_val_day = Day2.dayID; min_day_hour = hour; }
                // max val
                if (double.Parse(str_1) > max_crt_val && double.Parse(str_1) != -1) { max_crt_val = double.Parse(str_1); max_crt_val_day = Day1.dayID; max_day_hour = hour; }
                if (double.Parse(str_2) > max_crt_val && double.Parse(str_2) != -1) { max_crt_val = double.Parse(str_2); max_crt_val_day = Day2.dayID; max_day_hour = hour; }
                // mean val and missing info count
                if (double.Parse(str_1) == -1.0) { nr_of_missing_info++; }
                else
                {
                    sum_of_values += double.Parse(str_1);
                    nr_to_divide++;
                }
                if (double.Parse(str_2) == -1.0) { nr_of_missing_info++; }
                else
                {
                    sum_of_values += double.Parse(str_2);
                    nr_to_divide++;
                }
            }
            Console.WriteLine(statistics_about + ":");
            Console.WriteLine("\tMean: " + (sum_of_values / nr_to_divide).ToString("F2"));
            Console.WriteLine("\tMax:  " + max_crt_val + " at day: " + max_crt_val_day + ", hour: " + max_day_hour + ":00");
            Console.WriteLine("\tMin:  " + min_crt_val + " at day: " + min_crt_val_day + ", hour: " + min_day_hour + ":00");

            return nr_of_missing_info;
        }
    }
    
    internal class DataWriter
    {

    }
}
