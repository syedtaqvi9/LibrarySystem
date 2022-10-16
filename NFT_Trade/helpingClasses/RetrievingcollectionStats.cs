using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFT_Trade.helpingClasses
{
    public class RetrievingcollectionStatsRoots
    {
        public Stats stats { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
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
        public double floor_price { get; set; }
    }

    public class Roots
    {
        public Stats stats { get; set; }
    }


}