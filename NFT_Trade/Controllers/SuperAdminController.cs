using Newtonsoft.Json;
using NFT_Trade.BL;
using NFT_Trade.helpingClasses;
using NFT_Trade.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using HtmlAgilityPack;

namespace NFT_Trade.Controllers

{
    public class SuperAdminController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        GeneralPurpose gp = new GeneralPurpose();

        private bool isLogedIn()
        {
            if (gp.validateUser() != null)
            {
                if (gp.validateUser().Role == 1)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        //public ActionResult CoinGecko()
        //{
        //    var client = new RestClient("https://api.opensea.io/api/v1/assets?order_direction=desc&offset=0&limit=10");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Accept", "application/json");
        //    IRestResponse response = client.Execute(request);
        //    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content.ToString());
        //    ViewBag.Asset = myDeserializedClass.assets.ToList();

        //    var Auctionclient = new RestClient("https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=10&event_type=created");
        //    var Auctionrequest = new RestRequest(Method.GET);
        //    Auctionrequest.AddHeader("Accept", "application/json");
        //    IRestResponse Auctionresponse = Auctionclient.Execute(Auctionrequest);
        //    Auction myDeserializedAuction = JsonConvert.DeserializeObject<Auction>(Auctionresponse.Content.ToString());
        //    ViewBag.Auction = myDeserializedAuction.asset_events.ToList().ToPagedList(1,10);
        //    return View();
        //}

        //public ActionResult CoinGecko(string msg = "", string color = "", string time = "24h", string currency = "ETH", string Owner = "Owners", string Asset = "Assets", string Chain = "Chain", string Verify = "All")
        //{
        //    //       Retrieving events 
        //    //       curl--request GET \
        //    //       --url 'https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=20' \
        //    //       --header 'Accept: application/json'
        //    try
        //    {
        //        //var Auctionclient = new RestClient("https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=200");
        //        var Auctionclient = new RestClient("https://testnets-api.opensea.io/events?only_opensea=false&offset=0&limit=200");
        //        var Auctionrequest = new RestRequest(Method.GET);
        //        Auctionrequest.AddHeader("Accept", "application/json");
        //        Auctionrequest.AddHeader("X_API_KEY", "0"); // new line add
        //        IRestResponse Auctionresponse = Auctionclient.Execute(Auctionrequest);
        //        Auction myDeserializedAuction = JsonConvert.DeserializeObject<Auction>(Auctionresponse.Content.ToString());
        //        var auction = myDeserializedAuction.asset_events.ToList();
        //        ViewBag.Auction = auction;

        //        //       Retrieving a single asset
        //        //       curl--request GET \
        //        //       --url https://api.opensea.io/api/v1/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/5/
        //        //int count = 0;
        //        List<SingleAsset> sg = new List<SingleAsset>();
        //        foreach (var list in auction)
        //        {
        //            try
        //            {
        //                if (list.asset.asset_contract.address == null || list.asset.token_id == null)
        //                    continue;
        //                var asset_contract_address = list.asset.asset_contract.address;
        //                var token_id = list.asset.token_id;
        //                var url = "https://api.opensea.io/api/v1/asset/" + asset_contract_address + "/" + token_id + "/";
        //                var SingleAssetclient = new RestClient(url);  //https://api.opensea.io/api/v1/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/5/
        //                var SingleAssetrequest = new RestRequest(Method.GET);
        //                SingleAssetrequest.AddHeader("Accept", "application/json");
        //                IRestResponse SingleAssetresponse = SingleAssetclient.Execute(SingleAssetrequest);
        //                SingleAsset myDeserializedSingleAsset = JsonConvert.DeserializeObject<SingleAsset>(SingleAssetresponse.Content.ToString());
        //                if (myDeserializedSingleAsset.id != 0)
        //                    sg.Add(myDeserializedSingleAsset);
        //            }
        //            catch
        //            {
        //                continue;
        //            }
        //            if (sg.Count == 5) //100 
        //                break;
        //        }

        //        string chain = currency;
        //        //   Filteration Section
        //        if (Owner != "Owners")
        //        {
        //            if (Owner == "<500")
        //            {
        //                sg = sg.Where(x => x.collection.stats.num_owners < 500).ToList();
        //            }
        //            if (Owner == "500-1000")
        //            {
        //                sg = sg.Where(x => x.collection.stats.num_owners >= 500 && x.collection.stats.num_owners <= 1000).ToList();
        //            }
        //            if (Owner == "1000-5000")
        //            {
        //                sg = sg.Where(x => x.collection.stats.num_owners >= 1000 && x.collection.stats.num_owners <= 5000).ToList();
        //            }
        //            if (Owner == "5000-50000")
        //            {
        //                sg = sg.Where(x => x.collection.stats.num_owners >= 5000 && x.collection.stats.num_owners <= 50000).ToList();
        //            }
        //            if (Owner == "50000+")
        //            {
        //                sg = sg.Where(x => x.collection.stats.num_owners >= 50000).ToList();
        //            }
        //        }
        //        if (Asset != "Assets")
        //        {
        //            if (Asset == "<500")
        //            {
        //                sg = sg.Where(x => x.collection.stats.count < 500).ToList();
        //            }
        //            if (Asset == "500-1000")
        //            {
        //                sg = sg.Where(x => x.collection.stats.count >= 500 && x.collection.stats.count <= 1000).ToList();
        //            }
        //            if (Asset == "1000-5000")
        //            {
        //                sg = sg.Where(x => x.collection.stats.count >= 1000 && x.collection.stats.count <= 5000).ToList();
        //            }
        //            if (Asset == "5000-50000")
        //            {
        //                sg = sg.Where(x => x.collection.stats.count >= 5000 && x.collection.stats.count <= 50000).ToList();
        //            }
        //            if (Asset == "50000+")
        //            {
        //                sg = sg.Where(x => x.collection.stats.count >= 50000).ToList();
        //            }
        //        }
        //        if (Chain != "Chain")
        //        {
        //            if (Chain == "ETH") { chain = "ETH"; }
        //            if (Chain == "TBD") { chain = "TBD"; }
        //            if (Chain == "USD") { chain = "USD"; }
        //        }
        //        if (Verify != "All")
        //        {
        //            if (Verify == "Yes")
        //            {
        //                sg = sg.Where(x => x.owner.config != "").ToList();
        //            }
        //            if (Verify == "No")
        //            {
        //                sg = sg.Where(x => x.owner.config == "").ToList();
        //            }
        //        }

        //        var client = new RestClient("https://min-api.cryptocompare.com/data/price?fsym=ETH&tsyms=USD");
        //        var request = new RestRequest(Method.GET);
        //        request.AddHeader("Accept", "application/json");
        //        IRestResponse response = client.Execute(request);
        //        USD myDeserializedUSD = JsonConvert.DeserializeObject<USD>(response.Content.ToString());

        //        ViewBag.Volume = time;
        //        ViewBag.chain = chain;
        //        ViewBag.currencies = currency;
        //        ViewBag.Dollar = Convert.ToDouble(myDeserializedUSD.usd);
        //        ViewBag.SingleAsset = sg;
        //        ViewBag.msg = msg;
        //        ViewBag.color = color;
        //        return View();
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.SingleAsset = null;
        //        ViewBag.msg = msg;
        //        ViewBag.color = color;
        //        return View();
        //    }
        //}

        public ActionResult CoinGecko(string msg = "", string color = "", string time = "24h", string currency = "ETH",
            string Owner = "Owners", string Asset = "Assets", string Chain = "Chain", string Verify = "All")
        {
            //       Retrieving events 
            //       curl--request GET \
            //       --url 'https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=20' \
            //       --header 'Accept: application/json'
            RetColDB();
            try
            {
                var client =
                    new RestClient("https://api.opensea.io/api/v1/assets?order_direction=desc&offset=0&limit=50");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                RetrievingAssets myDeserializedAuction =
                    JsonConvert.DeserializeObject<RetrievingAssets>(response.Content.ToString());
                var auction = myDeserializedAuction.assets.ToList();
                ViewBag.Auction = auction;
                List<Roots> statlist = new List<Roots>();
                foreach (var list in auction)
                {
                    if (list.collection.slug == null)
                        continue;
                    var slug = list.collection.slug;
                    var url = "https://api.opensea.io/api/v1/collection/" + slug + "/" +
                              "stats"; //"https://api.opensea.io/api/v1/collection/the-cyclops-monkey-club/stats"
                    var collectionStatsClient = new RestClient(url);
                    var collectionStatsrequest = new RestRequest(Method.GET);
                    collectionStatsrequest.AddHeader("Accept", "application/json");
                    IRestResponse collectionStatsresponse = collectionStatsClient.Execute(collectionStatsrequest);
                    Roots myDeserializeStats =
                        JsonConvert.DeserializeObject<Roots>(collectionStatsresponse.Content.ToString());
                    var stats = myDeserializeStats;
                    statlist.Add(stats);
                }

                ViewBag.Stat = statlist;

                //       Retrieving a single asset
                //       curl--request GET \
                //       --url https://api.opensea.io/api/v1/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/5/
                //int count = 0;
                List<SingleAsset> sg = new List<SingleAsset>();
                //foreach (var list in auction)
                //{
                //    try
                //    {
                //        if (list.asset.asset_contract.address == null || list.asset.token_id == null)
                //            continue;
                //        var asset_contract_address = list.asset.asset_contract.address;
                //        var token_id = list.asset.token_id;
                //        var url = "https://api.opensea.io/api/v1/asset/" + asset_contract_address + "/" + token_id + "/";
                //        var SingleAssetclient = new RestClient(url);  //https://api.opensea.io/api/v1/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/5/
                //        var SingleAssetrequest = new RestRequest(Method.GET);
                //        SingleAssetrequest.AddHeader("Accept", "application/json");
                //        IRestResponse SingleAssetresponse = SingleAssetclient.Execute(SingleAssetrequest);
                //        SingleAsset myDeserializedSingleAsset = JsonConvert.DeserializeObject<SingleAsset>(SingleAssetresponse.Content.ToString());
                //        if (myDeserializedSingleAsset.id != 0)
                //            sg.Add(myDeserializedSingleAsset);
                //    }
                //    catch
                //    {
                //        continue;
                //    }
                //    if (sg.Count == 5) //100 
                //        break;
                //}

                string chain = currency;
                //   Filteration Section
                if (Owner != "Owners")
                {
                    if (Owner == "<500")
                    {
                        sg = sg.Where(x => x.collection.stats.num_owners < 500).ToList();
                    }

                    if (Owner == "500-1000")
                    {
                        sg = sg.Where(
                                x => x.collection.stats.num_owners >= 500 && x.collection.stats.num_owners <= 1000)
                            .ToList();
                    }

                    if (Owner == "1000-5000")
                    {
                        sg = sg.Where(x =>
                            x.collection.stats.num_owners >= 1000 && x.collection.stats.num_owners <= 5000).ToList();
                    }

                    if (Owner == "5000-50000")
                    {
                        sg = sg.Where(x =>
                            x.collection.stats.num_owners >= 5000 && x.collection.stats.num_owners <= 50000).ToList();
                    }

                    if (Owner == "50000+")
                    {
                        sg = sg.Where(x => x.collection.stats.num_owners >= 50000).ToList();
                    }
                }

                if (Asset != "Assets")
                {
                    if (Asset == "<500")
                    {
                        sg = sg.Where(x => x.collection.stats.count < 500).ToList();
                    }

                    if (Asset == "500-1000")
                    {
                        sg = sg.Where(x => x.collection.stats.count >= 500 && x.collection.stats.count <= 1000)
                            .ToList();
                    }

                    if (Asset == "1000-5000")
                    {
                        sg = sg.Where(x => x.collection.stats.count >= 1000 && x.collection.stats.count <= 5000)
                            .ToList();
                    }

                    if (Asset == "5000-50000")
                    {
                        sg = sg.Where(x => x.collection.stats.count >= 5000 && x.collection.stats.count <= 50000)
                            .ToList();
                    }

                    if (Asset == "50000+")
                    {
                        sg = sg.Where(x => x.collection.stats.count >= 50000).ToList();
                    }
                }

                if (Chain != "Chain")
                {
                    if (Chain == "ETH")
                    {
                        chain = "ETH";
                    }

                    if (Chain == "TBD")
                    {
                        chain = "TBD";
                    }

                    if (Chain == "USD")
                    {
                        chain = "USD";
                    }
                }

                if (Verify != "All")
                {
                    if (Verify == "Yes")
                    {
                        sg = sg.Where(x => x.owner.config != "").ToList();
                    }

                    if (Verify == "No")
                    {
                        sg = sg.Where(x => x.owner.config == "").ToList();
                    }
                }

                var USDclient = new RestClient("https://min-api.cryptocompare.com/data/price?fsym=ETH&tsyms=USD");
                var USDrequest = new RestRequest(Method.GET);
                USDrequest.AddHeader("Accept", "application/json");
                IRestResponse USDresponse = USDclient.Execute(USDrequest);
                USD myDeserializedUSD = JsonConvert.DeserializeObject<USD>(USDresponse.Content.ToString());

                ViewBag.Volume = time;
                ViewBag.chain = chain;
                ViewBag.currencies = currency;
                ViewBag.Dollar = Convert.ToDouble(myDeserializedUSD.usd);
                ViewBag.SingleAsset = sg;
                ViewBag.msg = msg;
                ViewBag.color = color;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.SingleAsset = null;
                ViewBag.msg = msg;
                ViewBag.color = color;
                return View();
            }
        }

        [HttpPost]
        public ActionResult FormFilter(string time = "24h", string currency = "ETH", string Owner = "Owners",
            string Asset = "Assets", string Chain = "Chain", string Verify = "ALL")
        {
            return RedirectToAction("CoinGecko", "SuperAdmin",
                new { time = time, currency = currency, Owner = Owner, Asset = Asset, Chain = Chain, Verify = Verify });
        }

        [HttpPost]
        public ActionResult GetSearchValues(string key)
        {
            List<string> collectionName = new List<string>();

            var client = new RestClient("https://api.opensea.io/api/v1/assets?order_direction=desc&offset=0");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response != null)
            {
                SearchValue myDeserializedClass =
                    JsonConvert.DeserializeObject<SearchValue>(response.Content.ToString());
                foreach (var CN in myDeserializedClass.assets)
                {
                    collectionName.Add(CN.name);
                }

                return Json(collectionName, JsonRequestBehavior.AllowGet);

                //if (collectionName != null && collectionName.Count() > 0)
                //{
                //    List<string> enumerable = collectionName.Where(x => x.ToLower().Contains(key.ToLower())).ToList();
                //    return Json(enumerable, JsonRequestBehavior.AllowGet);
                //}
            }
            //ViewBag.SearchValue = myDeserializedClass.assets.ToList();
            //string[] searchValues = { "Ava", "Amelia", "Abigail", "Alexander", "Aiden", "Anthony", "Aalam", "Aamir", "Aaren", "Abbie", "Abbot", "Abhay", "Abhiram", "Abira", "Abiir", "Abrahem", "Abra" };
            //searchValues = searchValues.Where(x => x.ToLower().Contains(key.ToLower())).ToArray();

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCoin()
        {

            var Auctionclient =
                new RestClient("https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=200");
            var Auctionrequest = new RestRequest(Method.GET);
            Auctionrequest.AddHeader("Accept", "application/json");
            IRestResponse Auctionresponse = Auctionclient.Execute(Auctionrequest);
            Auction myDeserializedAuction = JsonConvert.DeserializeObject<Auction>(Auctionresponse.Content.ToString());
            var auction = myDeserializedAuction.asset_events.ToList();

            List<SingleAsset> sg = new List<SingleAsset>();
            foreach (var list in auction)
            {
                try
                {
                    if (list.asset.asset_contract.address == null || list.asset.token_id == null)
                        continue;
                    var asset_contract_address = list.asset.asset_contract.address;
                    var token_id = list.asset.token_id;
                    var url = "https://api.opensea.io/api/v1/asset/" + asset_contract_address + "/" + token_id + "/";
                    var SingleAssetclient =
                        new RestClient(
                            url); //https://api.opensea.io/api/v1/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/5/
                    var SingleAssetrequest = new RestRequest(Method.GET);
                    SingleAssetrequest.AddHeader("Accept", "application/json");
                    IRestResponse SingleAssetresponse = SingleAssetclient.Execute(SingleAssetrequest);
                    SingleAsset myDeserializedSingleAsset =
                        JsonConvert.DeserializeObject<SingleAsset>(SingleAssetresponse.Content.ToString());
                    if (myDeserializedSingleAsset.id != 0)
                        sg.Add(myDeserializedSingleAsset);
                }
                catch
                {
                    continue;
                }

                if (sg.Count == 10) //100 
                    break;
            }

            return Json(sg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult postCoinGecko(string msg = "", string color = "")
        {
            //       Retrieving events 
            //       curl--request GET \
            //       --url 'https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=20' \
            //       --header 'Accept: application/json'
            try
            {
                var Auctionclient =
                    new RestClient("https://api.opensea.io/api/v1/events?only_opensea=false&offset=0&limit=200");
                var Auctionrequest = new RestRequest(Method.GET);
                Auctionrequest.AddHeader("Accept", "application/json");
                IRestResponse Auctionresponse = Auctionclient.Execute(Auctionrequest);
                Auction myDeserializedAuction =
                    JsonConvert.DeserializeObject<Auction>(Auctionresponse.Content.ToString());
                var auction = myDeserializedAuction.asset_events.ToList();
                ViewBag.Auction = auction;

                //       Retrieving a single asset
                //       curl--request GET \
                //       --url https://api.opensea.io/api/v1/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/5/
                //int count = 0;
                List<SingleAsset> sg = new List<SingleAsset>();
                foreach (var list in auction)
                {
                    try
                    {
                        if (list.asset.asset_contract.address == null || list.asset.token_id == null)
                            continue;
                        var asset_contract_address = list.asset.asset_contract.address;
                        var token_id = list.asset.token_id;
                        var url = "https://api.opensea.io/api/v1/asset/" + asset_contract_address + "/" + token_id +
                                  "/";
                        var SingleAssetclient =
                            new RestClient(
                                url); //https://api.opensea.io/api/v1/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/5/
                        var SingleAssetrequest = new RestRequest(Method.GET);
                        SingleAssetrequest.AddHeader("Accept", "application/json");
                        IRestResponse SingleAssetresponse = SingleAssetclient.Execute(SingleAssetrequest);
                        SingleAsset myDeserializedSingleAsset =
                            JsonConvert.DeserializeObject<SingleAsset>(SingleAssetresponse.Content.ToString());
                        if (myDeserializedSingleAsset.id != 0)
                            sg.Add(myDeserializedSingleAsset);
                    }
                    catch
                    {
                        continue;
                    }

                    if (sg.Count == 50) //100 
                        break;
                }

                ViewBag.SingleAsset = sg;
                ViewBag.msg = msg;
                ViewBag.color = color;
                return View();
            }
            catch
            {
                ViewBag.SingleAsset = null;
                ViewBag.msg = msg;
                ViewBag.color = color;
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        #region ManageUsers

        public ActionResult AddAdmin(string msg = "", string color = "")
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.msg = msg;
            ViewBag.color = color;
            return View();
        }

        [HttpPost]
        public ActionResult PostAddAdmin(User newUser)
        {
            if ((!isLogedIn()))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (newUser.Name == null || newUser.Email == null || newUser.Password == null || newUser.Address == null ||
                newUser.Contact == null)
            {
                return RedirectToAction("AddUser", new { msg = "Fill all the fieldds first", color = "red" });
            }

            User u = new UserBL().GetActiveUsersList(db).Where(x => x.Email.ToLower() == newUser.Email.ToLower())
                .FirstOrDefault();
            if (u != null)
            {
                return RedirectToAction("AddUser", new { msg = "Email already exist", color = "red" });
            }

            newUser.Password = StringCypher.Encrypt(newUser.Password);
            newUser.IsActive = 1;
            newUser.Role = 2;
            newUser.CreatedAt = DateTime.Now;
            if (new UserBL().AddUser(newUser, db))
            {
                return RedirectToAction("AddAdmin", new { msg = "Record Added successfully", color = "green" });
            }

            return RedirectToAction("AddAdmin", new { msg = "Record connot be added to database", color = "red" });
        }

        public ActionResult ViewAdmin(string msg = "", string color = "black")
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.msg = msg;
            ViewBag.color = color;
            return View();
        }

        [HttpPost]
        public ActionResult GetAdminList(string name = "", string contact = "", string email = "")
        {
            List<User> ulist = new UserBL().GetActiveUsersList(db).Where(x => x.Role == 2).OrderByDescending(x => x.Id)
                .ToList();
            if (name != "")
            {
                ulist = ulist.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            if (contact != "")
            {
                ulist = ulist.Where(x => x.Contact.ToLower().Contains(contact.ToLower())).ToList();
            }

            if (email != "")
            {
                ulist = ulist.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
            }

            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            if (sortColumnName != "" && sortColumnName != null)
            {
                if (sortDirection == "asc")
                {
                    ulist = ulist.OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                }
                else
                {
                    ulist = ulist.OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                }
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                ulist = ulist.Where(x => x.Name.ToLower().Contains(searchValue.ToLower()) ||
                                         x.Contact.ToLower().Contains(searchValue.ToLower()) ||
                                         x.Email.ToLower().Contains(searchValue.ToLower()) ||
                                         (x.Address != null && x.Address.ToLower().Contains(searchValue.ToLower())))
                    .ToList();
            }

            int totalrows = ulist.Count();
            int totalrowsafterfilterinig = ulist.Count();
            ulist = ulist.Skip(start).Take(length).ToList();

            List<User> udto = new List<User>();
            foreach (User u in ulist)
            {
                User obj = new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Contact = u.Contact,
                    Email = u.Email,
                    Address = u.Address
                };

                udto.Add(obj);
            }

            return Json(
                new
                {
                    data = udto,
                    draw = Request["draw"],
                    recordsTotal = totalrows,
                    recordsFiltered = totalrowsafterfilterinig
                }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PostUpdateAdmin(User _user)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            User u = new UserBL().GetActiveUserById(_user.Id, db);
            u.Name = _user.Name.Trim();
            u.Contact = _user.Contact;
            u.Address = _user.Address;

            bool chkUser = new UserBL().UpdateUser(u, db);

            if (chkUser)
            {
                return RedirectToAction("ViewAdmin", "SuperAdmin",
                    new { msg = "User updated successfully", color = "green" });
            }
            else
            {
                return RedirectToAction("ViewAdmin", "SuperAdmin", new { msg = "Somethings' wrong", color = "red" });
            }
        }

        [HttpPost]
        public ActionResult AdminById(int id)
        {
            User user = new UserBL().GetActiveUserById(id, db);
            User obj = new User()
            {
                Id = user.Id,
                Name = user.Name,
                Contact = user.Contact,
                Address = user.Address
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAdmin(int id)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (new UserBL().DeleteUser(id, db)
               )
            {
                return RedirectToAction("ViewAdmin", new { msg = "Record deleted successfully", color = "green" });
            }
            else
            {
                return RedirectToAction("ViewAdmin", new { msg = "Somethings' wrong", color = "red" });
            }
        }

        #endregion

        public ActionResult Marketplace()
        {
            return View();
        }

        public ActionResult About()
        {
            //searchScrap();
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Collection(string banner_image_url, string image_thumbnail_url, string floor_price,
            string one_day_volume)
        {
            ViewBag.banner_image_url = banner_image_url;
            ViewBag.image_thumbnail_url = image_thumbnail_url;
            ViewBag.floor_price = floor_price;
            ViewBag.one_day_volume = one_day_volume;

            return View();
        }

        public ActionResult NFTCollection(string description, string image_original_url, string name,
            string schema_name)
        {
            ViewBag.description = description;
            ViewBag.image_original_url = image_original_url;
            ViewBag.name = name;
            ViewBag.schema_name = schema_name;

            return View();
        }

        [HttpPost]
        public ActionResult Newsletter(string Email, string action)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", HttpContext.Request.Url.Scheme,
                HttpContext.Request.Url.Authority, "/");

            bool checkMail = MailSender.SendNewsletter(Email, BaseUrl);
            if (checkMail == true)
            {
                if (action == "coinGecko")
                {
                    return RedirectToAction("CoinGecko", "SuperAdmin");
                }

                if (action == "collection")
                {
                    return RedirectToAction("Collection", "SuperAdmin");
                }

                if (action == "NFTcollection")
                {
                    return RedirectToAction("NFTCollection", "SuperAdmin");
                }

                if (action == "contact")
                {
                    return RedirectToAction("Contact", "SuperAdmin");
                }

                if (action == "about")
                {
                    return RedirectToAction("About", "SuperAdmin");
                }

                if (action == "marketplace")
                {
                    return RedirectToAction("Marketplace", "SuperAdmin");
                }
            }

            return RedirectToAction("CoinGecko", "SuperAdmin", new { msg = "Mail sending fail!", color = "red" });
        }

        [HttpPost]
        public ActionResult PostContact(string Email, string Name, string Message)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", HttpContext.Request.Url.Scheme,
                HttpContext.Request.Url.Authority, "/");

            bool checkMail = MailSender.Contact(Name, Message, Email);
            if (checkMail == true)
            {
                return RedirectToAction("CoinGecko", "SuperAdmin");
            }

            return RedirectToAction("CoinGecko", "SuperAdmin", new { msg = "Mail sending fail!", color = "red" });
        }

        public ActionResult CollectionNFTs()
        {
            return View();
        }

        //Scraping action methods
        public ActionResult searchScrap(string url)
        {
            HtmlWeb website = new HtmlWeb();
            //            string url = "https://opensea.io/assets?search[query]=al";
            string Url = url;
            HtmlDocument doc = website.Load(Url);
            var parse = doc.DocumentNode.InnerHtml;

            List<HtmlNode> productResults = doc.DocumentNode
                .SelectNodes(
                    "//div[@class='Blockreact__Block-sc-1xf18x6-0 Assetreact__StyledContainer-sc-bnjqwy-0 dBFmez bwCDxg Asset--loaded']")
                .ToList();


            List<string> loveReact = new List<string>();
            List<string> cardfotter = new List<string>();
            List<string> price = new List<string>();

            //foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//div[@class='Imagereact__DivContainer-sc-dy59cl-0 kMPTZo Image--fade Image--isImageLoaded Image--isImageLoaderVisible AssetMedia--img']//img[@src]"))
            //{
            //    HtmlAttribute att = li.Attributes["src"];
            //    image.Add(att.Value);
            //}
            foreach (HtmlNode li in doc.DocumentNode.SelectNodes(
                         "//div[@class='Blockreact__Block-sc-1xf18x6-0 Flexreact__Flex-sc-1twd32i-0 FlexColumnreact__FlexColumn-sc-1wwz3hp-0 VerticalAlignedreact__VerticalAligned-sc-b4hiel-0 kDRydb jYqxGr ksFzlZ iXcsEj']"))
            {
                loveReact.Add(li.InnerText);
            }

            foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//div[@class='AssetCardFooter--name']"))
            {
                cardfotter.Add(li.InnerText);
            }

            foreach (HtmlNode li in doc.DocumentNode.SelectNodes(
                         "//div[@class='Overflowreact__OverflowContainer-sc-10mm0lu-0 gjwKJf Price--amount']"))
            {
                price.Add(li.InnerText);
            }

            ViewBag.ArtLoveReact = loveReact;
            ViewBag.ArtCardfotter = cardfotter;
            ViewBag.ArtPrice = price;
            ViewBag.size = cardfotter.Count();
            return View();
        }

        public ActionResult searchArtCollection()
        {
            HtmlWeb website = new HtmlWeb();
            string url = "https://opensea.io/collection/art";
            HtmlDocument doc = website.Load(url);
            var parse = doc.DocumentNode.InnerHtml;

            List<HtmlNode> productResults = doc.DocumentNode
                .SelectNodes(
                    "//div[@class='Blockreact__Block-sc-1xf18x6-0 CarouselCardreact__Container-sc-152cap8-0 dBFmez eyjpLW']")
                .ToList();

            List<string> link = new List<string>();
            foreach (HtmlNode li in doc.DocumentNode.SelectNodes(
                         "//div[@class='Blockreact__Block-sc-1xf18x6-0 CarouselCardreact__Container-sc-152cap8-0 dBFmez eyjpLW']//a[@href]"))
            {
                HtmlAttribute att = li.Attributes["href"];
                link.Add(att.Value);
            }

            List<string> Publisher = new List<string>();
            foreach (HtmlNode li in doc.DocumentNode.SelectNodes(
                         "//div[@class='Blockreact__Block-sc-1xf18x6-0 Textreact__Text-sc-1w94ul3-0 dBFmez hiGTZT CollectionCard--name']"))
            {
                Publisher.Add(li.InnerText);
            }

            List<string> Finallink = new List<string>();
            for (int i = 0; i < link.Count(); i++)
            {
                if (!(link[i].Contains("collection")))
                {
                    continue;
                    //Finallink.Add(link[i]);
                    //link.RemoveAt(i);
                }
                else
                    Finallink.Add(link[i]);
            }

            List<string> image = new List<string>();
            //foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//div[@class='Imagereact__DivContainer-sc-dy59cl-0 kMPTZo Image--isImageLoaded Image--isImageLoaderVisible CarouselCard--image']"))
            //{
            //    HtmlAttribute att = li.Attributes["src"];
            //    image.Add(att.Value);
            //}

            ViewBag.ArtPublisher = Publisher;
            ViewBag.ArtLink = Finallink;
            ViewBag.ArtImage = image;
            ViewBag.size = Publisher.Count();

            return View();
        }

        public ActionResult RetColDB()
        {
            //var ccclient = new RestClient("https://api.opensea.io/api/v1/collections?offset=1&limit=300");
            //var rrrequest = new RestRequest(Method.GET);
            //IRestResponse rrresponse = ccclient.Execute(rrrequest);

            RetrievingCollection rc = new RetrievingCollection();
            List<RetrievingCollection> rclist = new List<RetrievingCollection>();
            for (int i = 0; i < 84; i++)
            {
                var url = "https://api.opensea.io/api/v1/collections?offset=" + i + "&limit=300";
                var cclient = new RestClient(url);  // https://api.opensea.io/api/v1/collections?offset=1&limit=300
                var rrequest = new RestRequest(Method.GET);
                IRestResponse rresponse = cclient.Execute(rrequest);
                Root myDeserializedAuction =
                    JsonConvert.DeserializeObject<Root>(rresponse.Content.ToString());
                for (int j = 0; j < myDeserializedAuction.collections.Count(); j++)
                {
                    rc.one_day_average_price = myDeserializedAuction.collections[j].stats.one_day_average_price as float?;
                    rc.one_day_change = (float?)myDeserializedAuction.collections[j].stats.one_day_change;
                    rc.one_day_sales = (float?)myDeserializedAuction.collections[j].stats.one_day_sales;
                    rc.one_day_volume = myDeserializedAuction.collections[j].stats.one_day_volume is float ? (float)myDeserializedAuction.collections[j].stats.one_day_volume : 0;
                    rc.seven_day_average_price = myDeserializedAuction.collections[j].stats.seven_day_average_price as float?;
                    rc.seven_day_change = (float?)myDeserializedAuction.collections[j].stats.seven_day_change;
                    rc.seven_day_sales = (float?)myDeserializedAuction.collections[j].stats.seven_day_sales;
                    rc.seven_day_volume = myDeserializedAuction.collections[j].stats.seven_day_volume is float ? (float)myDeserializedAuction.collections[j].stats.seven_day_volume : 0;
                    rc.thirty_day_average_price = myDeserializedAuction.collections[j].stats.thirty_day_average_price as float?;
                    rc.thirty_day_change = (float?)myDeserializedAuction.collections[j].stats.thirty_day_change;
                    rc.thirty_day_sales = (float?)myDeserializedAuction.collections[j].stats.thirty_day_sales;
                    rc.thirty_day_volume = myDeserializedAuction.collections[j].stats.thirty_day_volume is float ? (float)myDeserializedAuction.collections[j].stats.thirty_day_volume : 0;
                    rc.total_sales = myDeserializedAuction.collections[j].stats.total_sales as float?;
                    rc.total_supply = (float?)myDeserializedAuction.collections[j].stats.total_supply;
                    rc.total_volume = (float?)myDeserializedAuction.collections[j].stats.total_volume;
                    rc.count = (float?)myDeserializedAuction.collections[j].stats.count;
                    rc.num_owners = myDeserializedAuction.collections[j].stats.num_owners;
                    rc.average_price = (float?)myDeserializedAuction.collections[j].stats.average_price;
                    rc.num_reports = myDeserializedAuction.collections[j].stats.num_reports;
                    rc.market_cap = (float?)myDeserializedAuction.collections[j].stats.market_cap;
                    rc.floor_price = myDeserializedAuction.collections[j].stats.floor_price;
                    rc.banner_image_url = myDeserializedAuction.collections[j].banner_image_url;
                    rc.featured_image_url = myDeserializedAuction.collections[j].featured_image_url;
                    rc.image_url = myDeserializedAuction.collections[j].image_url;
                    rc.large_image_url = myDeserializedAuction.collections[j].large_image_url;
                    rc.name = myDeserializedAuction.collections[j].name;
                    rc.slug = myDeserializedAuction.collections[j].slug;
                    rc.telegram_url = (string) myDeserializedAuction.collections[j].telegram_url;
                    rc.twitter_username = (string) myDeserializedAuction.collections[j].twitter_username;
                    rc.instagram_username = myDeserializedAuction.collections[j].instagram_username;
                    rc.CreatedAt = DateTime.Now;

                    rclist.Add(rc);
                    db.RetrievingCollections.Add(rc);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("CoinGecko");
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Traits
    {
    }

    public class Stats
    {
        public double one_day_volume { get; set; }
        public double one_day_change { get; set; }
        public double one_day_sales { get; set; }
        public double one_day_average_price { get; set; }
        public double seven_day_volume { get; set; }
        public double seven_day_change { get; set; }
        public double seven_day_sales { get; set; }
        public double seven_day_average_price { get; set; }
        public double thirty_day_volume { get; set; }
        public double thirty_day_change { get; set; }
        public double thirty_day_sales { get; set; }
        public double thirty_day_average_price { get; set; }
        public double total_volume { get; set; }
        public double total_sales { get; set; }
        public double total_supply { get; set; }
        public double count { get; set; }
        public int num_owners { get; set; }
        public double average_price { get; set; }
        public int num_reports { get; set; }
        public double market_cap { get; set; }
        public int floor_price { get; set; }
    }

    public class DisplayData
    {
        public string card_display_style { get; set; }
        public List<object> images { get; set; }
    }

    public class Collection
    {
        public List<object> primary_asset_contracts { get; set; }
        public Traits traits { get; set; }
        public Stats stats { get; set; }
        public string banner_image_url { get; set; }
        public object chat_url { get; set; }
        public DateTime created_date { get; set; }
        public bool default_to_fiat { get; set; }
        public string description { get; set; }
        public string dev_buyer_fee_basis_points { get; set; }
        public string dev_seller_fee_basis_points { get; set; }
        public object discord_url { get; set; }
        public DisplayData display_data { get; set; }
        public string external_url { get; set; }
        public bool featured { get; set; }
        public string featured_image_url { get; set; }
        public bool hidden { get; set; }
        public string safelist_request_status { get; set; }
        public string image_url { get; set; }
        public bool is_subject_to_whitelist { get; set; }
        public string large_image_url { get; set; }
        public object medium_username { get; set; }
        public string name { get; set; }
        public bool only_proxied_transfers { get; set; }
        public string opensea_buyer_fee_basis_points { get; set; }
        public string opensea_seller_fee_basis_points { get; set; }
        public string payout_address { get; set; }
        public bool require_email { get; set; }
        public object short_description { get; set; }
        public string slug { get; set; }
        public object telegram_url { get; set; }
        public object twitter_username { get; set; }
        public string instagram_username { get; set; }
        public object wiki_url { get; set; }
    }

    public class Root
    {
        public List<Collection> collections { get; set; }
    }

}