using System;
using System.Collections.Generic;

namespace CyberSecurity_Bot
{
    public class KeywordRecognition : ChatBotFeature, IResponseGenerator
    {
        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            {"password", new List<string>{
                "Using strong and unique passwords is crucial in protecting your online identity.",
                "A strong password should include a mix of letters, numbers, and symbols.",
                "Avoid using birthdays, names, or common words in your passwords."
            }},
            {"phishing", new List<string>{
                "Phishing is a common method used by attackers to steal sensitive information through deceptive emails or websites.",
                "Attackers may impersonate trusted organizations to trick you into revealing your credentials.",
                "Phishing links often look legitimate but redirect you to malicious sites."
            }},
            {"privacy", new List<string>{
                "Protecting your privacy online starts with limiting the personal information you share.",
                "Regularly updating your privacy settings on platforms can help safeguard your data.",
                "Two-factor authentication adds an extra layer of security to your private accounts."
            }},
            {"scam", new List<string>{
                  "It's completely understandable to feel that way. Scammers can be very convincing.",
                  "Online scams are increasing, and staying informed is your best defense.",
                  "Many scams are designed to look legitimate. It's important to stay cautious and verify sources."
}},
        };

        private Dictionary<string, string> keywordDetails = new Dictionary<string, string>()
        {
            {"password", "You can improve password security by using a password manager, enabling two-factor authentication, and never reusing passwords across different sites."},
            {"phishing", "To protect yourself from phishing, verify sender addresses, avoid clicking unknown links, and report suspicious emails to your IT department."},
            {"privacy", "Maintaining digital privacy also involves using secure networks, avoiding tracking cookies, and staying informed about data protection laws."},       
            {"scam", "To protect yourself from scams, avoid clicking on unknown links, never share personal info over email, and use security software to detect suspicious activity. If unsure, report the message to a cybersecurity professional."},

        };

        private string lastKeyword = null;
        private bool awaitingMoreInfoConfirmation = false;

        public override string ProcessInput(string input, UserData userData)
        {
            input = input.ToLower();

            if (awaitingMoreInfoConfirmation && input.Contains("yes") && lastKeyword != null)
            {
                awaitingMoreInfoConfirmation = false;
                return keywordDetails.ContainsKey(lastKeyword)
                    ? keywordDetails[lastKeyword]
                    : "Sorry, I don't have more details on that topic.";
            }

            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    if (string.IsNullOrEmpty(userData.FavoriteTopic))
                    {
                        userData.FavoriteTopic = keyword;
                    }

                    lastKeyword = keyword;
                    awaitingMoreInfoConfirmation = true;
                    string response = GenerateResponse(keyword);
                    return $"{response} Would you like me to explain further?";
                }
                Random rand = new Random();
                int fallbackIndex;
                do
                {
                    fallbackIndex = rand.Next(fallbackResponses.Count);
                } while (fallbackIndex == lastFallbackIndex);

                lastFallbackIndex = fallbackIndex;
                return fallbackResponses[fallbackIndex];

            }

            // If user says yes but no context exists
            if (awaitingMoreInfoConfirmation && input.Contains("yes"))
            {
                awaitingMoreInfoConfirmation = false;
                return "Sorry, I need more context to provide further details.";
            }

            return null;
        }
        private List<string> fallbackResponses = new List<string>
{
            "I didn't quite understand that. Could you rephrase?",
            "Hmm, I'm not sure I follow. Can you ask that in a different way?",
            "That's outside my area of expertise. Try asking about online security."
};

        private int lastFallbackIndex = -1;


        public string GenerateResponse(string keyword)
        {
            var responses = keywordResponses[keyword];
            Random rand = new Random();
            return responses[rand.Next(responses.Count)];
        }
    }
}
