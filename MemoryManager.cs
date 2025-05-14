using System;
using System.Collections.Generic;
using System.IO;

namespace Prog6221_Part1
{
    public class MemoryManager
    {
        // Path to the memory file where user data and conversation history are stored
        private readonly string memoryFilePath;

        // Constructor initializes the memory file path relative to the base directory
        public MemoryManager()
        {
            // Remove the "bin\\Debug\\" part to point to the project root directory
            string basePath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "");
            // Combine base path with file name to get the full path to memory.txt
            memoryFilePath = Path.Combine(basePath, "memory.txt");
        }

        // Method to save the user's name and favorite topic to memory.txt without deleting previous data
        public void SaveMemory(string userName, string favoriteTopic)
        {
            // List to store existing lines from the memory file
            List<string> existingLines = new List<string>();

            // Check if the memory file exists before reading it
            if (File.Exists(memoryFilePath))
            {
                // Read all lines from the file into the list
                existingLines.AddRange(File.ReadAllLines(memoryFilePath));
            }

            // Flags to check if the name or topic has already been updated
            bool nameUpdated = false;
            bool topicUpdated = false;

            // Iterate through the existing lines to update name or topic if they exist
            for (int i = 0; i < existingLines.Count; i++)
            {
                if (existingLines[i].StartsWith("Name:"))
                {
                    // Replace the line with the new name
                    existingLines[i] = $"Name:{userName}";
                    nameUpdated = true;
                }
                else if (existingLines[i].StartsWith("Topic:"))
                {
                    // Replace the line with the new topic
                    existingLines[i] = $"Topic:{favoriteTopic}";
                    topicUpdated = true;
                }
            }

            // If no previous name or topic was found, insert them at the top
            if (!nameUpdated)
                existingLines.Insert(0, $"Name:{userName}");
            if (!topicUpdated)
                existingLines.Insert(1, $"Topic:{favoriteTopic}");

            // Write all lines (updated or new) back to the memory file
            File.WriteAllLines(memoryFilePath, existingLines);
        }

        // Method to append a user question and bot response to the end of the memory file
        public void AppendConversation(string userInput, string botResponse)
        {
            // Use StreamWriter in append mode to add new lines to the existing file
            using (StreamWriter writer = new StreamWriter(memoryFilePath, true))
            {
                // Write the user input prefixed for clarity
                writer.WriteLine($":User  {userInput}");
                // Write the bot response
                writer.WriteLine($"Bot: {botResponse}");
                // Add a blank line for better visual separation between conversations
                writer.WriteLine();
            }
        }

        // Method to load the saved user name and favorite topic from the memory file
        public (string userName, string favoriteTopic) LoadMemory()
        {
            // Variables to hold the retrieved name and topic
            string userName = "";
            string favoriteTopic = "";

            // Check if the memory file exists before attempting to read from it
            if (File.Exists(memoryFilePath))
            {
                // Read all lines from the file
                var lines = File.ReadAllLines(memoryFilePath);

                // Loop through each line to find and extract name and topic
                foreach (var line in lines)
                {
                    if (line.StartsWith("Name:"))
                        userName = line.Substring("Name:".Length).Trim();

                    if (line.StartsWith("Topic:"))
                        favoriteTopic = line.Substring("Topic:".Length).Trim();
                }
            }

            // Return the retrieved values as a tuple
            return (userName, favoriteTopic);
        }
    }
}
