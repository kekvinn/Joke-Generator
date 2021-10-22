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
            _category = new CategoryJsonFeed(_url);
            return _category.GetJsonString();
        }
        
        public string[] GetRandomJokes(string firstname, string lastname, string category)
        {
            _joke = new JokeJsonFeed(_url, firstname, lastname, category);
            return _joke.GetJsonString();
        }
        
        public dynamic GetNames()
        {
            _name = new NameJsonFeed(_url);
            return _name.GetJsonString();
        }

    }
}