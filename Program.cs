using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // Namespace for working with images and graphics (not used directly here but possibly required elsewhere in the project)

namespace Prog6221_Part1
{
    public class Program
    {
        //  Main entry point for the application
        static void Main(string[] args)
        {
            // Create an instance of the play_sound class to handle background or notification sounds
            // The constructor likely handles sound playback automatically, so no need to store the reference
            new play_sound() { };

            //  Create an instance of the Logo class to render the ASCII logo in the console
            // This adds visual branding or identification when the app starts
            new Logo() { };

            //  Instantiate KeywordResponder to process and react to user input based on specific keywords
            // Presumably initializes keyword-response mappings or starts listening for input
            new KeywordResponder();

            //  Instantiate MemoryManager to load/save user preferences or past conversation history
            // Enables persistence of user name, favorite topic, and chat logs
            new MemoryManager();

            //  Instantiate SentimentAnalyzer to interpret user emotions from input and tailor responses
            // Supports empathetic and emotionally-aware communication
            new SentimentAnalyzer();

        } // end of Main method
    } // end of Program class
} // end of namespace

