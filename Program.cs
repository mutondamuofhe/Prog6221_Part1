using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // Namespace for working with images and graphics

namespace Prog6221_Part1
{
    public class Program
    {
        // Main entry point for the program
        static void Main(string[] args)
        {
            // Create an instance of the play_sound class to handle sound playback
            // No need for capturing the instance because it’s executed in the constructor
            new play_sound() { };

            // Create an instance of the Logo class to display the ASCII logo on the console
            new Logo() { };

            // Create instances of the UserInteraction and ResponseSystem classes
            // UserInteraction handles user input for getting their name and possibly other interactions
            UserInteraction userInteraction = new UserInteraction();

            // ResponseSystem handles answering the user’s questions based on predefined responses
            ResponseSystem responseSystem = new ResponseSystem();

            // Get the user’s name from the UserInteraction class
            string userName = userInteraction.GetUserName();

            // Infinite loop to handle continuous user interaction until 'exit' is entered
            while (true)
            {
                // Prompt the user for their input (question or command)
                Console.Write("You: ");
                // Read user input, trimming any leading/trailing spaces and converting to lowercase for easier matching
                string userInput = Console.ReadLine()?.Trim().ToLower();

                // Check if the user wants to exit the conversation
                if (userInput == "exit")
                {
                    // Display a thank you message with a yellow color to signify the end of the session
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nThank you for using the Cybersecurity Awareness Bot. Stay safe online!");
                    Console.ResetColor(); // Reset the color back to default
                    break; // Exit the loop and terminate the program
                }

                // If user doesn't type "exit", continue with providing a response
                Console.Write("Bot: ");
                // Pass the user's input and name to the ResponseSystem for processing and generating an answer
                responseSystem.HandleUserInput(userInput, userName);
            }
        }
    }
}
