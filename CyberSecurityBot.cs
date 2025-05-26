using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

namespace CyberSecurity_Bot
{
    public class CyberSecurityBot
    {
        private ResponseManager responseManager;
        private string userName;
        private string currentTopic = "";
        private bool awaitingConfirmation = false;
        private int conversationDepth = 0;

        public CyberSecurityBot()
        {
            responseManager = new ResponseManager();
        }

        public void Run()
        {
            PlayVoice();
            ShowAscii();
            GreetUser();
            ChatLoop();
        }

        private void PlayVoice()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("voice.wav");
                player.PlaySync();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Voice greeting could not be played. Make sure 'voice.wav' exists.");
                Console.ResetColor();
            }
        }

        private void ShowAscii()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════════╗
║ 🔒                                                                       ║
║              CYBER SECURITY AWARENESS BOT – Stay Safe Online             ║
║                                                               🔒         ║
╚══════════════════════════════════════════════════════════════════════════╝
");
            Console.ResetColor();
        }

        private void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hi, What's your name?");
            Console.ResetColor();
            Console.Write(">>> ");
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "Friend";
            }

            Console.WriteLine($"\nWelcome, {userName}! I'm here to help you stay safe online.\n");
            responseManager.RememberUserInfo("name", userName);
        }

        private void ChatLoop()
        {
            while (true)
            {
                Divider();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ask me about cybersecurity or type 'x' to quit:");
                Console.ResetColor();
                Console.Write(">>> ");
                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Response("I didn't catch that. Could you repeat?");
                    continue;
                }

                if (input == "x")
                {
                    Response($"Goodbye {userName}! Stay safe and secure online - there are weirdos out there!");
                    break;
                }

                ProcessInput(input);
                Console.WriteLine();
            }
        }

        private void ProcessInput(string input)
        {
            if (awaitingConfirmation)
            {
                HandleFollowUp(input);
                return;
            }

            if (!input.Contains("more") && !input.Contains("explain"))
            {
                conversationDepth = 0;
            }

            if (input.Contains("worried") || input.Contains("concerned") || input.Contains("scared"))
            {
                currentTopic = DetectTopic(input);
                Response(responseManager.GetSentimentResponse("worried", currentTopic));
                AskToElaborate();
                return;
            }

            if (input.Contains("curious") || input.Contains("wonder") || input.Contains("tell me more"))
            {
                currentTopic = DetectTopic(input);
                Response(responseManager.GetSentimentResponse("curious", currentTopic));
                AskToElaborate();
                return;
            }

            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
            {
                currentTopic = DetectTopic(input);
                Response(responseManager.GetSentimentResponse("frustrated", currentTopic));
                AskToElaborate();
                return;
            }

            if (input.Contains("remember") && input.Contains("privacy"))
            {
                responseManager.RememberUserInfo("interest", "privacy");
                Response($"Got it {userName}! I'll remember you're interested in privacy. It's a crucial topic!");
                return;
            }

            string interest = responseManager.RecallUserInfo("interest");
            if (interest != null && input.Contains(interest))
            {
                currentTopic = interest;
                Response($"Since you're interested in {interest}, here's something you might find useful: " +
                        responseManager.GetResponse(interest));
                AskToElaborate();
                return;
            }

            if (input.StartsWith("why ") || input.StartsWith("how ") || input.EndsWith("?"))
            {
                currentTopic = DetectTopic(input);
                Response(responseManager.GetResponse(currentTopic));
                AskToElaborate("Would you like a more technical explanation of this?");
                return;
            }

            string response = null;
            foreach (var keyword in responseManager.GetKeywords())
            {
                if (input.Contains(keyword))
                {
                    currentTopic = keyword;
                    response = responseManager.GetResponse(keyword);
                    break;
                }
            }

            if (response != null)
            {
                Response(response);
                AskToElaborate();
            }
            else if (input.Contains("how are you"))
            {
                Response("I'm functioning optimally, thanks for asking! How about you?");
            }
            else if (input.Contains("purpose"))
            {
                Response("My purpose is to help you navigate the digital world safely. What concerns you most about online security?");
            }
            else if (input.Contains("what can i ask"))
            {
                Response($"You can ask me about: {string.Join(", ", responseManager.GetKeywords())}. Or share your concerns – I might have helpful advice!");
            }
            else
            {
                Response("I'm not sure I understand. Could you try rephrasing or ask about cybersecurity topics?");
            }
        }

        private void HandleFollowUp(string input)
        {
            if (input.Contains("yes") || input.Contains("yeah") || input.Contains("sure") || input.Contains("explain") || input.Contains("more"))
            {
                conversationDepth++;

                if (conversationDepth == 1)
                {
                    Response(responseManager.GetDetailedExplanation(currentTopic));
                    AskToElaborate("Would you like even more technical details about this?");
                }
                else if (conversationDepth == 2)
                {
                    Response(responseManager.GetTechnicalDeepDive(currentTopic));
                    AskToElaborate("I've given you quite detailed information. Would you like practical examples?");
                }
                else if (conversationDepth >= 3)
                {
                    Response(responseManager.GetPracticalExamples(currentTopic));
                    awaitingConfirmation = false;
                    conversationDepth = 0;
                }
            }
            else
            {
                awaitingConfirmation = false;
                conversationDepth = 0;
                Response("Understood. What else would you like to know about?");
            }
        }

        private string DetectTopic(string input)
        {
            foreach (var keyword in responseManager.GetKeywords())
            {
                if (input.Contains(keyword))
                    return keyword;
            }
            return "cybersecurity";
        }

        private void Divider()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 80));
            Console.ResetColor();
        }

        private void Response(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(20);
            }
            Console.WriteLine();
        }

        private void AskToElaborate(string customPrompt = "\nWould you like me to elaborate on this?")
        {
            Response(customPrompt);
            awaitingConfirmation = true;
        }
    }
}
