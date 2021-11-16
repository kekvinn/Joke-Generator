using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace JokeGenerator
{
    public class CategoryJsonFeedSource : IJsonFeedSource
    {
        private readonly string _url;

        public CategoryJsonFeedSource(string url)
        {
            _url = url;
        }

        public string GetJsonString()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            return Task.FromResult(client.GetStringAsync("categories").Result).Result;
        }

        public void SetOption(string option)
        {
            throw new NotImplementedException();
        }
    }
}