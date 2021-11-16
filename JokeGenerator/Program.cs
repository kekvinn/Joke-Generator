using System;
using System.Collections.Generic;
// using NUnitLite;
// using System.Reflection;


namespace JokeGenerator
{
    internal static class Program
    {
        private static List<string> _results = new();
        private static char _key;
        private static Tuple<string, string> _name;
        private static string _category;

        private static readonly JsonFeedProcessor MyJsonFeedProcessor = new JsonFeedProcessor(
            new CategoryJsonFeedSource("https://api.chucknorris.io/jokes/categories"),
            new JokeJsonFeedSource("https://api.chucknorris.io"),
            new NameJsonFeedSource("https://www.names.privserv.com/api/?region=canada")
        );

        private static void Main()
        {
            // return new AutoRun(Assembly.GetCallingAssembly()).Execute(new String[] {"--labels=All"});

            Console.WriteLine("Welcome to the Chuck Norris joke generator! To get started, enter ? to get instructions.");

            if (Console.ReadLine() != "?") 
                return;
            
            while (true)
            {
                Console.WriteLine("Press c to get categories");
                Console.WriteLine("Press r to get random jokes");
                _key = char.Parse(Console.ReadLine() ?? string.Empty);

                if (_key == 'c')
                {
                    GetCategories();
                    PrintResults();
                }

                if (_key == 'r')
                {
                    Console.WriteLine("Want to use a random name? y/n");
                    _key = char.Parse(Console.ReadLine() ?? string.Empty);

                    switch (_key)
                    {
                        case 'y':
                            GetNames();
                            break;
                        case 'n':
                            InputNames();
                            break;
                    }

                    Console.WriteLine("Want to specify a category? y/n");
                    _key = char.Parse(Console.ReadLine() ?? string.Empty);

                    if (_key == 'y')
                    {
                        Console.WriteLine("Enter a category:");
                        _category = Console.ReadLine();
                        Console.WriteLine("How many jokes do you want? (1-9)");
                        var n = int.Parse(Console.ReadLine() ?? string.Empty);
                        GetRandomJokes(n);
                    }
                    else
                    {
                        Console.WriteLine("How many jokes do you want? (1-9)");
                        _category = null;
                        var n = int.Parse(Console.ReadLine() ?? string.Empty);
                        GetRandomJokes(n);
                    }
                }
                _name = null;
            }
        }

        private static void InputNames()
        {
            Console.WriteLine("Please enter a name: ");
            var names = Console.ReadLine()?.Split(' ');

            _name = Tuple.Create(names?[0], names?[1]);
        }

        private static void PrintResults()
        {
            foreach (var i in _results)
            {
                Console.WriteLine(i);
            }
        }
        
        private static void GetCategories()
        {
            _results = MyJsonFeedProcessor.GetCategories();
        }
        
        private static void GetNames()
        {
            var result = MyJsonFeedProcessor.GetNames();
            _name = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }

        private static void GetRandomJokes(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _results = MyJsonFeedProcessor.GetRandomJokes(_name?.Item1, _name?.Item2, _category);
                PrintResults();
            }
        }
    }
}