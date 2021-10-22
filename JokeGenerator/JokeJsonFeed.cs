using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class JokeJsonFeed : IJsonFeedSource
    {
        private string _url;
        private string _firstname;
        private string _lastname;
        private string _category;

        public JokeJsonFeed(string url, string firstname, string lastname, string category)
        {
            _url = url;
            _firstname = firstname;
            _lastname = lastname;
            _category = category;
        }

        public string[] GetJsonString()
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

            string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;
            joke = ReplaceName(joke);

            return new string[] {JsonConvert.DeserializeObject<dynamic>(joke).value};
        }

        public string ReplaceName(string joke)
        {
            if (_firstname != null && _lastname != null)
            {
                int index = joke.IndexOf("Chuck Norris");
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + "Chuck Norris".Length,
                    joke.Length - (index + "Chuck Norris".Length));
                joke = firstPart + " " + _firstname + " " + _lastname + secondPart;
            }

            return joke;
        }
    }
}