using System;
using System.Collections.Generic;

namespace JokeGenerator
{
    class Program
    {
        static List<string> results = new List<string>();
        static char key;
        static Tuple<string, string> name;
        private static string category;

        static JsonFeedProcessor myJsonFeedProcessor = new JsonFeedProcessor(
            new CategoryJsonFeedSource("https://api.chucknorris.io/jokes/categories"),
            new JokeJsonFeedSource("https://api.chucknorris.io"),
            new NameJsonFeedSource("https://www.names.privserv.com/api/?region=canada")
        );

        static void Main(string[] args)
        {
            
            // return new AutoRun(Assembly.GetCallingAssembly()).Execute(new String[] {"--labels=All"});
            
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
                            GetRandomJokes(n);
                            PrintResults();
                        }
                        else
                        {
                            Console.WriteLine("How many jokes do you want? (1-9)");
                            category = null;
                            int n = Int32.Parse(Console.ReadLine());
                            GetRandomJokes(n);
                            PrintResults();
                        }
                    }
                    name = null;
                }
            }
        }

        private static void InputNames()
        {
            
            Console.WriteLine("Please enter a name: ");
            string nameTmp = Console.ReadLine();
            string[] names = nameTmp.Split(' ');

            name = Tuple.Create(names[0], names[1]);
        }

        private static void PrintResults()
        {
            Console.WriteLine("Here are your results:");

            foreach (string i in results)
            {
                Console.WriteLine(i);
            }
        }
        
        private static void GetCategories()
        {
            results = myJsonFeedProcessor.GetCategories();
        }
        
        private static void GetNames()
        {
            dynamic result = myJsonFeedProcessor.GetNames();
            name = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }

        private static void GetRandomJokes(int n)
        {
            for (int i = 1; i < n; i++)
            {
                results = myJsonFeedProcessor.GetRandomJokes(name?.Item1, name?.Item2, category);
                PrintResults();
            }
        }
    }
}