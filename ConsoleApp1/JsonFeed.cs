using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class JsonFeed
    {
        /// <summary>
        /// Constants and variables
        /// </summary>
        const string JOKES_URL = "https://api.chucknorris.io/jokes/";
        const string NAMES_URL = "http://uinames.com/api/";
        static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public JsonFeed() { }

        /// <summary>
        /// Obtaines a joke from from chucknorris.io web api
        /// </summary>
        /// <param name="firstname">Name to replace Chuck if provided</param>
        /// <param name="lastname">Surname to replace Norris if provided</param>
        /// <param name="category">Category to filter the joke if provided</param>
        /// <returns></returns>
        public static dynamic GetRandomJokes(string firstname, string lastname, string category)
        {
            string url = JOKES_URL + "random";
            if (category != null)
            {
                if (url.Contains('?'))
                {
                    url += "&";
                }
                else
                {
                    url += "?";
                }
                url += "category=";
                url += category;
            }

            string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

            if (firstname != null && lastname != null)
            {
                int index = joke.IndexOf("Chuck Norris");
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                joke = firstPart + " " + firstname + " " + lastname + secondPart;
            }

            return JsonConvert.DeserializeObject<dynamic>(joke).value;
        }

        /// <summary>
        /// Obtaines a fake name and a fake surname from uinames.com web api
        /// </summary>
        /// <returns>JSON object containing the fake names</returns>
		public static dynamic GetNames()
        {
            string result = Task.FromResult(client.GetStringAsync(NAMES_URL).Result).Result;
            return JsonConvert.DeserializeObject<dynamic>(result);
        }

        /// <summary>
        /// Obtains categories from chucknorris.io web api
        /// </summary>
        /// <returns>JSON object containing all categories</returns>
        public static dynamic GetCategories()
        {
            string result = Task.FromResult(client.GetStringAsync(JOKES_URL + "categories").Result).Result;
            return JsonConvert.DeserializeObject<dynamic>(result);
        }
    }
}