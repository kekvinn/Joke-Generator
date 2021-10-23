using System;

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