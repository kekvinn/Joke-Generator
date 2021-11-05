using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class NameJsonFeedSource : IJsonFeedSource
    {
        private string _url;

        public NameJsonFeedSource(string url)
        {
            _url = url;
        }

        public string GetJsonString()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            var result = client.GetStringAsync("").Result;
            return result;
        }

        public void SetOption(String option)
        {
            throw new NotImplementedException();
        }
    }
}