using System;

namespace JokeGenerator
{
    public class JsonFeed
    {
        private static CategoryJsonFeed _category;
        private static JokeJsonFeed _joke;
        private static NameJsonFeed _name;

        public JsonFeed(CategoryJsonFeed category, JokeJsonFeed joke, NameJsonFeed name)
        {
            _category = category;
            _joke = joke;
            _name = name;
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