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
        static string[] results = new string[50];
        static char key;
        static Tuple<string, string> names;
        private static JsonFeed myJsonFeed;

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
                            GetRandomJokes(Console.ReadLine(), n);
                            PrintResults();
                        }
                        else
                        {
                            Console.WriteLine("How many jokes do you want? (1-9)");
                            int n = Int32.Parse(Console.ReadLine());
                            GetRandomJokes(null, n);
                            PrintResults();
                        }
                    }
                    names = null;
                }
            }

        }
        
        private static void PrintResults()
        {
            Console.WriteLine("[" + string.Join(", ", results) + "]");
        }

        private static void GetCategories()
        {
            myJsonFeed = new JsonFeed("https://api.chucknorris.io", 0);
            results = myJsonFeed.GetCategories();
        }
        
        private static void GetNames()
        {
            myJsonFeed = new JsonFeed("https://www.names.privserv.com/api/", 0);
            dynamic result = myJsonFeed.GetNames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
        
        private static void GetRandomJokes(string category, int number)
        {
            myJsonFeed = new JsonFeed("https://api.chucknorris.io", number);
            results = myJsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
        }

        
    }
}