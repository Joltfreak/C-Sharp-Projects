

namespace Dungeon_Crawler_Project;

class Program
{
    static bool ProgramRunning = true;
    static bool GameStarted = false;
    static void Main()
    {
        while(ProgramRunning)
        {
            // we start at the main menu
            ShowMainMenu();
        }
    }

    static void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Dungeon Crawler Project Version 1.0 \n");
        Console.WriteLine("1: Start Game");
        Console.WriteLine("2: Exit Game\n");
        Console.Write("Input: ");
        string? UserInput = Console.ReadLine();
        if (int.TryParse(UserInput, out int selection))
        {
            if(selection == 1)
            {
                StartGame();
            }
            else if(selection == 2)
            {
                ProgramRunning = false;
            }
        }
    }

    static void StartGame()
    {
        GameStarted = true;
        Console.Clear(); // we clear the menu for the upcoming text
        // the game will open up to a character creation
        Player player = CreateCharacter();
    }

    static Player CreateCharacter()
    {
        // this will be the function to make a character
        // these currently don't do anything as they don't have any stats like Mana, Dex or anything like that
        Console.WriteLine("Choose a Class:");
        Console.WriteLine("1: Warrior");
        Console.WriteLine("Warrior - Good Defence, High Health, Lower Attack, Low Mana\n");
        Console.WriteLine("2: Rogue");
        Console.WriteLine("Rogue - Medium Defence, Medium Health, High Attack, Medium Mana\n");
        Console.WriteLine("3: Mage");
        Console.WriteLine("Warrior - Lowest Defence, Lowest Health, High Damage, High Mana\n");
        Console.Write("Input: ");
        string? UserInput = Console.ReadLine();
        Player player;
        if(int.TryParse(UserInput, out int selection))
        {
            if(selection == 1)
            {
                player = new Player(150, 1, 0, 15, 10, 5);
                return player;
            }
            else if(selection == 2)
            {
                player = new Player(100, 1, 0, 25, 7, 10);
                return player;
            }
            else if(selection == 3)
            {
                player = new Player(75, 1, 0, 20, 5, 20);
                return player;
            }
        }
        // If input is invalid or not 1, 2, or 3, repeat character creation
        Console.Clear();
        Console.WriteLine("Invalid selection. Please try again.");
        return CreateCharacter();
    }

    // this project will be using Classes, turn based logic and more.
    // for the beginning I want to start off by making it Console Based so just text.
    // the idea is that we will have a dungeon crawling game where you get to choose a class that has stats
    // then go through the dungeon and fight enemies and hopefully reach the final enemy and destroy them.
    // as the player plays through the dungeon crawler they will get XP and loot so I need a loot table with
    // different loot drops and chances.
}

class Player
{
    public int Health { get; set;}
    public int Level { get; set;}
    public int XP { get; set;}
    public int Damage { get; set;}
    public int ArmorClass { get; set;}
    public int Mana { get; set;}

    public Player(int health, int level, int xp, int damage, int armorclass, int mana)
    {
        Health = health;
        Level = level;
        XP = xp;
        Damage = damage;
        ArmorClass = armorclass;
        Mana = mana;
    }
}

class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int ArmorClass { get; set; }
    public int XPReward { get; set; }

    public Enemy(string name, int health, int damage, int armorClass, int xpReward)
    {
        Name = name;
        Health = health;
        Damage = damage;
        ArmorClass = armorClass;
        XPReward = xpReward;
    }
}
