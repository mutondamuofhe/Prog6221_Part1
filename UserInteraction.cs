using System;

namespace Prog6221_Part1
{
    public class UserInteraction
    {
        private string userName;

        // Constructor to greet the user and ask for their name
        public UserInteraction()
        
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║    WELCOME TO THE CYBERSECURITY AWARENESS BOT      ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.WriteLine("║     I'm here to help you stay safe online!         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            
           //colour of the text
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Good day! I'm your Cybersecurity Awareness Assistant.");
            Console.Write("May I know your name to personalize our interaction? ");

            // Validate user input
            userName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.Write("Please enter a valid name: ");
                userName = Console.ReadLine();
            }

            // Display personalized welcome message
            Console.WriteLine($"\nHello, {userName}. I'm here to guide you through the essentials of cybersecurity.");
            Console.WriteLine("\nYou can ask me cybersecurity questions(type 'exit' to quit). Try asking:");
        }

        // Function to return the user's name
        public string GetUserName()
        {
            return userName;
        }
    }
}


