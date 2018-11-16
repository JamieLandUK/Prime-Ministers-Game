using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prime_Ministers_Game
{
    class Player
    {
        private int _score;
        private bool _date_option;

        public Player()
        {
            _score = 0; // Default score
            _date_option = false;
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; } // So the program can just add one to the score.
        }

        public bool DateOption
        {
            get { return _date_option; }
            set { _date_option = value; }
        }
    }

    class PrimeMinister
    {
        // Setting up the values for the PMs.
        // The { get; } means that they are read-only.
        public int Num { get; }
        public string Name { get; }
        public string StartDate { get; }
        public string EndDate { get; }

        public PrimeMinister(int num, string name, string sd, string ed)
        {
            Num = num;
            Name = name;
            StartDate = sd;
            EndDate = ed;
        }
    }

    class Game
    {
        public static void StartGame(List<PrimeMinister> pm, Player player)
        {
            // For randomising which prime ministers to pick
            Random rn = new Random();
        }

        public static void Main(string[] args)
        {
            /* To get the information out of the .csv file.
             * Headers:
             * No   Prime Minister  Date of Birth   Start date of term  End date of term
             * The DoB of the PM is unimportant in the task so it will be skipped.
             */
            List<PrimeMinister> prime_ministers = new List<PrimeMinister>();

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
                            Convert.ToInt32(values[0]), // No
                            values[1], // Name
                            values[3], // Start date of term
                            values[4] // End date of term
                            ));
                    }
                }
                
                /*
                for (int i = 0; i < prime_ministers.Count; i++)
                {
                    var a = prime_ministers[i];
                    Console.WriteLine(@"{0}", a.Num);
                    Console.WriteLine(@"{0}", a.Name);
                    Console.WriteLine(@"{0}", a.StartDate);
                    Console.WriteLine(@"{0}", a.EndDate);
                }
                Console.ReadKey();
                */
            }
            
            // The player class to be used.
            var player = new Player();
            var date_option = player.DateOption;

        menu:
            Console.WriteLine("Prime Minister Game");
            Console.WriteLine("1) How to play");
            if (date_option)
                Console.WriteLine("2) Guess PM from date: ON");
            else
                Console.WriteLine("2) Guess PM from date: OFF");
            Console.WriteLine("3) Start game");
            Console.Write("Choose an option: ");
            string menu_answer = Console.ReadLine();
            int nma;
            
            try
            {
                nma = Convert.ToInt32(menu_answer);
            }
            catch (Exception e)
            {
                Console.WriteLine("That is not a menu option.");
                goto menu;
            }

            StartGame(prime_ministers, player);
            
        }
    }
}
