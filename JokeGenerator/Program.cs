using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnitLite;

namespace JokeGenerator
{
    class Program
    {
        static List<string> results = new List<string>();
        static char key;
        static Tuple<string, string> name;
        private static string category;

        
        private static JsonFeedProcessor myJsonFeedProcessor = new(
            new CategoryJsonFeedSource("https://api.chucknorris.io/jokes/categories"),
            new JokeJsonFeedSource("https://api.chucknorris.io"),
            new NameJsonFeedSource("https://www.names.privserv.com/api/?region=canada")
        );

        static int Main(string[] args)
        {
            return new AutoRun(Assembly.GetCallingAssembly()).Execute(new String[] {"--labels=All"});
            
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
                        // GetCategories();
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
                            InputNames();
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
                    name = null;
                }
            }
        }

        private static void InputNames()
        {
            
            Console.WriteLine("Please enter the name: ");
            string nameTmp = Console.ReadLine();

            string firstName = nameTmp.Substring(0, nameTmp.IndexOf(' '));
            string lastName = nameTmp.Substring(nameTmp.IndexOf(' ') + 1, nameTmp.Length - 1);
            
            Console.WriteLine(firstName);
            Console.WriteLine(lastName);
        }

        private static void PrintResults()
        {
            Console.WriteLine("Here are your results:");

            foreach (string i in results)
            {
                Console.WriteLine(i);
            }
            // Console.WriteLine("[" + string.Join(",\n ", results) + "]\n");
        }

        
        private void GetCategories()
        {
            results = JsonFeedProcessor.GetCategories();
        }
        

        private static void GetNames()
        {
            dynamic result = JsonFeedProcessor.GetNames();
            name = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }

        private static void GetRandomJokes()
        {
            results = JsonFeedProcessor.GetRandomJokes(name?.Item1, name?.Item2, category);
        }
    }
}