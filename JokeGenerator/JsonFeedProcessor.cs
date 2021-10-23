using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class JsonFeedProcessor
    {
        private static CategoryJsonFeedSource _category;
        private static JokeJsonFeedSource _joke;
        private static NameJsonFeedSource _name;

        public JsonFeedProcessor(CategoryJsonFeedSource category, JokeJsonFeedSource joke, NameJsonFeedSource name)
        {
            _category = category;
            _joke = joke;
            _name = name;
        }

        public static List<string> GetCategories()
        {
            return new List<string>(new string []{_category.GetJsonString()});
        }

        public dynamic GetNames()
        {
            return JsonConvert.DeserializeObject<dynamic>(_name.GetJsonString());
        }

        public List<string> GetRandomJokes( string firstName, string lastName, string category, int count = 1)
        {
            _joke.SetCategory(category);
            
            var jokeList = new List<string>();
            for (var x = 0; x < count; x++)
            {
                var tmp = JsonConvert.DeserializeObject<dynamic>(ReplaceName(_joke.GetJsonString(), firstName, lastName)).value;
                jokeList.Add( tmp.ToString() );
            }

            return jokeList;
        }
        
        private string ReplaceName(string joke, string firstName, string lastName)
        {
            if (firstName != null && lastName != null)
            {
                int index = joke.IndexOf("Chuck Norris");
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + "Chuck Norris".Length,
                    joke.Length - (index + "Chuck Norris".Length));
                joke = firstPart + " " + firstName + " " + lastName + secondPart;
            }

            return joke;
        }

        
    }
}