using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using NFT_Trade.helpingClasses;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFT_Trade.Controllers
{
    //this controller is not in use. it is just for test opensea API's
    public class NFTController : Controller
    {
        // GET: Retrieving assets
        public Root Index()
        {
            var client = new RestClient("https://api.opensea.io/api/v1/assets?order_direction=desc&offset=0&limit=1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            var vas = JsonConvert.DeserializeObject<Root>(response.Content.ToString());
            return vas;
        }

        // GET: Retrieving assets
        public Root Asset()
        {
            var client = new RestClient("https://api.opensea.io/api/v1/assets?order_direction=desc&offset=0&limit=1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content.ToString()); 
            //var vas = JsonConvert.DeserializeObject<Root>(response.Content.ToString());
            return myDeserializedClass;
        }

        //
        public IRestResponse auction()
        {
            var client = new RestClient("https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=20");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            return response;
        }

    }
}