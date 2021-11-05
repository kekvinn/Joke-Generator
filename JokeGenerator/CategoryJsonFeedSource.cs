using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class CategoryJsonFeedSource : IJsonFeedSource
    {
        private string _url;

        public CategoryJsonFeedSource(string url)
        {
            _url = url;
        }

        public string GetJsonString()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            return Task.FromResult(client.GetStringAsync("categories").Result).Result;
        }

        public void SetOption(string option)
        {
            throw new NotImplementedException();
        }
    }
}