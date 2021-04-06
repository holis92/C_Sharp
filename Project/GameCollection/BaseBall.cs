using System;
using System.Collections.Generic;
using System.Text;

namespace GameCollection
{
    class BaseBall
    {
        public enum LEVEL
        {
            Easy,
            Normal,
            Hard
        }
        public enum MODE
        {
            Solo,
            vsAI
        }
        public LEVEL level { get; private set; }
        public MODE mode { get; private set; }
        public int AInumber { get; private set; }
        public List<Tuple<int, string>> AItry;
        public int MYnumber { get; private set; }
        public List<Tuple<int, string>> Mytry;

        public int Chances { get; private set; }

        public int Lobby()
        {
            string menu = null;
            while(menu != "1" && menu != "3")
            {
                Console.WriteLine("### WELCOME TO BASEBALL GAME ###");
                Console.WriteLine("### 1. Start                 ###");
                Console.WriteLine("### 2. How To Play           ###");
                Console.WriteLine("### 3. Exit                  ###");
                Console.Write    ("### -> ");
                menu = Console.ReadLine();
                if (menu == "2") HowToPlay();
            }

            return Convert.ToInt32(menu);
        }
        public void HowToPlay()
        {
            Console.WriteLine("### WELCOME TO BASEBALL GAME ###");
            Console.WriteLine("BASEBALL GAME is 3 numbers guessing game. Your opposite will have a set of 3 numbers.");
            Console.WriteLine("And you guess the number within the given chances according to level. If you could");
            Console.WriteLine("guess the number, you win. If not within the given chances, you lose. when you make");
            Console.WriteLine("a guess, you will get a result of the guess from your opposite as a strike and ball form.");
            Console.WriteLine("If you have the same number at the same spot of the answer set, You get 1 strike.");
            Console.WriteLine("If you have the same number but at a different spot of the answer set, You get 1 ball.");
            Console.WriteLine("If you have nothing matched, You get OUT.");
            Console.WriteLine("Here's an example.");
            Console.WriteLine("Answer: 396");
            Console.WriteLine("When you guessed 697,");
            Console.WriteLine("You have matched 9 at the same spot -> 1 strike");
            Console.WriteLine("You have matched 6 but at a different spot -> 1 ball");
            Console.WriteLine("7 you have matched is not anywhere -> nothing");
            Console.WriteLine("The result is 1S1B, which means 1 strike 1 ball");
            Console.WriteLine("Again,");
            Console.WriteLine("When you guess 518,");
            Console.WriteLine("You have no number matching the answer number -> OUT");
            Console.WriteLine();
            Console.WriteLine("Press Enter key to go back");
            Console.ReadLine();
            Console.Clear();
        }
        public void Initialize()
        {
            Console.Clear(); // Get ready for the Console to run the game
            string _mode = null;
            while (_mode != "1" && _mode != "2")
            {
                Console.WriteLine("1. Solo");
                Console.WriteLine("2. vs AI");
                Console.Write("Select a mode: ");
                _mode = Console.ReadLine();
                Console.Clear();
            }

            if (_mode == "1") 
                mode = MODE.Solo;
            else 
                mode = MODE.vsAI;

            string lev = null;
            while (lev != "1" && lev != "2" && lev != "3")
            {
                Console.WriteLine("1. Easy");
                Console.WriteLine("2. Normal");
                Console.WriteLine("3. Hard");
                Console.Write("Select a level: ");
                lev = Console.ReadLine();
                Console.Clear();
            }

            if (lev == "1")
                level = LEVEL.Easy;
            else if (lev == "2")
                level = LEVEL.Normal;
            else
                level = LEVEL.Hard;

            switch(level)
            {
                case LEVEL.Easy:
                    Chances = 12;
                    break;
                case LEVEL.Normal:
                    Chances = 9;
                    break;
                case LEVEL.Hard:
                    Chances = 6;
                    break;
            }

            AInumber = GenerateNumber();
            if (mode == MODE.vsAI) MYnumber = GenerateNumber();
        }

        public int GenerateNumber()
        {
            int first = 0, second = 0, third = 0;
            Random rand = new Random();
            // first digit
            first = rand.Next(1, 10);

            // second digit
            second = rand.Next(1, 10);
            while (second == first)
                second = rand.Next(1, 10);

            // third digit
            third = rand.Next(1, 10);
            while (third == first || third == second) 
                third = rand.Next(1, 10);

            return first + second * 10 + third * 100;
        }

        public void SoloPlay()
        {
            Mytry = new List<Tuple<int, string>>();

            while (Chances > 0)
            {
                int guess = 0;
                
                Console.Clear();
                GuessHistory();
                Console.WriteLine();
                
                guess = GuessInput();

                if (guess != AInumber)
                {
                    Mytry.Add(GetResultOfGuess(guess));
                }
                else
                {
                    Console.WriteLine("Correct! The answer was {0}", guess);
                    Console.ReadLine();
                    break;
                }

                Chances--;
                if(Chances == 0)
                {
                    Console.WriteLine("You are out of all chances ): The answer was {0}", guess);
                    Console.ReadLine();
                }
            }
            Console.Clear();
        }

        public int GuessInput()
        {
            int num = 0;
            while (num == 0)
            {
                Console.Write($"Guess the number!: ");
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Only a set of 3 numbers is valid.");
                    Console.ReadLine();
                    //Console.Clear();
                    continue;
                }

                if (num < 99 || num > 999)
                {
                    Console.WriteLine("Must be a set of 3 numbers.");
                    Console.ReadLine();
                    //Console.Clear();
                    num = 0;
                    continue;
                }

                int _first = num % 10;
                int _third = num / 100;
                int _second = (num - (_third * 100)) / 10;
                if (_first == _second || _first == _third || _second == _third)
                {
                    Console.WriteLine("The each number must be a different number.");
                    Console.ReadLine();
                    //Console.Clear();
                    num = 0;
                    continue;
                }
            }

            return num;
        }
        public Tuple<int, string> GetResultOfGuess(int guess)
        {
            int my_first = guess % 10;
            int my_third = guess / 100;
            int my_second = (guess - (my_third * 100)) / 10;

            int a_first = AInumber % 10;
            int a_third = AInumber / 100;
            int a_second = (AInumber - (a_third * 100)) / 10;

            int strike = 0, ball = 0;
            if (my_first == a_first) strike++;
            if (my_third == a_third) strike++;
            if (my_second == a_second) strike++;

            if (my_first == a_third || my_first == a_second) ball++;
            if (my_second == a_first || my_second == a_third) ball++;
            if (my_third == a_first || my_third == a_second) ball++;

            if(strike == 0 && ball == 0)
                return Tuple.Create(guess, "OUT ");
            else
                return Tuple.Create(guess, $"{strike}S{ball}B");
        }

        public void GuessHistory()
        {
            
            Console.WriteLine("# # # Guess History # # #");
            if(Mytry.Count == 0)
                Console.WriteLine("#   Result shown here   #");
            else
            {
                Console.WriteLine("# == Number = Result == #");
                Mytry.ForEach(guess => Console.WriteLine($"#     {guess.Item1}      {guess.Item2}     #"));
            }

            Console.WriteLine("# # # # # # # # # # # # #");
        }
    }
}
