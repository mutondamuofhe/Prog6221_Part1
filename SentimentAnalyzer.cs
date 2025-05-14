using System.Collections.Generic;

namespace Prog6221_Part1
{
    public class SentimentAnalyzer
    {
        // Dictionary to map user sentiment keywords to appropriate bot responses
        private readonly Dictionary<string, string> sentimentResponses;

        // Constructor initializes the sentiment-response dictionary
        public SentimentAnalyzer()
        {
            sentimentResponses = new Dictionary<string, string>()
            {
                // Key-value pairs mapping sentiment keywords to empathetic or informative responses
                {"worried", "It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe."},
                {"curious", "I'm glad you're curious! Learning more about cybersecurity is the first step to staying safe."},
                {"frustrated", "I know cybersecurity can be overwhelming at times. You're not alone, and I'm here to help however I can."},
                {"happy", "That's great to hear! Staying positive is a great mindset, and learning about cybersecurity can be empowering."},
                {"anxious", "It’s okay to feel anxious—online threats can be scary. Let's walk through ways to protect yourself one step at a time."},
                {"confused", "No problem! Cybersecurity can be complex. I'm here to explain things simply—just ask."},
                {"angry", "It’s frustrating when things go wrong online. Let’s see what we can do to prevent issues in the future."}
            };
        }

        // Method to analyze user input and return a sentiment-based response if a match is found
        public bool TryGetSentimentResponse(string userInput, out string response)
        {
            // Iterate through each known sentiment keyword
            foreach (var sentiment in sentimentResponses.Keys)
            {
                // If the user's input contains a sentiment keyword, return the corresponding response
                if (userInput.Contains(sentiment))
                {
                    response = sentimentResponses[sentiment];
                    return true;
                }
            }

            // If no sentiment is matched, set response to null and return false
            response = null;
            return false;
        }
    }
}
