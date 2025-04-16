
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
