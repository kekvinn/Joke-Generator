using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace JokeGenerator
{
    public class JokeJsonFeedSource : IJsonFeedSource
    {
        private readonly string _url;
        private string _category;

        public JokeJsonFeedSource(string url)
        {
            _url = url;
            _category = null;
        }

        public string GetJsonString()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            var url = "jokes/random";

            if (_category == null) 
                return Task.FromResult(client.GetStringAsync(url).Result).Result;
            
            url += "?category=";
            url += _category;

            return Task.FromResult(client.GetStringAsync(url).Result).Result;
        }

        public void SetOption(string option)
        {
            _category = option;
        }
    }
}