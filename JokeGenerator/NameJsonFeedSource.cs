using System;
using System.Net.Http;


namespace JokeGenerator
{
    public class NameJsonFeedSource : IJsonFeedSource
    {
        private readonly string _url;

        public NameJsonFeedSource(string url)
        {
            _url = url;
        }

        public string GetJsonString()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            var result = client.GetStringAsync("").Result;
            return result;
        }

        public void SetOption(string option)
        {
            throw new NotImplementedException();
        }
    }
}