using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;

namespace CyberSecurity_Bot
{


    class Program
    {
        private static List<ChatBotFeature> features = new List<ChatBotFeature>
        {
            new KeywordRecognition(),
            new SentimentDetection(),
            new MemoryFeature()
        };

        static void Main(string[] args)
        {
            PlayVoice();
            ShowAscii();
            UserData userData = GreetUser();
            ChatBot(userData);
        }

        static void PlayVoice()
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

        static void ShowAscii()
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

        static UserData GreetUser()
        {
            var userData = new UserData();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hi, What's your name?");
            Console.ResetColor();
            Console.Write(">>> ");
            userData.Name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userData.Name))
            {
                userData.Name = "Friend";
            }

            Console.WriteLine($"\nWelcome, {userData.Name}! I'm here to help you stay safe online.\n");
            return userData;
        }

        static void ChatBot(UserData userData)
        {
            while (true)
            {
                Divider();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ask me a question or type 'x' to quit:");
                Console.ResetColor();
                Console.Write(">>> ");
                string input = Console.ReadLine()?.ToLower().Trim();
                userData.ConversationHistory.Add(input);

                if (string.IsNullOrWhiteSpace(input))
                {
                    Response("Type something...");
                }
                else if (input == "x")
                {
                    Response("See ya! Stay safe and secure online - there are weirdos out there!");
                    break;
                }
                else
                {
                    string response = ProcessFeatures(input, userData);
                    if (response == null)
                    {
                        response = DefaultResponse(input);
                    }
                    Response(response);
                }

                Console.WriteLine();
            }
        }

        static string ProcessFeatures(string input, UserData userData)
        {
            foreach (var feature in features)
            {
                string response = feature.ProcessInput(input, userData);
                if (response != null)
                {
                    return response;
                }
            }
            return null;
        }

        static string DefaultResponse(string input)
        {
            if (input.Contains("how are you"))
            {
                return "I'm fine, thanks!";
            }
            else if (input.Contains("purpose"))
            {
                return "My purpose is to help you stay safe online.";
            }
            else if (input.Contains("what can i ask"))
            {
                return "You can ask me about password safety, phishing, privacy, and safe browsing.";
            }
            else
            {
                return "I didn't quite understand that. Could you rephrase?";
            }
        }

        static void Divider()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 120));
            Console.ResetColor();
        }

        static void Response(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
        }
    }
}