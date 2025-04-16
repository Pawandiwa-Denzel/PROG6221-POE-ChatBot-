
    using System;
    using System.Media;
    using System.Threading;

    namespace CyberSecurity_Bot
    
    {
        class Program
        {
            static void Main(string[] args)
            {
                PlayVoice();
                ShowAscii();
                GreetUser();

            
            }

            static void PlayVoice()
            {
                try
                {
                    SoundPlayer player = new SoundPlayer("voice.wav");
                    player.PlaySync(); // Waits until audio finishes
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Voice greeting could not be played. Make sure 'voice' exists.");
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
        static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hi, What's your name?");
            Console.ResetColor();
            Console.Write(">>> ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Friend";
            }

            Console.WriteLine($"\nWelcome, {name}! I'm here to help you stay safe online.\n");

            ChatBot();
        }

        static void ChatBot()
        {
            while (true)
            {
                Divider();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ask me a question or type 'x' to quit:");
                Console.ResetColor();
                Console.Write(">>> ");
                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Type something...");
                }
                else if (input.Contains("how are you"))
                {
                    Response("I'm fine , thanks!");
                }
                else if (input.Contains("purpose"))
                {
                    Response("My purpose is to help you stay safe online.");
                }
                else if (input.Contains("what can i ask"))
                {
                    Response("You can ask me about password safety, phishing, and safe browsing.");
                }
                else if (input.Contains("password"))
                {
                    Response("Use strong, unique passwords that contain numbers, special characters and diffrent cases. Avoid reusing passwords!");
                }
                else if (input.Contains("phishing"))
                {
                    Response("Be mindful of suspicious emails and links sent to you. Always verify the source.");
                }
                else if (input.Contains("safe browsing"))
                {
                    Response("Ensure websites use HTTPS, and don't download files from untrusted sources.");
                }
                else if (input == "x")
                {
                    Response("See ya! and  stay safe and secure online there are weirdos out there");
                    break;
                }
                else
                {
                    Response("I didn't quite understand that. Could you rephrase?");
                }

                Console.WriteLine();
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
                    Thread.Sleep(35); // Simulate typing
                }
                Console.WriteLine();
            }


        }
    }
