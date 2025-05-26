using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CyberSecurity_Bot
{
    public class ResponseManager
    {
        private Dictionary<string, List<string>> keywordResponses;
        private Dictionary<string, List<string>> detailedExplanations;
        private Dictionary<string, List<string>> technicalDeepDives;
        private Dictionary<string, List<string>> practicalExamples;
        private Dictionary<string, int> lastUsedResponseIndex;
        private Dictionary<string, string> userMemory;
        private Random random;

        private List<string> unknownKeywordResponses = new List<string>
        {
            "🤔 I'm not sure about that topic. Could you ask something else?",
            "❓ That one's a bit outside my expertise. Try another question?"
        };

        private List<string> unknownMemoryResponses = new List<string>
        {
            "🧠 I don't seem to remember that. Could you remind me?",
            "😕 Sorry, I can't recall that info. Want to try telling me again?"
        };

        private List<string> generalResponses = new List<string>
        {
            "I'm functioning optimally, thanks for asking! How about you?",
            "My circuits are buzzing with security knowledge! What can I help you with?",
            "I'm doing well, though constantly vigilant against cyber threats! And you?"
        };

        private List<Regex> favoritePatterns = new List<Regex>
        {
            new Regex(@"i (like|love|enjoy) (.*)", RegexOptions.IgnoreCase),
            new Regex(@"i('m| am) interested (in|by) (.*)", RegexOptions.IgnoreCase),
            new Regex(@"(my favorite|favourite) (topic|subject) is (.*)", RegexOptions.IgnoreCase),
            new Regex(@"i want to learn (more )?about (.*)", RegexOptions.IgnoreCase)
        };
        private List<string> worryResponses = new List<string>
        {
            "It's completely normal to feel worried about these things. Let me help you understand it better.",
            "Cybersecurity can be worrying, but knowledge is your best defense. Here's what you should know...",
            "I understand your concern. The good news is there are ways to protect yourself. Let me explain..."
        };

        private List<Regex> namePatterns = new List<Regex>
        {
            new Regex(@"my name is (.*)", RegexOptions.IgnoreCase),
            new Regex(@"i am (.*)", RegexOptions.IgnoreCase),
            new Regex(@"call me (.*)", RegexOptions.IgnoreCase),
            new Regex(@"i'm (.*)", RegexOptions.IgnoreCase)
        };


        public ResponseManager()
        {
            keywordResponses = new Dictionary<string, List<string>>();
            detailedExplanations = new Dictionary<string, List<string>>();
            technicalDeepDives = new Dictionary<string, List<string>>();
            practicalExamples = new Dictionary<string, List<string>>();
            lastUsedResponseIndex = new Dictionary<string, int>();
            userMemory = new Dictionary<string, string>();
            random = new Random();

            InitializeResponses();
        }

        private void InitializeResponses()
        {
            #region Password Security
            keywordResponses["password"] = new List<string>
            {
                "🔐 Password security is critical! Use passphrases with 16+ characters mixing uppercase, lowercase, numbers and symbols like 'BlueCoffeeTable$2023!'",
                "💡 The NIST recommends: 1) Use memorable passphrases 2) Enable 2FA everywhere 3) Never reuse passwords 4) Use a password manager like Bitwarden",
                "🚨 81% of breaches involve weak passwords. Try diceware method: 'CorrectHorseBatteryStaple' is more secure than 'P@ssw0rd!'"
            };

            detailedExplanations["password"] = new List<string>
            {
                "🔍 Deep Password Security:\n\n• Password cracking tools can test billions of combinations per second\n• Rainbow tables reverse-engineer hashed passwords\n• Credential stuffing uses leaked passwords on other sites\n• Always check haveibeenpwned.com\n• Consider FIDO2 security keys for ultimate protection",
                "🧠 Advanced Password Strategy:\n\n1. Use different password tiers (banking, email, other sites)\n2. Implement passkeys where available\n3. Beware of phishing sites stealing passwords\n4. Monitor for breaches with tools like Firefox Monitor\n5. Never store passwords in plaintext anywhere"
            };

            technicalDeepDives["password"] = new List<string>
            {
                "⚙️ Technical Password Mechanics:\n\n• Modern GPUs can test 100 billion hashes/sec\n• PBKDF2, bcrypt and Argon2 are secure hashing algorithms\n• Salting prevents rainbow table attacks\n• Pepper values add another protection layer\n• Password managers use zero-knowledge encryption",
                "🔬 Password Cryptography:\n\n• AES-256 encryption for password managers\n• SRP (Secure Remote Password) protocol\n• HMAC-based One-Time Password (HOTP) systems\n• Time-based OTP (TOTP) algorithms\n• WebAuthn standards for passwordless auth"
            };

            practicalExamples["password"] = new List<string>
            {
                "🛠️ Practical Password Tips:\n\n1. Create a strong password: 'WinterSnowfall@2023!'\n2. Set up 2FA on your email right now\n3. Install Bitwarden and import your passwords\n4. Check haveibeenpwned.com for breaches\n5. Enable passwordless login where available",
                "📝 Password Exercise:\n1. Think of a memorable phrase\n2. Add special characters and numbers\n3. Make it at least 16 characters\n4. Test its strength at passwordmonster.com\n5. Store it in your password manager"
            };
            #endregion

            #region Phishing
            keywordResponses["phishing"] = new List<string>
            {
                "🎣 Modern phishing uses:\n• Fake login pages identical to real ones\n• Urgent 'account suspension' messages\n• Personalized info from your social media\n• Calls pretending to be Microsoft support",
                "🛡️ Phishing Defense:\n1. Hover to see real URLs\n2. Check for slight domain variations (e.g., micros0ft.com)\n3. Look for poor grammar\n4. Verify unexpected requests via another channel\n5. Use email aliases for signups",
                "💀 94% of malware comes via email phishing. Always verify sender addresses carefully!"
            };

            detailedExplanations["phishing"] = new List<string>
            {
                "🌐 Phishing Techniques Deep Dive:\n\n• Spear phishing - Targets specific individuals\n• Whaling - Targets executives\n• Clone phishing - Copies real emails\n• BEC (Business Email Compromise) - Fake vendor invoices\n• Smishing - Phishing via SMS\n• Vishing - Voice call scams",
                "🕵️ Advanced Phishing Detection:\n\n1. Check email headers for inconsistencies\n2. Examine 'From' addresses carefully\n3. Watch for urgency/threats in language\n4. Beware of requests for sensitive info\n5. Use email authentication tools like DMARC\n6. Consider browser isolation for risky links"
            };

            technicalDeepDives["phishing"] = new List<string>
            {
                "⚙️ Phishing Infrastructure:\n\n• Domain generation algorithms (DGAs)\n• Fast-flux DNS techniques\n• HTTPS phishing sites with valid certificates\n• Homograph attacks using Unicode characters\n• Email spoofing via SMTP vulnerabilities",
                "🔬 Phishing Forensics:\n\n• SPF, DKIM and DMARC records\n• Email header analysis\n• WHOIS record investigation\n• SSL certificate fingerprinting\n• Network traffic analysis of phishing kits"
            };

            practicalExamples["phishing"] = new List<string>
            {
                "🛠️ Anti-Phishing Practice:\n1. Examine a real phishing email (check reportphishing.net)\n2. Practice hovering over links without clicking\n3. Set up email filters for common phishing terms\n4. Enable Sender Policy Framework (SPF) on your domain\n5. Conduct a phishing test with your team",
                "📝 Phishing Defense Drill:\n1. Receive a test phishing email\n2. Identify all red flags\n3. Report it properly\n4. Check the headers\n5. Analyze the URL structure"
            };
            #endregion

            #region Privacy
            keywordResponses["privacy"] = new List<string>
            {
                "👁️ Privacy protection essentials:\n• Use Signal or Telegram for messaging\n• Enable 2FA everywhere\n• Review app permissions monthly\n• Use burner emails for signups\n• Consider using a password manager",
                "📱 Mobile privacy tips:\n1. Disable ad tracking IDs\n2. Review location permissions\n3. Use app lockers for sensitive apps\n4. Encrypt your device storage\n5. Be wary of apps requesting unnecessary permissions"
            };

            detailedExplanations["privacy"] = new List<string>
            {
                "🔐 Advanced privacy measures:\n• Use Linux for maximum control\n• Consider Tails OS for sensitive work\n• Learn about Tor and onion routing\n• Understand metadata and how it can reveal information\n• Use hardware security keys for critical accounts",
                "🌍 The privacy paradox:\nWhile complete privacy is nearly impossible today, you can significantly reduce your exposure through:\n1. Data minimization\n2. Encryption\n3. Anonymization techniques\n4. Understanding data retention policies\n5. Regular digital footprint audits"
            };

            technicalDeepDives["privacy"] = new List<string>
            {
                "⚙️ Privacy Technologies:\n• End-to-end encryption protocols\n• Zero-knowledge proofs\n• Differential privacy algorithms\n• Onion routing and mix networks\n• Homomorphic encryption",
                "🔬 Privacy Engineering:\n1. Data anonymization techniques\n2. Metadata stripping tools\n3. VPN tunneling protocols\n4. Blockchain for identity management\n5. Secure multi-party computation"
            };

            practicalExamples["privacy"] = new List<string>
            {
                "🛠️ Privacy Action Plan:\n1. Audit your social media privacy settings\n2. Install Privacy Badger browser extension\n3. Set up a VPN on all devices\n4. Create email aliases for different services\n5. Enable disk encryption on your computer",
                "📝 Privacy Checklist:\n• Review app permissions\n• Clear browser cookies\n• Use private browsing mode\n• Disable location services\n• Opt-out of data sharing"
            };
            #endregion

            // Initialize last used index
            foreach (var key in keywordResponses.Keys)
                lastUsedResponseIndex[key] = -1;
        }

        public List<string> GetKeywords() => keywordResponses.Keys.ToList();

        public string GetResponse(string input)
        {
            input = input.ToLower();

            // Check for name patterns first
            string detectedName = DetectName(input);
            if (detectedName != null)
            {
                RememberUserInfo("name", detectedName);
                return $"👋 Nice to meet you, {detectedName}! How can I help you with cybersecurity today?";
            }

            if (input.Contains("how are you"))
                return generalResponses[random.Next(generalResponses.Count)];

            if (input.Contains("what can i ask") || input.Contains("help"))
                return $"You can ask me about: {string.Join(", ", GetKeywords())}. Or share your concerns - I might have helpful advice!";

            // Check for worry/concern expressions
            if (input.Contains("worried") || input.Contains("concerned") || input.Contains("scared"))
            {
                string topic = GetKeywords().FirstOrDefault(k => input.Contains(k));
                if (topic != null)
                {
                    return $"{worryResponses[random.Next(worryResponses.Count)]}\n\n{GetResponse(topic)}";
                }
                return worryResponses[random.Next(worryResponses.Count)];
            }

            string detectedTopic = DetectFavoriteTopic(input);
            if (detectedTopic != null)
            {
                RememberUserInfo("favourite", detectedTopic);
                string name = RecallUserInfo("name");
                if (!name.StartsWith("🧠"))
                {
                    return $"🔖 Got it, {name}! I've noted that you're interested in {detectedTopic}. What would you like to know about it?";
                }
                return $"🔖 Got it! I've noted that you're interested in {detectedTopic}. What would you like to know about it?";
            }

            // Check if user is asking about remembered info
            if (input.Contains("remember") || input.Contains("my name") || input.Contains("favorite topic"))
            {
                string name = RecallUserInfo("name");
                string topic = RecallUserInfo("favourite");

                if (name.StartsWith("🧠") && topic.StartsWith("🧠"))
                    return "I don't seem to remember your name or favorite topic yet. Could you tell me?";
                if (name.StartsWith("🧠"))
                    return $"I know you're interested in {topic}, but I don't know your name yet. What should I call you?";
                if (topic.StartsWith("🧠"))
                    return $"Hi {name}! I remember you, but I don't know your favorite topic yet. What cybersecurity topics interest you?";

                return $"Hi {name}! I remember you're interested in {topic}. Want to explore that more?";
            }

            foreach (var keyword in GetKeywords())
            {
                if (input.Contains(keyword))
                {
                    return keywordResponses[keyword][random.Next(keywordResponses[keyword].Count)];
                }
            }

            return unknownKeywordResponses[random.Next(unknownKeywordResponses.Count)];
        }

        private string DetectName(string input)
        {
            foreach (var pattern in namePatterns)
            {
                var match = pattern.Match(input);
                if (match.Success)
                {
                    return match.Groups[1].Value.Trim();
                }
            }
            return null;
        }


        private string DetectFavoriteTopic(string input)
        {
            foreach (var pattern in favoritePatterns)
            {
                var match = pattern.Match(input);
                if (match.Success)
                {
                    string topic = match.Groups[match.Groups.Count - 1].Value.Trim();
                    return GetKeywords().FirstOrDefault(k => topic.Contains(k, StringComparison.OrdinalIgnoreCase));
                }
            }
            return null;
        }

        public string GetDetailedExplanation(string topic) =>
            detailedExplanations.TryGetValue(topic, out var list) ? list[random.Next(list.Count)] : unknownKeywordResponses[random.Next(unknownKeywordResponses.Count)];

        public string GetTechnicalDeepDive(string topic) =>
            technicalDeepDives.TryGetValue(topic, out var list) ? list[random.Next(list.Count)] : unknownKeywordResponses[random.Next(unknownKeywordResponses.Count)];

        public string GetPracticalExamples(string topic) =>
            practicalExamples.TryGetValue(topic, out var list) ? list[random.Next(list.Count)] : unknownKeywordResponses[random.Next(unknownKeywordResponses.Count)];

        public string GetSentimentResponse(string sentiment, string topic)
        {
            string baseResponse = GetResponse(topic);

            return sentiment.ToLower() switch
            {
                "worried" => $"I completely understand your concern about {topic}. It's smart to be cautious!\n\n{baseResponse}\n\nWould you like me to explain this in more detail?",
                "curious" => $"That's an excellent question about {topic}! The field evolves constantly.\n\n{baseResponse}\n\nWould you like a deeper technical explanation?",
                "frustrated" => $"I hear your frustration with {topic}. Cybersecurity can be complex!\n\n{baseResponse}\n\nShould I break this down further?",
                _ => baseResponse
            };
        }

        public void RememberUserInfo(string key, string value) => userMemory[key.ToLower()] = value;

        public string RecallUserInfo(string key) =>
            userMemory.TryGetValue(key.ToLower(), out var value)
                ? value
                : unknownMemoryResponses[random.Next(unknownMemoryResponses.Count)];

        public string GreetUser()
        {
            string name = RecallUserInfo("name");
            string favTopic = RecallUserInfo("favourite");

            if (name.StartsWith("🧠") && favTopic.StartsWith("🧠"))
                return "👋 Hi there! I'd love to know more about you. What's your name and what cybersecurity topics interest you?";

            if (name.StartsWith("🧠"))
                return $"👋 Welcome! I know you're interested in {favTopic}, but I don't know your name yet. What should I call you?";

            if (favTopic.StartsWith("🧠"))
                return $"👋 Welcome back, {name}! What cybersecurity topics would you like to explore today?";

            return $"👋 Welcome back, {name}! Ready to explore more about {favTopic}?";
        }
    }
}
