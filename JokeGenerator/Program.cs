using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    class Program
    {
        static string[] results;
        static char key;
        static Tuple<string, string> names;
        private static string category;

        private static CategoryJsonFeedSource _categoryJsonFeedSource = new("https://api.chucknorris.io");
        private static NameJsonFeedSource _nameJsonFeedSource = new("https://www.names.privserv.com/api/");

        private static JokeJsonFeedSource _jokeJsonFeedSource = new("https://api.chucknorris.io");

        private static JsonFeedProcessor _myJsonFeedProcessor = new(_categoryJsonFeedSource, _jokeJsonFeedSource, _nameJsonFeedSource);


        static void Main(string[] args)
        {
            Console.WriteLine("Press ? to get instructions.");

            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    Console.WriteLine("Press c to get categories");
                    Console.WriteLine("Press r to get random jokes");
                    key = Char.Parse(Console.ReadLine());

                    if (key == 'c')
                    {
                        GetCategories();
                        PrintResults();
                    }

                    if (key == 'r')
                    {
                        Console.WriteLine("Want to use a random name? y/n");
                        key = Char.Parse(Console.ReadLine());

                        if (key == 'y')
                        {
                            GetNames();
                        }
                        else if (key == 'n')
                        {
                            // ask user to enter name???
                        }

                        Console.WriteLine("Want to specify a category? y/n");
                        key = Char.Parse(Console.ReadLine());

                        if (key == 'y')
                        {
                            Console.WriteLine("How many jokes do you want? (1-9)");
                            int n = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter a category:");
                            category = Console.ReadLine();
                            GetRandomJokes();
                            PrintResults();
                        }
                        else
                        {
                            Console.WriteLine("How many jokes do you want? (1-9)");
                            int n = Int32.Parse(Console.ReadLine());
                            GetRandomJokes();
                            PrintResults();
                        }
                    }

                    names = null;
                }
            }
        }

        private static void PrintResults()
        {
            Console.WriteLine("Here are your results:");
            foreach (string item in results)
            {
                Console.WriteLine(item);
            }
        }

        private static void GetCategories()
        {
            results = _myJsonFeedProcessor.GetCategories().ToArray();
        }

        private static void GetNames()
        {
            dynamic result = _myJsonFeedProcessor.GetNames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }

        private static void GetRandomJokes()
        {
            results = _myJsonFeedProcessor.GetRandomJokes(names.Item1, names.Item2, category).ToArray();
        }
    }
}