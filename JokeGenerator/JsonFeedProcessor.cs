using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class JsonFeedProcessor
    {
        private IJsonFeedSource _category;
        private IJsonFeedSource _joke;
        private IJsonFeedSource _name;

        public JsonFeedProcessor(IJsonFeedSource category, IJsonFeedSource joke, IJsonFeedSource name)
        {
            _category = category;
            _joke = joke;
            _name = name;
        }

        public List<string> GetCategories()
        {
            return new List<string>(new string []{_category.GetJsonString()});
        }

        public dynamic GetNames()
        {
            return JsonConvert.DeserializeObject<dynamic>(_name.GetJsonString());
        }

        public List<string> GetRandomJokes(string firstName, string lastName, string category)
        {
            // fix ReplaceName feature
            // somehow just return the value; not the whole JsonString
            
            
            
            _joke.SetOption(category);
            string joke = _joke.GetJsonString();
            // joke = ReplaceName(joke, firstName, lastName);
            
            return new List<string>(new string []{joke});
        }
        
        private static string ReplaceName(string joke, string firstName, string lastName)
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