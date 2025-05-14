using System;
using System.Collections.Generic;
using Prog6221_Part1;

namespace Prog6221_Part1
{
    public class KeywordResponder
    {
        // Stores the user's name
        private string userName;

        // Stores the user's favorite cybersecurity topic
        private string favoriteTopic;

        // Manages loading and saving user data
        private MemoryManager memory;

        // Analyzes user input to detect sentiment and respond accordingly
        private SentimentAnalyzer sentimentAnalyzer;

        // Stores predefined responses for specific cybersecurity-related keywords
        private Dictionary<string, List<string>> keywordResponses;

        // Used for selecting random responses from keyword lists
        private Random random;

        // Constructor for KeywordResponder - this initializes the chatbot logic and handles user interaction
        public KeywordResponder()
        {
            // Initialize supporting classes and keyword responses
            memory = new MemoryManager();
            sentimentAnalyzer = new SentimentAnalyzer();
            keywordResponses = InitializeKeywordResponses();
            random = new Random();

            // Display the ASCII welcome banner in green color
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║    WELCOME TO THE CYBERSECURITY AWARENESS BOT      ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.WriteLine("║     I'm here to help you stay safe online!         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;

            // Load previous user name and topic from memory (if available)
            var (savedName, savedTopic) = memory.LoadMemory();

            // If the user's name was saved from a previous session, greet them by name
            if (!string.IsNullOrEmpty(savedName))
            {
                userName = savedName;
                Console.WriteLine($"Welcome back, {userName}!");
            }
            else
            {
                // Prompt new users to enter their name, ensuring it's valid
                Console.Write("Good day! What's your name? ");
                userName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(userName))
                {
                    Console.Write("Please enter a valid name: ");
                    userName = Console.ReadLine();
                }
            }

            // If a favorite topic was previously saved, recall and display it
            if (!string.IsNullOrEmpty(savedTopic))
            {
                favoriteTopic = savedTopic;
                Console.WriteLine($"\nI remember you're interested in {favoriteTopic}!");
            }

            // Save the user’s name and topic for future sessions
            memory.SaveMemory(userName, favoriteTopic);

            // Display initial greeting after loading/saving user info
            Console.WriteLine($"\nHello, {userName}! You can ask me anything (type 'exit' to quit).");

            // Infinite loop to continuously interact with the user
            while (true)
            {
                Console.Write("\nYou> ");
                string userInput = Console.ReadLine();

                // If user input is empty or whitespace, prompt again
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBot > I didn't catch that. Could you type something?");
                    Console.ResetColor();
                    continue;
                }

                // Normalize the input for easier processing
                userInput = userInput.ToLower().Trim();

                // Exit condition - break loop if user types "exit"
                if (userInput == "exit")
                {
                    // Farewell message with green formatting
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n+------------------------------------------------+");
                    Console.WriteLine("|           THANK YOU FOR USING THE BOT!         |");
                    Console.WriteLine("|               Stay Cyber Safe!                 |");
                    Console.WriteLine("+------------------------------------------------+");
                    Console.ResetColor();
                    break;
                }

                // Detect if user is setting or updating their favorite topic
                if (userInput.StartsWith("i'm interested in") || userInput.StartsWith("my favorite topic is"))
                {
                    int index = userInput.IndexOf("in") + 3;
                    if (index > 2 && index < userInput.Length)
                    {
                        favoriteTopic = userInput.Substring(index).Trim();
                        memory.SaveMemory(userName, favoriteTopic);

                        // Confirm topic update to the user
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nBot > Great! I'll remember that you're interested in {favoriteTopic}.\n");
                        Console.ResetColor();
                        continue;
                    }
                }

                // Handle input using sentiment analysis if matched
                if (sentimentAnalyzer.TryGetSentimentResponse(userInput, out string sentimentResponse))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nBot > {sentimentResponse}\n");
                    memory.AppendConversation(userInput, sentimentResponse); // Save the interaction
                    Console.ResetColor();
                    continue; // Skip remaining checks if a sentiment match was found
                }

                // Respond to user input if it matches a keyword
                if (TryGetKeywordResponse(userInput, out string keywordResponse))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nBot > {keywordResponse}");

                    // If the response relates to the user's favorite topic, add a personalized message
                    if (!string.IsNullOrEmpty(favoriteTopic) && userInput.Contains(favoriteTopic.ToLower()))
                    {
                        Console.WriteLine($"By the way {userName}, since you're interested in {favoriteTopic}, this tip is especially useful!");
                    }

                    Console.WriteLine();
                    memory.AppendConversation(userInput, keywordResponse); // Save the interaction
                    Console.ResetColor();
                    continue;
                }

                // Fallback response for unrecognized input
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBot > I'm not sure I understand. Can you try rephrasing or ask about something cybersecurity-related?\n");
                Console.ResetColor();
            }
        }

        // Initializes a dictionary mapping cybersecurity-related keywords to lists of informative responses
        private Dictionary<string, List<string>> InitializeKeywordResponses()
        {
            return new Dictionary<string, List<string>>()
            {
                // Each keyword below includes a list of possible response messages to be selected randomly
                { "privacy", new List<string>{
        "Privacy is key to cybersecurity—avoid oversharing online.",
        "Adjust privacy settings on social media platforms.",
        "Encrypt personal data to protect your privacy.",
        "You can use privacy screens to keep your information safe.",
        "Be mindful of where and how you share personal information online to ensure privacy.",
        "Regularly review the privacy policies of apps and services.",
        "Avoid using public Wi-Fi for sensitive transactions without a VPN.",
        "Limit the amount of personal data stored online.",
        "Use a password manager to avoid reusing passwords it helps to keep your privacy.",
        "Understand and manage the privacy risks associated with location tracking."
                }},
                { "password", new List<string>{
        "Use complex passwords with letters, numbers, and symbols.",
        "Never reuse passwords across different accounts.",
        "Change your passwords regularly.",
        "Avoid using obvious passwords like '123456' or 'password'.",
        "Use a trusted password manager to store your passwords.",
        "Enable two-factor authentication (2FA) wherever possible.",
        "Do not share passwords with anyone, even trusted individuals.",
        "Create long, unique passwords for each online account.",
        "Always check the security of websites before entering your password.",
        "Use passphrases instead of single words for added security on your password."
                }},
                {     "phishing", new List<string>{
        "Be cautious of emails asking for personal information.",
        "Check the sender's email address carefully.",
        "Avoid clicking on suspicious links in emails or messages.",
        "Report phishing emails to your IT department immediately.",
        "Hover over links to see where they really lead before clicking.",
        "Look for signs of urgency or threats in phishing emails.",
        "Always verify requests for sensitive information by contacting the requester directly.",
        "Be wary of attachments in unsolicited emails.",
        "Keep your antivirus and anti-phishing filters up to date.",
        "Educate yourself and others about common phishing tactics."
                }},
                {  "malware", new List<string>{
        "Keep your antivirus software up to date.",
        "Don't download software from unknown sources.",
        "Regularly scan your devices for malware.",
        "Be cautious with email attachments even if they seem harmless.",
        "Use a firewall to help block malicious traffic.",
        "Avoid clicking on pop-ups or ads that seem suspicious.",
        "Backup your data regularly to minimize the impact of malware infections.",
        "Use browser extensions or settings to block malicious websites.",
        "Disable macros in documents from unknown sources.",
        "Stay informed about the latest malware threats and trends."
                }},
                { "firewall", new List<string>{
        "A firewall acts as a barrier between your device and threats.",
        "Keep your firewall enabled at all times.",
        "Configure your firewall rules carefully.",
        "Update your firewall software regularly.",
        "Use both hardware and software firewalls for stronger protection.",
        "Firewalls can block malicious incoming and outgoing traffic.",
        "Regularly review firewall logs to detect suspicious activity.",
        "Ensure your firewall is properly configured to protect all network ports.",
        "Use firewalls to segment internal networks and limit access to critical systems.",
        "Disable unused ports and services to reduce exposure to threats."
                }},
                {    "encryption", new List<string>{
        "Encryption protects your data from unauthorized access.",
        "Always encrypt sensitive files before sending them.",
        "Use strong encryption standards like AES-256.",
        "Encrypt your hard drive to protect your stored data.",
        "Never share your encryption keys carelessly.",
        "Ensure that your communications are encrypted when using public networks.",
        "Use end-to-end encryption for messaging and file transfers.",
        "Encrypt backups of critical data to protect them from theft.",
        "Always verify the encryption status of your data before storing it.",
        "Educate employees and users on the importance of encryption."
                }},
                {   "social engineering", new List<string>{
        "Always verify the identity of someone asking for sensitive information.",
        "Don't overshare personal information online.",
        "Question urgent requests for money or data.",
        "Educate yourself on common social engineering tactics.",
        "Be skeptical of unsolicited requests.",
        "Use multi-factor authentication to reduce the risk of social engineering.",
        "Avoid clicking on links in unsolicited emails or messages.",
        "Don't provide information based solely on an emotional appeal.",
        "Always confirm financial transactions via trusted channels.",
        "Report suspicious behavior to your IT department or security team."
                }},
                {  "authentication", new List<string>{
        "Use two-factor authentication (2FA) whenever possible.",
        "Don't share authentication codes with anyone.",
        "Use biometric authentication like fingerprint or facial recognition for extra security.",
        "Strong authentication reduces the risk of unauthorized access.",
        "Backup your authentication methods if possible.",
        "Ensure your authentication methods are up to date with security best practices.",
        "Consider using a password manager with integrated authentication features.",
        "Be aware of phishing attempts targeting your authentication credentials.",
        "Disable outdated or unused authentication methods.",
        "Ensure authentication credentials are encrypted both in transit and at rest."
                }},
                {  "data breach", new List<string>{
        "Change your passwords immediately after a breach.",
        "Monitor your accounts for suspicious activity.",
        "Enable notifications for account logins and changes.",
        "Use unique passwords to limit the damage from breaches.",
        "Stay informed about major data breaches.",
        "Check if your email or personal information was exposed using tools like Have I Been Pwned.",
        "Consider placing a credit freeze or fraud alert if your financial data is exposed.",
        "Notify your bank or credit card company if you suspect fraud due to a breach.",
        "Ensure your devices are secured with up-to-date antivirus software post-breach.",
        "Follow the guidance of relevant authorities when handling a breach."
                }},
                {     "vpn", new List<string>{
        "Use a VPN on public Wi-Fi to protect your data.",
        "Choose a reputable VPN provider with a no-logs policy.",
        "VPNs encrypt your internet traffic.",
        "Avoid free VPNs; they may not be secure.",
        "A VPN hides your IP address from websites.",
        "VPNs can bypass geographic restrictions, allowing access to global content.",
        "Always disconnect from a VPN when you're finished with sensitive activities.",
        "Use a VPN in combination with other security measures, such as firewalls.",
        "Ensure your VPN provider offers strong encryption standards.",
        "Understand that a VPN does not protect against all online threats."
                }},
                { "updates", new List<string>{
        "Always install software updates promptly.",
        "Updates often patch important security vulnerabilities.",
        "Enable automatic updates where possible.",
        "Update all devices, including your smartphone and router.",
        "Outdated software can be an easy target for hackers.",
        "Test updates in a controlled environment before deploying them in critical systems.",
        "Regularly check for firmware updates for all connected devices.",
        "Keep your operating system and applications updated to ensure optimal security.",
        "Enable security-focused updates, such as antivirus signature updates.",
        "Create a process for regularly auditing and managing updates within your organization."
                }}
            };
        }

        // Checks if the user's input contains any predefined keyword and selects a random associated response
        private bool TryGetKeywordResponse(string input, out string response)
        {
            foreach (var pair in keywordResponses)
            {
                if (input.Contains(pair.Key))
                {
                    response = pair.Value[random.Next(pair.Value.Count)];
                    return true;
                }
            }

            response = null; // Default if no keyword match is found
            return false;
        }

    }//end of class
}//end of namespace