using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class CategoryJsonFeed : IJsonFeedSource
    {
        private string _url;

        public CategoryJsonFeed(string url)
        {
            _url = url;
        }

        public string[] GetJsonString()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);

            return new string[] {Task.FromResult(client.GetStringAsync("categories").Result).Result};
        }
    }
}