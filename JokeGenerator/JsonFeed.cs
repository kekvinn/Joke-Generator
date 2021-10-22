using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class JsonFeed
    {
        static string _url = "";
        private static CategoryJsonFeed _category;
        private static JokeJsonFeed _joke;
        private static NameJsonFeed _name;

        public JsonFeed(CategoryJsonFeed category, JokeJsonFeed joke, NameJsonFeed name)
        {
            _category = category;
            _joke = joke;
            _name = name;
        }
        
        public JsonFeed(string endpoint, int results)
        {
            _url = endpoint;
        }

        public string[] GetCategories()
        {
            return _category.GetJsonString();
        }

        public dynamic GetNames()
        {
            return _name.GetJsonString();
        }
        
        public string[] GetRandomJokes()
        {
            return _joke.GetJsonString();
        }

    }
}