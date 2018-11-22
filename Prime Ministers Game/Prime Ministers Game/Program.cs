using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;

namespace Prime_Ministers_Game
{
    class Player
    {
        public Player()
        {
            Score = 0; // Default score
            DateOption = false;
        }

        public int Score
        {
            get;
            set;
        }

        public bool DateOption
        {
            get;
            set;
        }
    }

    class PrimeMinister
    {
        // Setting up the values for the PMs.
        // The { get; } means that they are read-only.
        public string Name { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public PrimeMinister(string name, string sd, string ed)
        {
            Name = name;
            StartDate = Convert.ToDateTime(sd, CultureInfo.GetCultureInfo("en-GB"));
            if (ed == "Incumbent")
            {
                string temp = DateTime.Today.ToString();
                string temp2 = $"{temp:dd/MM/yyyy}";
                EndDate = Convert.ToDateTime(temp2);
            }
            else
            {
                EndDate = Convert.ToDateTime(ed, CultureInfo.GetCultureInfo("en-GB"));
            }
        }
    }

    class Game
    {
        public static void StartGame(List<PrimeMinister> pm, Player player)
        {
            // Game loop starting (5 rounds)
            for (var loop = 0; loop < 5; loop++)
            {
            game_loop:
                Random rn = new Random();
                var rn_list = new List<int>();
                
                for (var i = 0; i < 6; i++)
                {
                    rn_list.Add(rn.Next(0, pm.Count));
                }

                PrimeMinister p0 = pm[rn_list[0]];
                PrimeMinister p1 = pm[rn_list[1]];
                PrimeMinister p2 = pm[rn_list[2]];
                PrimeMinister p3 = pm[rn_list[3]];
                PrimeMinister p4 = pm[rn_list[4]];
                PrimeMinister p5 = pm[rn_list[5]];
                
                if (p0.Name == p1.Name ||
                    p1.Name == p2.Name ||
                    p0.Name == p2.Name)
                {
                    goto game_loop;
                }
                
                if (player.DateOption)
                {
                    if (p0.Name == p3.Name || p1.Name == p3.Name || p2.Name == p3.Name || p4.Name == p3.Name || p5.Name == p3.Name ||
                        p0.Name == p4.Name || p1.Name == p4.Name || p2.Name == p4.Name || p3.Name == p4.Name || p5.Name == p4.Name ||
                        p0.Name == p5.Name || p1.Name == p5.Name || p2.Name == p5.Name || p3.Name == p5.Name || p4.Name == p5.Name)
                    {
                        goto game_loop;
                    }
                }

            pre_pm:
                Console.WriteLine("Prime Minister 1: {0}", p0.Name);
                Console.WriteLine("Prime Minister 2: {0}", p1.Name);
                Console.WriteLine("Prime Minister 3: {0}", p2.Name);
                Console.Write("Which PM served first? ");
                string game_answer = Console.ReadLine()?.ToUpper();

                if (game_answer == p0.Name.ToUpper())
                {
                    if (p0.StartDate < p1.StartDate && p0.StartDate < p2.StartDate)
                    {
                        Console.WriteLine("Correct!");
                        player.Score++;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect...");
                        Console.ReadKey();
                    }
                }
                else if (game_answer == p1.Name.ToUpper())
                {
                    if (p1.StartDate < p0.StartDate && p1.StartDate < p2.StartDate)
                    {
                        Console.WriteLine("Correct!");
                        player.Score++;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect...");
                        Console.ReadKey();
                    }
                }
                else if (game_answer == p2.Name.ToUpper())
                {
                    if (p2.StartDate < p0.StartDate && p2.StartDate < p1.StartDate)
                    {
                        Console.WriteLine("Correct!");
                        player.Score++;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("That is not one of the prime ministers on the list.");
                    Console.ReadKey();
                    Console.Clear();
                    goto pre_pm;
                }
                   
                // If the player selected to play the extension.
                if (player.DateOption)
                {
                    PrimeMinister[] mains = {p3, p4, p5};
                    Console.Clear();
                    Extension(pm, player, mains);
                }
                
                Console.Clear();
            }
            
            EndGame(pm, player);
        }

        public static void Extension(List<PrimeMinister> pm, Player player, PrimeMinister[] mains)
        {
            Random rn = new Random();
            // There are 6 possible permutations between 3 different values,
            // so to truly randomise the order of the values, I must get a random number between 0 and 5.
            int rl = rn.Next(0, 6);
            
        pre_loop:
            Console.WriteLine("Second section:");
            Console.WriteLine("Which PM served during: {0}-{1}", $"{mains[0].StartDate:dd/MM/yyyy}",
                $"{mains[0].EndDate:dd/MM/yyyy}");
            Console.WriteLine();
            Console.WriteLine("Options:");

            switch (rl)
            {
                case 0: // 1 2 3
                    Console.WriteLine("- {0}", mains[0].Name);
                    Console.WriteLine("- {0}", mains[1].Name);
                    Console.WriteLine("- {0}", mains[2].Name);
                    break;
                case 1: // 1 3 2
                    Console.WriteLine("- {0}", mains[0].Name);
                    Console.WriteLine("- {0}", mains[2].Name);
                    Console.WriteLine("- {0}", mains[1].Name);
                    break;
                case 2: // 2 1 3
                    Console.WriteLine("- {0}", mains[1].Name);
                    Console.WriteLine("- {0}", mains[0].Name);
                    Console.WriteLine("- {0}", mains[2].Name);
                    break;
                case 3: // 2 3 1
                    Console.WriteLine("- {0}", mains[1].Name);
                    Console.WriteLine("- {0}", mains[2].Name);
                    Console.WriteLine("- {0}", mains[0].Name);
                    break;
                case 4: // 3 1 2
                    Console.WriteLine("- {0}", mains[2].Name);
                    Console.WriteLine("- {0}", mains[0].Name);
                    Console.WriteLine("- {0}", mains[1].Name);
                    break;
                case 5: // 3 2 1
                    Console.WriteLine("- {0}", mains[2].Name);
                    Console.WriteLine("- {0}", mains[1].Name);
                    Console.WriteLine("- {0}", mains[0].Name);
                    break;
            }
            
            Console.Write("Please type the name: ");
            // Make sure that all names are in upper case.
            string answer = Console.ReadLine()?.ToUpper();
            string first_name = mains[0].Name.ToUpper();
            string second_name = mains[1].Name.ToUpper();
            string third_name = mains[2].Name.ToUpper();

            if (answer == first_name)
            {
                Console.WriteLine("Correct!");
                player.Score++;
                Console.ReadKey();
            }
            else if (answer == second_name || answer == third_name)
            {
                Console.WriteLine("Incorrect...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("That was not one of the options.");
                Console.ReadKey();
                Console.Clear();
                goto pre_loop;
            }

        }

        public static void EndGame(List<PrimeMinister> pm, Player player)
        {
            if (player.DateOption)
            {
                Console.WriteLine("Score: {0} out of 10.", player.Score);
                if (player.Score == 10)
                    Console.WriteLine("Perfect!!");
                else if (player.Score > 7)
                    Console.WriteLine("Nice job!");
                else if (player.Score > 5)
                    Console.WriteLine("Not bad.");
                else
                    Console.WriteLine("Better luck next time!");

                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Score: {0} out of 5.", player.Score);
                if (player.Score == 5)
                    Console.WriteLine("Perfect!!");
                else if (player.Score > 2)
                    Console.WriteLine("Not bad.");
                else
                    Console.WriteLine("Better luck next time!");

                Console.ReadKey();
                Console.Clear();
            }

            // Reset everything back to default.
            player.Score = 0;
            player.DateOption = false;
            // Sent back to the main menu.
            Menu(pm, player);
        }

        public static void HowToPlay()
        {
            Console.WriteLine("Prime Minister Game");
            Console.WriteLine("You will be presented with three British Prime Ministers, " +
                              "and you will have to guess which of them served first.");
            Console.WriteLine("There will be five rounds of questioning and you will be given " +
                              "a score out of five depending on how many you got right.");
            Console.WriteLine("If you have selected the 'Guess PM from date' option, " +
                              "you will also be faced with a second section of the game every " +
                              "round. This section will give you the date that a PM entered into office " +
                              "and when they left office. You will also be given a list of three " +
                              "names of Prime Ministers. You will have to guess which one was incumbent at the time.");
            Console.WriteLine("In that case, your score will be out of ten from the five rounds.");
            Console.WriteLine();
            Console.WriteLine("Press any key when you are ready to return to the menu.");
            Console.ReadKey();
        }

        public static void Menu(List<PrimeMinister> pm, Player player)
        {
            // In any case where there is an error, it will reprint the menu.
            while (true)
            {
                Console.WriteLine("Prime Minister Game");
                Console.WriteLine("1) How to play");
                // Keep the extension in the player class so that the call for StartGame only has two parameters.
                if (player.DateOption)
                    Console.WriteLine("2) Guess PM from date: ON");
                else
                    Console.WriteLine("2) Guess PM from date: OFF");
                Console.WriteLine("3) Start game");
                Console.WriteLine("4) Quit game");
                Console.Write("Choose an option: ");
                string menu_answer = Console.ReadLine();
                int nma = 0;

                // If the option input is not an int.
                try
                {
                    nma = Convert.ToInt32(menu_answer);
                }
                catch
                {
                    Console.WriteLine("Please input a menu option.");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(pm, player);
                }

                // Menu options
                switch (nma)
                {
                    case 1:
                        Console.Clear();
                        HowToPlay();
                        Console.Clear();
                        continue;
                    case 2:
                        if (player.DateOption)
                        {
                            player.DateOption = false;
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            player.DateOption = true;
                            Console.Clear();
                            continue;
                        }
                    case 3:
                        Console.Clear();
                        StartGame(pm, player);
                        Console.Clear();
                        continue;
                    case 4:
                        Console.WriteLine();
                        Console.WriteLine("Thank you for playing!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("That is not a menu option.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                }
            }
        }

        public static void Main(string[] args)
        {
            /* To get the information out of the .csv file.
             * Headers:
             * No   Prime Minister  Date of Birth   Start date of term  End date of term
             * The No and DoB of the PM is unimportant in the task so it will be skipped.
             */
            List<PrimeMinister> prime_ministers = new List<PrimeMinister>();
            
            try
            {
                using (StreamReader reader = new StreamReader(@"PrimeMinisters.csv"))
                {
                    while (!reader.EndOfStream) // Until the end of the file.
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values[0] == "No")
                        {
                            continue; // First line has only headers.
                        }
                        else
                        {
                            prime_ministers.Add(new PrimeMinister(
                                values[1], // Name
                                values[3], // Start date of term
                                values[4] // End date of term
                                ));
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("PrimeMinisters.csv has not been found.");
                Console.WriteLine("Please put the .csv file in the same location as this .exe, " +
                    "and then reload the program.");
                Console.ReadKey();
                Environment.Exit(1);
            }
                
                // For testing if the file is working or not.
                /*
                for (int i = 0; i < prime_ministers.Count; i++)
                {
                    var a = prime_ministers[i];
                    Console.WriteLine(@"{0}", a.Name);
                    Console.WriteLine(@"{0}", a.StartDate);
                    Console.WriteLine(@"{0}", a.EndDate);
                }
                Console.ReadKey();
                */
            
            // The player class to be used.
            Player player = new Player();
            
            Menu(prime_ministers, player);
        }
    }
}
