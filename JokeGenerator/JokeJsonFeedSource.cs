using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class JokeJsonFeedSource : IJsonFeedSource
    {
        private string _url;
        private string _category;

        public JokeJsonFeedSource(string url)
        {
            _url = url;
            _category = null;
        }

        public string GetJsonString()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            string url = "jokes/random";
            if (_category != null)
            {
                if (url.Contains('?'))
                    url += "&";
                else
                    url += "?";

                url += "category=";
                url += _category;
            }

            return Task.FromResult(client.GetStringAsync(url).Result).Result;
        }
        

        public void SetOption(string option)
        {
            _category = option;
        }
    }
}