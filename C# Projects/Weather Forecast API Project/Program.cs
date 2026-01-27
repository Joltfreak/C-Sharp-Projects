using System;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace Weather_Forecast_API_Project;

class Program
{
    static bool ProgramRunning = true;
    static string LAT = string.Empty;
    static string LON = string.Empty;
    static void Main()
    {
        while
        (ProgramRunning)
        {
            Console.Clear();
            Console.WriteLine("Do you want to check you local temperatue?");
            Console.WriteLine("Enter Y for YES and N for NO");
            string? userInput = Console.ReadLine();

            if(userInput.ToLower() == "y")
            {
                Console.Write("Please enter a LATITUDE: ");
                LAT = Console.ReadLine();
                Console.Write("Please enter a LONGITUDE: ");
                LON = Console.ReadLine();

                GetWeatherData(LON, LAT).Wait();

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if(userInput.ToLower() == "n")
            {
                ProgramRunning = false;
            }
        }
    }

    static async Task GetWeatherData(string lon, string lat)
    {
        // create a new HTTP Client
        using HttpClient client = new HttpClient();

        try
        {
            // we then declare the URL
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly=temperature_2m&temperature_unit=fahrenheit";

            // we await a response from the URL
            string jsonResponse = await client.GetStringAsync(url);
            
            // we use the JsonNode.Parse() because we need a dynamic like object.
            // we could use something like JsonSerializer.Deserialize but that requires it intaking something like a Class.
            var weatherData = JsonNode.Parse(jsonResponse);
            var currentTemp = weatherData["hourly"]["temperature_2m"][0];
            var tempUnit = weatherData["hourly"]["temperature_unit"];

            Console.WriteLine($"Current Temperature: {currentTemp}{tempUnit}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
