using System;
using System.Threading; // Namespace to handle thread sleeping for the typing effect

namespace Prog6221_Part1
{
    // The ResponseSystem class is responsible for handling the user input and providing appropriate responses
    public class ResponseSystem
    {
        // 2D Array to store question-answer pairs
        private string[,] qaArray = new string[,]
        {
            { "how do i create a strong password?", "Strong passwords should be at least 12 characters long, include a mix of letters, numbers, and symbols, and avoid common words or personal info." },
            { "how can i recognize a phishing email?", "Look for suspicious email addresses, generic greetings, urgent language, and unexpected attachments or links." },
            { "what are the best practices for safe browsing?", "Always keep your browser updated, avoid suspicious websites, and be cautious when downloading files or clicking links." },
            { "what should i do if my account gets hacked?", "Change your password immediately, enable two-factor authentication, and report the incident to the service provider." },
            { "how can i safely shop online?", "Use reputable websites, check for HTTPS in the URL, and never save your card details on public or shared computers." },
            { "what is two-factor authentication?", "Two-factor authentication adds an extra security step when logging in, such as a code sent to your phone." },
            { "how do i protect my wifi network?", "Change your router's default password, enable encryption (WPA2/WPA3), and hide your network's SSID." },
            { "how can i avoid online scams?", "Verify sellers, avoid deals that seem too good to be true, and never share personal details unless necessary." },
            { "is public wifi safe to use?", "Public WiFi is risky—avoid logging into sensitive accounts and use a VPN if possible." },
            { "what should i do if i get a suspicious message?", "Do not click on links or download attachments from unknown sources. Report and block suspicious messages." },
            { "how are you?", "I'm doing well, thank you for asking. I'm a large language model, so I don't have feelings or emotions like humans do, but I'm always happy to chat and assist with any questions or topics you'd like to discuss. How about you? How can I assist you today?" },
            { "what's your purpose?", "My purpose is to help you understand cybersecurity and guide you through how to stay safe online. Feel free to ask me anything about that!" },
            { "what can i ask you about?", "You can ask me anything related to online safety. Topics like password safety, phishing, safe browsing, and much more are covered!" }
        };

        // Function to simulate typing effect for a message
        private void TypingEffect(string message)
        {
            foreach (char c in message)
            {
                // Print each character with a small delay to simulate typing
                Console.Write(c);
                Thread.Sleep(50); 
            }
            Console.WriteLine(); // Move to the next line after the message
        }

        // Function to calculate similarity score between user input and predefined questions
        // It uses a basic word-matching approach to evaluate how closely the input matches any of the questions
        private int GetSimilarityScore(string input, string question)
        {
            string[] inputWords = input.Split(' '); // Split the input into words
            string[] questionWords = question.Split(' '); // Split the question into words

            int matchCount = 0;
            // Compare each word in the input with each word in the question
            foreach (string word in inputWords)
            {
                foreach (string qWord in questionWords)
                {
                    if (word == qWord) // If a word matches, increment the match count
                    {
                        matchCount++;
                    }
                }
            }

            // Return a percentage score based on the number of matching words relative to the total words in the question
            return (matchCount * 100) / questionWords.Length;
        }

        // Function to handle user input and provide a relevant response
        public void HandleUserInput(string input, string userName)
        {
            // If the input is empty or just whitespace, ask the user to enter something valid
            if (string.IsNullOrWhiteSpace(input))
            {
                TypingEffect("I didn't quite catch that. Could you please type something?");
                return;
            }

            input = input.ToLower(); // Convert input to lowercase for easier matching
            int bestMatchIndex = -1; // Index to store the best matching question
            int highestScore = 0; // Highest similarity score

            // Loop through all question-answer pairs to find the best match for the input
            for (int i = 0; i < qaArray.GetLength(0); i++)
            {
                int score = GetSimilarityScore(input, qaArray[i, 0]); // Calculate similarity score for each question

                if (score > highestScore) // If the current score is higher than the previous highest, update
                {
                    highestScore = score;
                    bestMatchIndex = i;
                }
            }

            // If the highest score is above a certain threshold (50% match), respond with the corresponding answer
            if (highestScore > 50)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                TypingEffect($"{userName}, {qaArray[bestMatchIndex, 1]}"); // Display the response
                Console.ResetColor(); // Reset console color after displaying the response
            }
            else // If no good match is found, ask the user to clarify their question
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypingEffect("I didn't quite catch that. Could you please clarify your question?");
                Console.ResetColor();
            }
        }
    }
}
