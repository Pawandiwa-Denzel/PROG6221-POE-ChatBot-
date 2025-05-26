using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_Bot
{
    public class SentimentDetection : ChatBotFeature
    {
        public override string ProcessInput(string input, UserData userData)
        {
            if (input.Contains("worried") || input.Contains("concerned") || input.Contains("anxious"))
            {
                userData.CurrentMood = "worried";
                return "I understand this can be concerning. Let me help you feel more secure.";
            }
            else if (input.Contains("happy") || input.Contains("excited") || input.Contains("great"))
            {
                userData.CurrentMood = "happy";
                return "That's great to hear! Let's keep your online experience positive and secure.";
            }
            else if (input.Contains("angry") || input.Contains("frustrated") || input.Contains("annoyed"))
            {
                userData.CurrentMood = "frustrated";
                return "I'm sorry you're feeling this way. Let's work through this together.";
            }

            return null;
        }
    }
}
