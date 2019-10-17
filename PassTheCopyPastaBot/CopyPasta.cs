using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassTheCopyPastaBot
{
    class CopyPasta
    {
        private IRestClient client;

        public int CharacterLimit { get; set; }

        public CopyPasta()
        {
            client = new RestClient("https://www.reddit.com");
        }


        public ChildData GetRandomPasta(PostFilter postFilter = PostFilter.TOP)
        {
            return GetPosts(postFilter, 100).Data.Children
                .AsEnumerable()
                .Select(c => c.Data)
                .Where(d => d.Selftext.Length > 0 && d.Selftext.Length <= CharacterLimit)
                .RandomShuffle()
                .FirstOrDefault();
        }

        public Posts GetPosts(PostFilter postFilter = PostFilter.TOP, int limit = 100)
        {
            var request = new RestRequest("r/copypasta/" + postFilter.GetString() + ".json", Method.GET);
            request.AddQueryParameter("limit", limit.ToString());
            request.AddQueryParameter("t", "all");

            var response = client.Execute(request);

            return Posts.FromJson(response.Content);
        }
    }
}
