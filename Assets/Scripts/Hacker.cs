using UnityEngine;

public class Hacker : MonoBehaviour
{
    // The current level
    public int level;

    public static string[] libraryPasswords = new string[] { "password", "book", "shelf", "aisle", "borrow", "cover" };
    public static string[] policePasswords = new string[] { "uniform", "prisoner", "arrest", "handcuffs", "cell", "9-1-1" };
    public static string[] nasaPasswords = new string[] { "telescope", "starfield", "meteor", "emPloyEe", "americA", "superdupersecretpassword" };
    public static string[] moonPasswords = new string[] { "crater", "space_station", "themoonischeese", "grey", "notaplanet" };
    public static string[] universePasswords = new string[] { "star", "galaxy", "planets", "moons", "blackholes", "verylarge" };

    public float startHackDelay = 0.5f;
    public float askForPasswordDelay = 1f;
    public float giveHintDelay = 4f;

    public float restartDelay = 1f;
    public float winScreenDelay = 5f;

    public string[] levelNames = {
        "Your_Local_Library",
        "The_Police_Station",
        "NASA",
        "The_Moon",
        "The_Universe"
    };

    public int libraryIndex = 0;
    public int policeIndex = 0;
    public int nasaIndex = 0;
    public int moonIndex = 0;
    public int universeIndex = 0;

    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    // Displays the main menu for the game
    void ShowMainMenu ()
    {
        currentScreen = Screen.MainMenu;
        level = 0;

        Terminal.ClearScreen();
        Terminal.WriteLine("Welcome to Terminal_Hacker");
        Terminal.WriteLine("Choose where you want to hack into:");
        Terminal.WriteLine("");
        Terminal.WriteLine("[1] Your Local Library");
        Terminal.WriteLine("[2] The Police Station");
        Terminal.WriteLine("[3] NASA");
        Terminal.WriteLine("[4] The Moon");
        Terminal.WriteLine("[5] The Universe");
        Terminal.WriteLine("");
        Terminal.WriteLine("Type your selction and hit enter: ");

        SendMenuHint();
    }

    // Fires on user input from the terminal once the user hits enter
    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
            print("Menu input");
        }

        bool validLevelSelection = (input == "1" || input == "2" || input == "3" || input == "4" || input == "5");

        if (currentScreen == Screen.Password)
        {
            HandlePasswordGuessing(input);
        }
        else if (validLevelSelection && currentScreen == Screen.MainMenu)
        {
            level = int.Parse(input);
            print("Level input");

            if (currentScreen == Screen.MainMenu)
            {
                StartGame();
            }
        }
        else if(!validLevelSelection && currentScreen == Screen.MainMenu)
        {
            Terminal.WriteLine("Please enter a valid level number:");
        }
    }

    // Start the game with a specific level
    void StartGame()
    {
        libraryIndex = UnityEngine.Random.Range(0, libraryPasswords.Length);
        policeIndex = UnityEngine.Random.Range(0, policePasswords.Length);
        nasaIndex = UnityEngine.Random.Range(0, nasaPasswords.Length);
        moonIndex = UnityEngine.Random.Range(0, moonPasswords.Length);
        universeIndex = UnityEngine.Random.Range(0, universePasswords.Length);

        currentScreen = Screen.Password;

        Terminal.ClearScreen();
        
        Invoke("StartHack", startHackDelay);
        
        Invoke("AskForPassword", askForPasswordDelay);

        Invoke("GiveHint", giveHintDelay);
    }

    // Handle the guessing for the password
    void HandlePasswordGuessing(string input)
    {
        switch (level)
        {
            case 1:
                if (libraryPasswords[libraryIndex] == input)
                    WinGame();
                else
                    HandleWrongGuess();
                break;
            case 2:
                if (policePasswords[policeIndex] == input)
                    WinGame();
                else
                    HandleWrongGuess();
                break;
            case 3:
                if (nasaPasswords[nasaIndex] == input)
                    WinGame();
                else
                    HandleWrongGuess();
                break;
            case 4:
                if (moonPasswords[moonIndex] == input)
                    WinGame();
                else
                    HandleWrongGuess();
                break;
            case 5:
                if (universePasswords[universeIndex] == input)
                    WinGame();
                else
                    HandleWrongGuess();
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    // Handle wrong password
    void HandleWrongGuess()
    {
        Terminal.WriteLine("Wrong answer! Try again");
    }

    // Show the winning screen
    void ShowWinScreen()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Well done! You successfully hacked: ");
        Terminal.WriteLine(levelNames[level - 1]);
    }

    // Handle winning
    void WinGame()
    {
        currentScreen = Screen.Win;
        ShowWinScreen();
        Invoke("ShowMainMenu", winScreenDelay);
    }

    /* <!---------------------------------- Helper methods ----------------------------------!> */

    // Give a password hint
    void GiveHint()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Heres a hint: " + libraryPasswords[libraryIndex].Anagram());
                break;

            case 2:
                Terminal.WriteLine("Heres a hint: " + policePasswords[policeIndex].Anagram());
                break;

            case 3:
                Terminal.WriteLine("Heres a hint: " + nasaPasswords[nasaIndex].Anagram());
                break;
            case 4:
                Terminal.WriteLine("Heres a hint: " + moonPasswords[moonIndex].Anagram());
                break;
            case 5:
                Terminal.WriteLine("Heres a hint: " + universePasswords[universeIndex].Anagram());
                break;
        }
    }

    // Send the start hack message
    void StartHack()
    {
        Terminal.WriteLine("Starting to hack " + levelNames[level - 1] + "...");
    }

    // Send a message to ask for password
    void AskForPassword()
    {
        Terminal.WriteLine("Please enter the password: ");
    }

    // Send menu hint
    void SendMenuHint ()
    {
        Terminal.WriteLine("Type 'menu' to return the menu");
    }
}
