using System.Diagnostics;

namespace Dungeon_Crawler_Project;

class Program
{
    static List<Enemy> normalEnemyPool = new List<Enemy>
    {
        new Enemy("Goblin", 36, 5, 6, 9, 30),
        new Enemy("Skeleton", 24, 4, 5, 7, 20),
        new Enemy("Rat", 15, 2, 4, 6, 10),
    };
    static List<Enemy> eliteEnemyPool = new List<Enemy>
    {
        new Enemy("Orc Warrior", 60, 6, 8, 11, 80),
        new Enemy("Troll", 80, 8, 10, 13, 100)
    };
    static bool ProgramRunning = true;
    static bool GameStarted = false;
    static int NumberOfRooms;
    static int CurrentRoomIndex;
    static Player? player;
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
        Random random = new Random();
        NumberOfRooms = random.Next(3, 5);
        GameStarted = true;
        Console.Clear();
        player = CreateCharacter();
        if(GameStarted == true)
        {
            StartGameLoop();
        }
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
        Console.WriteLine("Mage - Lowest Defence, Lowest Health, High Damage, High Mana\n");
        Console.Write("Input: ");
        string? UserInput = Console.ReadLine();
        if(int.TryParse(UserInput, out int selection))
        {
            if(selection == 1)
            {
                player = new Player(150, 1, 0, 4, 6, 10, 5);
                return player;
            }
            else if(selection == 2)
            {
                player = new Player(100, 1, 0, 6, 8, 7, 10);
                return player;
            }
            else if(selection == 3)
            {
                player = new Player(75, 1, 0, 4, 8, 5, 20);
                return player;
            }
        }
        // If input is invalid or not 1, 2, or 3, repeat character creation
        Console.Clear();
        Console.WriteLine("Invalid selection. Please try again.");
        return CreateCharacter();
    }

    static void StartGameLoop()
    {
        Room currentRoom;
        while(GameStarted)
        {
            Console.Clear();
            Console.WriteLine("As you enter you see 3 wooden doors presented to you.\n");
            Console.WriteLine("Select the door you wish to take.");
            Console.Write("Input: ");
            string? playerInput = Console.ReadLine();
            if(int.TryParse(playerInput, out int selection))
            {
                if(selection < 1 || selection > 3)
                {
                    Console.Clear();
                    Console.WriteLine("That is an invalid choice, choose again");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if(NumberOfRooms != CurrentRoomIndex)
                    {
                        CurrentRoomIndex++;
                        Console.Clear();
                        // we generate a room
                        currentRoom = GenerateRoom(false);
                        Console.WriteLine("You enter the room and see..." + currentRoom.Description);
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        HandleRoomInteraction(currentRoom);
                    }
                    else if(NumberOfRooms == CurrentRoomIndex)
                    {
                        Console.Clear();
                        // boss room
                        currentRoom = GenerateRoom(true);
                        Console.WriteLine("You enter the room and see..." + currentRoom.Description);
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        HandleRoomInteraction(currentRoom);
                    }
                }
            }   
        }
    }

    static void HandleRoomInteraction(Room currentRoom)
    {
        Enemy? currentEnemy = null;
        if(currentRoom.Type == RoomType.Normal)
        {
            currentEnemy = SelectRandomEnemy(normalEnemyPool);
            StartCombat(currentEnemy);
        }
        else if(currentRoom.Type == RoomType.Elite)
        {
            currentEnemy = SelectRandomEnemy(eliteEnemyPool);
            StartCombat(currentEnemy);
        }
        else if(currentRoom.Type == RoomType.Loot)
        {
            
        }
        else if(currentRoom.Type == RoomType.Boss)
        {
            
        }
    }

    static Enemy SelectRandomEnemy(List<Enemy> enemyPool)
    {
        Random random = new Random();
        int randomIndex = random.Next(0, enemyPool.Count);
        return enemyPool[randomIndex];
    }

    static void StartCombat(Enemy currentEnemy)
    {
        bool PlayerDead = false;
        bool EnemyDead = false;
        Console.Clear();
        Console.WriteLine($"You see a {currentEnemy.Name}");
        Console.WriteLine("You have engaged in combat!\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        while(PlayerDead == false && EnemyDead == false)
        {
            Console.Clear();
            Console.WriteLine("Select what you would like to do.");
            Console.WriteLine("1: Attack");
            // will need to add other options later. This is for prototype however
            Console.WriteLine($"\n Player Health: {player.Health}" + "   " + $"Enemy Health: {currentEnemy.Health}");
            string? PlayersInput = Console.ReadLine();
            
            // first the player attacks
            if(int.TryParse(PlayersInput, out int selection) && selection == 1)
            {
                Console.Clear();
                int currentRoll = RollDiceToHit(21);
                // we attack enemy
                Console.WriteLine($"You rolled: {currentRoll}");
                if(currentRoll >= currentEnemy.ArmorClass)
                {
                    int damage = RollDiceForDamage(player.MinDamage, player.MaxDamage);
                    Console.WriteLine("You hit the enemy!");
                    Console.WriteLine($"The enemy takes: {damage} points damage!");
                    currentEnemy.Health -= damage;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    if(currentEnemy.Health <= 0)
                    {
                        EnemyDead = true;
                        Console.WriteLine($"You have slayed the enemy {currentEnemy.Name}");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        return;
                    }
                }
                else if(currentRoll <= currentEnemy.ArmorClass)
                {
                    Console.WriteLine("You attempted to hit the enemy but they dodged the attack...");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }

            // then the enemy attacks
            int enemyDiceRoll = RollDiceToHit(21);
            Console.WriteLine($"The enemy {currentEnemy.Name} attacks...");
            if(enemyDiceRoll >= player.ArmorClass)
            {
                int damage = RollDiceForDamage(currentEnemy.MinDamage, currentEnemy.MaxDamage);
                Console.WriteLine($"The {currentEnemy.Name} hits you for {damage}");
                // deal damage to player
                player.Health -= damage;
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                if(player.Health <= 0)
                {
                    PlayerDead = true;
                    Console.WriteLine("You have Died");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Enemy {currentEnemy.Name}'s attack has missed!");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }

    static int RollDiceToHit(int MaxRoll)
    {
        Random Dice = new Random();
        int attackRoll = Dice.Next(1, MaxRoll);
        return attackRoll;
    }

    static int RollDiceForDamage(int MinDamage, int MaxDamge)
    {
        Random Dice = new Random();
        int attackDamage = Dice.Next(MinDamage, MaxDamge + 1);
        return attackDamage;
    }

    static Room GenerateRoom(bool IsBossRoom)
    {
        Random RandomNum = new Random();
        int roomIndex = RandomNum.Next(0, 3);

        if(IsBossRoom)
        {
            Room bossRoom = new Room(RoomType.Boss, "a towering monster, larger and stronger than the others");
            return bossRoom;
        }
        else if(roomIndex == 0) // normal enemy room
        {
            Room normalRoom = new Room(RoomType.Normal, "an enemy sitting in the center of the room");
            return normalRoom;
        }
        else if(roomIndex == 1) // loot room
        {
            Room lootRoom = new Room(RoomType.Loot, "something that resembles a chest");
            return lootRoom;
        }
        else if(roomIndex == 2) // elite room
        {
            Room eliteRoom = new Room(RoomType.Elite, "a strong foe, stronger than the other ones you've faced");
            return eliteRoom;
        }
        
        return GenerateRoom(IsBossRoom);
    }

    static void GenerateASCIIArt(Enemy currentEnemy)
    {
        if(currentEnemy.Name == "Troll")
        {
            string TrollASCII = @"
                                                        ....:.                                                  
                                          .~...  ~:                                                 
                                         :~:      ^~                                                
                                         ^..       ^^                                               
                                        ::....^.. ..!^.                                             
                                      ^^^.~7Y!^~7!^^~^                                              
                                       ^!..... .   :::.                                             
                                      .~.7..    .  ..:?                                             
                                     ^~7.~^..:... .^.^?!^..::                                       
                                   .^..!^.         . :?~ ..:^....                                   
                                   :! .^7^.   ....  :?: ..:. :  .^^.                                
                               .~...::..~77:.7!^^~.^!~.. .:... .::.^:^^                             
                              .:~:....::..:::^.  .:.     .    .:..  :!^~.                           
                              !:.:   ...            .       . :::^: .: .:...                        
                            :!::..:. ..   .   .              .^:~~.       .^                        
                         ^::^^..  ^.             .           ..^77:.       7:                       
                        :J: .. . .^. .        .                .7GJ~.     .^^                       
                      ::7. ....^!?^.          ..         ...:^^~!7?^:...  .:^                       
                      ~:^ . ^:.^5J:  ........              ..  . .^~.  ..  . ^.                     
                       ?^... :^75J7^^:.                           .^:   .   .!                      
                      .!^.:.^:~!J7:                               ..~~.      ~                      
                     :......:^^^J^           .                    .::5!:.    ~.                     
                     ~.   ..:::^J::.                         .. .:..!5!:.    ^!                     
                     ...  .::..!!^~:...                          ::.^?!::   ..^^...                 
                    .      ^. :~~^:...                              .~?::^:  . .^!.                 
                   ..    ....:?~::   .                             ...?Y:~^. .. .~~.                
                .:~!    .:::.7Y::                                  :. ^P:^^.. ...:.~^..             
               .~::^^...:.^:7#!                                    .:..?^.:.:  .  ...: .~           
             .:^^.  ...:.::~5Y.    .                               ...:7!:~.      ...^^77           
             :!J...     ..:!J7.                                    .:::!~.:.      ...:~:            
              ^:^         ^:7J....                                 .^~^J!. :.        :7.            
              !~  .    ....~5?^:. . ..                            .:~~!?!^:..        ^:^^.          
              7?        ..  ~P?^.......             :            .^!7~!J7~.   .       . ::          
     .?..:.. .!.         ...:YY?~... ... .                      .:~?!~^5?:            . .7:         
     ^5~:.:^^?.            ..?~77~^. .......                  ..:^~!~!Y5J:.    ..      .:.:~        
      .:!J^:~:             .:J^?:.^!^::. ...                .....:~~JP?55 ::. .:..      .:~^        
         ^77J.              :!?7.. .~!^^^:......            ....^7!7?YJ?Y~:..~?:....      ~:        
           ~G~      .   ..  :^~?!.   ^.::~!^^::::::........:..:~!^7?!JY~!Y!.:!!.::^^....   ~.       
           ^~!... .. ^..!^~~!!7?Y7.      .~J!^::::.:....:...^::!7~J~!J~:^~J~:~~   :J!^...!Y!        
           ?~??...:^^^  ?Y^^..:^!!?^      .^:....... ....   .:.?~:~^J7.  .^?~...^:.7!...~J?         
           ^7!7  .7:.:. ~5^^. . ...::::.    .                ..!.^.?~..   .:7^:^J7^..:.!7~.         
            .~?!..~?   .75^^.~...      .::....                .... ?.  .   .:?7!:. ..:^!            
               :!:~?~...^YJY^^:~             .:.              . : :!        .^!:::::                
                 .:..:!~?7!~:!~7..             .:.              . ~^         :7                     
                     ^^:^....^^^~^..             ^^:.....       ..J:       ..~?.                    
                     ~.~^    .~...:^::.               ....::.:::^JY:        ^!^!                    
                     ^.~~     ^^:   .:!.                         .^~^...    ..:!:                   
                     :^~^      :7.    .::~.                         ...::.  ...^~                   
                     .7^^       .!  ..    ^:.                            ^..::::?                   
                     .?~^..    . ^~...:^^  ...~:                            .~^~J^..                
                     ^..!~^....   ~::5Y.^.:....~::                            ...    ::.            
                     !  :.  ..    :!J5.  .  .::.^J~                                .~!!~            
                    ::..:.        .~?!         ...~!...                            ^?               
                   .:  ~!.     .. :7!           .  J: .                          . .7^              
                   ~:.  .     .:::?^               .~::~7^::...                  . ..:!             
                  ^ ^.  .:....   :?                  ...:::~~:. :.                  :.7             
                 ::          ..  .~.                        .^^~ ..    ..         :^~:^~            
                 ::          .^..:7:                           ~7~:.    .     .. .~Y!..             
                 ^~.          .:~~:                              .^7:   ...  .:777??^               
                  ~^.        . ^7.                                :?^^:.  .::.:!7~!J!               
                  .7         ::7:                                  ...:^~~~~77~7^..5~               
                  ~:         !7.                                     .:.:.  ..:^: ^J:               
                  ^ ..    .::!.                                       .^7:    . .:!!?               
                 ....      7!^                                          ?..      .^:7.  .           
                :.         .^~                                          ~^^. ..    :7. .            
                . .          ::                                        .:..        . ~.             
               ^  ..          ^:                                     ..!         ... .~             
              :Y!..   :  ..   .^!:::......                       ...:^Y^   .~:   :  ^ :~..          
              .~!~!:.~:. .57 ...?!^::::...                        ...:J?.:.:JY^::!~:~~^^.           
                  ...^::^^~~::^^^:.....                               .^::.. .^^::..                ";
            Console.WriteLine(TrollASCII);
        }
    }
}

class Player
{
    public int Health { get; set;}
    public int Level { get; set;}
    public int XP { get; set;}
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }
    public int ArmorClass { get; set;}
    public int Mana { get; set;}

    public Player(int health, int level, int xp, int minDamage, int maxDamage, int armorclass, int mana)
    {
        Health = health;
        Level = level;
        XP = xp;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        ArmorClass = armorclass;
        Mana = mana;
    }
}

class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }
    public int ArmorClass { get; set; }
    public int XPReward { get; set; }

    public Enemy(string name, int health, int minDamage, int maxDamage, int armorClass, int xpReward)
    {
        Name = name;
        Health = health;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        ArmorClass = armorClass;
        XPReward = xpReward;
    }
}

enum RoomType
{
    Normal,
    Loot,
    Boss,
    Elite
}

class Room
{
    public RoomType Type { get; set; }
    public string? Description { get; set; }

    public Room(RoomType type, string description)
    {
        Type = type;
        Description = description;
    }
}