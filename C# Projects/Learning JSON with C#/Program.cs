using System.Text.Json;
using System.IO;

namespace Learning_JSON_with_C_;

class Program
{
    static readonly string jsonFilePath = @"..\..\..\contacts.json";
    static List<Person> people = new List<Person>();
    static bool ProgramRunning = true;
    static void Main()
    {
        while(ProgramRunning)
        {   
            // we check to see if the json file exists, if so load the data
            if(File.Exists(jsonFilePath))
            {
                LoadContacts();
            }

            Console.Clear();
            Console.WriteLine("1: Create New Contact");
            Console.WriteLine("2: View All Contacts");
            Console.WriteLine("3: Exit");
            Console.Write("Input: ");
            string? input = Console.ReadLine();

            // just logic for adding new contacts
            if(input == "1")
            {
                Console.Clear();
                Person newContact = new Person();
                Console.WriteLine("Please Enter a Name For The Contact");
                Console.Write("Input: ");
                string? nameInput = Console.ReadLine();
                Console.WriteLine("Please Enter a Age For The Contact");
                Console.Write("Input: ");
                string? ageInputString = Console.ReadLine();
                int ageInput = int.TryParse(ageInputString, out int age) ? age : 0;
                Console.WriteLine("Please Enter a Email For The Contact");
                Console.Write("Input: ");
                string? emailInput = Console.ReadLine();

                newContact.AddInformation(nameInput ?? "", ageInput, emailInput ?? "");
                people.Add(newContact);
                SaveContacts();

                Console.WriteLine("Contact Added Successfully. Press Any Button To Continue");
                Console.ReadKey();
            }
            else if(input == "2")
            {
                Console.Clear();
                int index = 0;
                foreach (Person contact in people)
                {
                    Console.WriteLine(index + 1 + ": " + contact.Name + ", " + contact.Age + ", " + contact.Email);
                    index++;
                }
                Console.WriteLine("Press Any Key To Continue");
                Console.ReadKey();
            }
            else if(input == "3")
            {
                ProgramRunning = false;
            }
        }
    }

    static void SaveContacts()
    {   
        // here we do a try and see if we can save the data.
        // in the try we are declaring the string that will be what is written
        // we then take that string and write it to the JSON path.
        try
        {
            string jsonString = JsonSerializer.Serialize(people, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(jsonFilePath, jsonString);
            Console.WriteLine($"Contacts Saved To {jsonFilePath}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error Saving Contacts: {ex.Message}");
        }
    }

    static void LoadContacts()
    {
        // in this we do a check to see if the file exists.
        // if so then we create a var that allows us to deserialize the list
        // we then throw the loadedPeople variable into the people list and then we're good to go.
        try
        {
            if(File.Exists(jsonFilePath))
            {
                string jsonString = File.ReadAllText(jsonFilePath);
                var loadedPeople = JsonSerializer.Deserialize<List<Person>>(jsonString);
                if(loadedPeople != null)
                {
                    people = loadedPeople;
                    Console.WriteLine($"Loaded {people.Count} Contacts");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Loading Contacts: {ex.Message}");
        }
    }
}

public class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;

    public void AddInformation(string name, int age, string email)
    {
        Name = name;
        Age = age;
        Email = email;
    }
}