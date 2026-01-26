using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Learning_About_APIs;

class Program
{
    static bool ProgramRunning = true;
    static async Task Main()
    {
        while(ProgramRunning)
        {
            Console.Clear();
            Console.WriteLine("Would you like to hear a cat fact?");
            Console.WriteLine("Enter Y for YES and N for NO");

            string? userInput = Console.ReadLine();

            if(userInput?.ToLower() == "y")
            {
                await GetCatFact();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            else if(userInput?.ToLower() == "n")
            {
                ProgramRunning = false;
            }
        }
    }

    static async Task GetCatFact()
    {
        // this creates our HttpClient which allows us to do stuff with web requests
        using HttpClient client = new HttpClient();

        try
        {
            // we need a URL to access the API from
            string url = "https://catfact.ninja/fact";
            // we then setup a variable for the response and await for the the client to to return the URL to us
            string response = await client.GetStringAsync(url);

            // we then just display what was retrieved from the URL
            Console.WriteLine("API Response:");
            Console.WriteLine(response);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
