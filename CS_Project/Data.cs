using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_Project_Air_Quality_App
{
    public class Observator
    {
        //We can use enum instead of strigns 
        public string month;
        public string obsID;
        public List<Day> days = new List<Day>();

        public void ReadDataOfDay(string dayID)
        {
            Day auxDay = new Day();
            try
            {
                auxDay.dayID = int.Parse(dayID);
                DataReader.ReadData(ref auxDay, month, obsID, dayID);
            }
            catch(Exception ex)
            { 
                Console.WriteLine($"Exception from Read Data function: {ex.Message}");
            } 
            days.Add(auxDay);
        }
        public void ShowDataOfDay(string dayID)
        {
            try
            {
                foreach (Day currentDay in this.days)
                {
                    if (currentDay.dayID == int.Parse(dayID))
                    {
                        DataReader.WriteDataConsole(currentDay);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from Show Data Of Day function: {ex.Message}");
            }
            
        }
    }
    public class Day
    {
        public int dayID;
        private List<double> temperature = new List<double>();
        private List<double> humidity = new List<double>();
        private List<double> clouds_prob = new List<double>();
        private List<int> no_cars = new List<int>();
        private List<int> no_flights =new List<int>();
        private List<int> factories = new List<int>();
        private List<string> label = new List<string>();
        private const int offset = 6;//matching between hours and index will be hours 6 -> index 0 in the list


        //Method to add a temperature to the private list of temperatures
        public void AddTemperature(double temperature) {
            this.temperature.Add(temperature);
        }
        //Method used to update a temperature to a specified hour of the private list of temperature
        //matching between hours and index will be hours - offset -> index 0 in the list will be hour 6
        public void SetTemperature(double temperature, int hour)
        {
            try
            {
                this.temperature[hour - Day.offset] = temperature;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception from set temperature: {ex.Message}");
            }
        }
        //Method used to get a temperature from a specified hour of the private list of temperatures
        //matching between hours and index will be hours - offset -> index 0 in the list will be hour 6
        public double GetTemperature(int hour) {
            try
            {
                return this.temperature.ElementAt(hour - Day.offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from get temperature: {ex.Message}");
                return 0;
            }
           
        }
        //Method to add a humidity to the private list of humidity
        public void AddHumidity(double humidity) {
            this.humidity.Add(humidity);
        }
        //Method used to update a humidity to a specified hour of the private list of humidity
        //matching between hours and index will be hours - offset -> index 0 in the list will be hour 6
        public void SetHumidity(double humidity, int hour)
        {
            try
            {
                this.humidity[hour - Day.offset] = humidity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from set humidity: {ex.Message}");
            }
        }
        //Method used to get a humidity from a specified hour of the private list of humidity
        //matching between hours and index will be hours - offset -> index 0 in the list will be hour 6
        public double GetHumidity(int hour) {
            try
            {
                return this.humidity.ElementAt(hour - Day.offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from get humidity: {ex.Message}");
                return 0;
            }
        }
        public void AddCloudsProb(double cloudsProb) {
            this.clouds_prob.Add(cloudsProb);
        }
        public void SetCloudsProb(double cloudProb, int hour)
        {
            try
            {
                this.clouds_prob[hour - Day.offset] = cloudProb;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from set cloud_prob: {ex.Message}");
            }
        }
        public double GetCloudsProb(int hour) {
            try
            {
                return this.clouds_prob.ElementAt(hour - Day.offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from get cloud_prob: {ex.Message}");
                return 0;
            }
        }
        public void AddNoCars(int noCars) {
            this.no_cars.Add(noCars);
        }
        public void SetNoCars(int noCars, int hour)
        {
            try
            {
                this.no_cars[hour - Day.offset] = noCars;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from set no_cars: {ex.Message}");
            }
        }
        public int GetNoCars(int hour) {
            try
            {
                return this.no_cars.ElementAt(hour - Day.offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from get no_cars: {ex.Message}");
                return 0;
            }
        }
        public void AddNoFlights(int noFlights) {
            this.no_flights.Add(noFlights); 
        }
        public void SetNoFlights(int noFlights, int hour)
        {
            try
            {
                this.no_flights[hour - Day.offset] = noFlights;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from set no_flights: {ex.Message}");
            }
        }
        public int GetNoFlights(int hour) {
            try
            {
                return this.no_flights.ElementAt(hour - Day.offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from get no_flights: {ex.Message}");
                return 0;
            }
        }
        public void AddFactories(int factories) {
            this.factories.Add(factories);
        }
        public void SetFactories(int factories, int hour)
        {
            try
            {
                this.factories[hour - Day.offset] = factories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from set factories: {ex.Message}");
            }
        }
        public int GetFactories(int hour) {
            try
            {
                return this.factories.ElementAt(hour - Day.offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from get factories: {ex.Message}");
                return 0;
            }
        }
        //Method to add a label to the private list of label
        public void AddLabel(string label) {
            this.label.Add(label);
        }
        //Method used to update a label to a specified hour of the private list of labels
        //matching between hours and index will be hours - offset -> index 0 in the list will be hour 6
        //Add only accepted label "Low", "Medium", "High"
        public void SetLabel(string label, int hour)
        {
            try
            {
                if (label.Trim() == "Low" || label.Trim() == "Medium" || label.Trim() == "High")
                    this.label[hour - Day.offset] = label.Trim();
                else
                    throw new Exception("Wrong value for \"label\". Need to be Low, Medium or High.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from set factories: {ex.Message}");
            }
        }
        //Method used to get a label from a specified hour of the private list of labels
        //matching between hours and index will be hours - offset -> index 0 in the list will be hour 6
        public string GetLabel(int hour)
        {
            try
            {
                return this.label.ElementAt(hour - Day.offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from get label: {ex.Message}");
                return null;
            }
           
        }

        //Make prediction for missing data
        public void MakePredictions()
        {
            //Prediction for temperature
            double average = 0;
            int count = 0;
            int countMissing = 0;
            for (int indexOfElements = 0; indexOfElements < this.temperature.Count; indexOfElements++)//16 is the number of hours from the input data
            {
                if (this.temperature[indexOfElements] != -1)
                {
                    average += this.temperature[indexOfElements];
                    count++;
                }
                else
                {
                    countMissing++;
                }
            }
            if (countMissing > 0)
            {
                Console.WriteLine($"For temperature {countMissing} predictions were made!");
                if (count > 0)
                {
                    average /= count;
                }
                for (int index = 0; index < this.temperature.Count; index++)
                {
                    if (this.temperature[index] == -1)
                    {
                        this.temperature[index] = average;
                    }
                }
            }
            //Prediction for humidity
            average = 0;
            count = 0;
            countMissing = 0;
            for (int indexOfElements = 0; indexOfElements < this.humidity.Count; indexOfElements++)//16 is the number of hours from the input data
            {
                if (this.humidity[indexOfElements] != -1)
                {
                    average += this.humidity[indexOfElements];
                    count++;
                }
                else
                {
                    countMissing++;
                }
            }
            if (countMissing > 0)
            {
                Console.WriteLine($"For humidity {countMissing} predictions were made!");
                if (count > 0)
                {
                    average /= count;
                }
                for (int index = 0; index < this.humidity.Count; index++)
                {
                    if (this.humidity[index] == -1)
                    {
                        this.humidity[index] = average;
                    }
                }
            }
            //Prediction for clouds_prob
            average = 0;
            count = 0;
            countMissing = 0;
            for (int indexOfElements = 0; indexOfElements < this.clouds_prob.Count; indexOfElements++)//16 is the number of hours from the input data
            {
                if (this.clouds_prob[indexOfElements] != -1)
                {
                    average += this.clouds_prob[indexOfElements];
                    count++;
                }
                else
                {
                    countMissing++;
                }
            }
            if (countMissing > 0)
            {
                Console.WriteLine($"For clouds_prob {countMissing} predictions were made!");
                if (count > 0)
                {
                    average /= count;
                }
                for (int index = 0; index < this.clouds_prob.Count; index++)
                {
                    if (this.clouds_prob[index] == -1)
                    {
                        this.clouds_prob[index] = average;
                    }
                }
            }
            //Prediction for no_cars
            average = 0;
            count = 0;
            countMissing = 0;
            for (int indexOfElements = 0; indexOfElements < this.no_cars.Count; indexOfElements++)//16 is the number of hours from the input data
            {
                if (this.no_cars[indexOfElements] != -1)
                {
                    average += this.no_cars[indexOfElements];
                    count++;
                }
                else
                {
                    countMissing++;
                }
            }
            if (countMissing > 0)
            {
                Console.WriteLine($"For no_cars {countMissing} predictions were made!");
                if (count > 0)
                {
                    average /= count;
                }
                for (int index = 0; index < this.no_cars.Count; index++)
                {
                    if (this.no_cars[index] == -1)
                    {
                        this.no_cars[index] = (int)average;
                    }
                }
            }
            //Prediction for no_flights
            average = 0;
            count = 0;
            countMissing = 0;
            for (int indexOfElements = 0; indexOfElements < this.no_flights.Count; indexOfElements++)//16 is the number of hours from the input data
            {
                if (this.no_flights[indexOfElements] != -1)
                {
                    average += this.no_flights[indexOfElements];
                    count++;
                }
                else
                {
                    countMissing++;
                }
            }
            if (countMissing > 0)
            {
                Console.WriteLine($"For no_cars {countMissing} predictions were made!");
                if (count > 0)
                {
                    average /= count;
                }
                for (int index = 0; index < this.no_flights.Count; index++)
                {
                    if (this.no_flights[index] == -1)
                    {
                        this.no_flights[index] = (int)average;
                    }
                }
            }
            //Prediction for factories
            average = 0;
            count = 0;
            countMissing = 0;
            for (int indexOfElements = 0; indexOfElements < this.factories.Count; indexOfElements++)//16 is the number of hours from the input data
            {
                if (this.factories[indexOfElements] != -1)
                {
                    average += this.factories[indexOfElements];
                    count++;
                }
                else
                {
                    countMissing++;
                }
            }
            if (countMissing > 0)
            {
                Console.WriteLine($"For factories {countMissing} predictions were made!");
                if (count > 0)
                {
                    average /= count;
                }
                for (int index = 0; index < this.factories.Count; index++)
                {
                    if (this.factories[index] == -1)
                    {
                        this.factories[index] = (int)average;
                    }
                }
            }
        }
    }
}
