using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_Bot
{

    public class MemoryFeature : ChatBotFeature
    {
        private readonly HashSet<string> _trackedTopics = new HashSet<string> { "password", "privacy", "phishing" };

        public override string ProcessInput(string input, UserData userData)
        {
            // Store favorite topic if detected in any keyword
            foreach (var topic in _trackedTopics)
            {
                if (input.Contains(topic) && (string.IsNullOrEmpty(userData.FavoriteTopic) || userData.FavoriteTopic != topic))
                {
                    userData.FavoriteTopic = topic;
                    break; // Store only the first matching topic
                }
            }

            // Recall functionality - works for any stored topic
            if (!string.IsNullOrEmpty(userData.FavoriteTopic))
            {
                if (input.Contains("remember") || input.Contains("recall") ||
                    input.Contains("what did i like") || input.Contains("my interest"))
                {
                    return $"I remember you're interested in {userData.FavoriteTopic}. " +
                           $"Here's a tip: {GetTopicTip(userData.FavoriteTopic)}";
                }

                // Proactively mention remembered topic in responses
                if (new Random().Next(0, 3) == 0) // 33% chance to mention
                {
                    return $"Since you're interested in {userData.FavoriteTopic}, " +
                           $"you might want to know: {GetTopicTip(userData.FavoriteTopic)}";
                }
            }

            // Mood-based responses
            if (!string.IsNullOrEmpty(userData.CurrentMood) &&
                input.Contains("how are you"))
            {
                return MoodBasedResponse(userData.CurrentMood);
            }

            return null;
        }

        private string GetTopicTip(string topic)
        {
            return topic switch
            {
                "privacy" => "Always check app permissions before installing new software.",
                "password" => "Change your passwords every 3-6 months for better security.",
                "phishing" => "Hover over links to see the actual URL before clicking.",
                _ => "Regular security updates are important for all online activities."
            };
        }

        private string MoodBasedResponse(string mood)
        {
            return mood switch
            {
                "worried" => "I hope my previous advice helped ease your concerns. Is there anything else worrying you?",
                "happy" => "I'm glad you're feeling positive! Staying happy online is just as important as staying safe.",
                "frustrated" => "I understand this can be frustrating. Let me know how I can help make things clearer.",
                _ => "I'm doing well, thank you for asking!"
            };
        }
    }
}