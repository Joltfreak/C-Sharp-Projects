

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
        Console.WriteLine("Welcome to the Dungeon Crawler Project Version 1.0 \n");
        Console.WriteLine("1: Start Game");
        Console.WriteLine("2: Exit Game");
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
