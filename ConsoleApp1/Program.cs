using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// Variables
        /// </summary>
        const int JOKES_NUM_MIN = 1;
        const int JOKES_NUM_MAX = 9;
        static string key;
        static List<string> results = new List<string>();
        static Tuple<string, string> names;
        static JsonFeed jsonFeed = new JsonFeed();

        /// <summary>
        /// Main entry point of program
        /// </summary>
        /// <param name="args">Program arguments</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Press ? to get instructions.");
            key = Console.ReadLine();

            if (key == "?")
            {
                while (true)
                {
                    Console.WriteLine("Press c to get categories");
                    Console.WriteLine("Press r to get random jokes");
                    key = Console.ReadLine();

                    if (key.ToLower() == "c")
                    {
                        GetCategories();
                        PrintResults();
                    }
                    else if (key.ToLower() == "r")
                    {

                        do
                        {
                            Console.WriteLine("Want to use a random name? y/n");
                            key = Console.ReadLine();
                            if (key.ToLower() == "y")
                            {
                                GetNames();
                            }
                        } while (key.ToLower() != "y" && key.ToLower() != "n");

                        String category = null;
                        do
                        {
                            Console.WriteLine("Want to specify a category? y/n");
                            key = Console.ReadLine();
                            if (key.ToLower() == "y")
                            {
                                Console.WriteLine("Enter a category:");
                                category = Console.ReadLine();
                            }
                        } while (key.ToLower() != "y" && key.ToLower() != "n");

                        bool isOkay;
                        int n;
                        do
                        {
                            Console.WriteLine("How many jokes do you want? (1-9)");
                            isOkay = Int32.TryParse(Console.ReadLine(), out n);
                        } while (isOkay == false || n < JOKES_NUM_MIN || n > JOKES_NUM_MAX);

                        GetRandomJokes(category, n);
                        PrintResults();
                    }
                }
            }

        }

        /// <summary>
        /// Outputs results from web api requests in an uniform format
        /// </summary>
        private static void PrintResults()
        {
            Console.WriteLine("[" + string.Join(",", results) + "]");
        }

        /// <summary>
        /// Requests random jokes
        /// </summary>
        /// <param name="category">Category to filter requested jokes if provided</param>
        /// <param name="number">Number to limit amount of requested jokes if provided</param>
        private static void GetRandomJokes(string category, int number)
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < number; i++)
            {
                dynamic result = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
                temp.Add((string)result);
            }
            results = temp;
        }

        /// <summary>
        /// Requests joke categories
        /// </summary>
        private static void GetCategories()
        {
            dynamic result = JsonFeed.GetCategories();
            List<string> temp = new List<string>();
            foreach (string category in result)
            {
                temp.Add(category);
            }
            results = temp;
        }

        /// <summary>
        /// Requests fake name and fake surname
        /// </summary>
        private static void GetNames()
        {
            dynamic result = JsonFeed.GetNames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}