using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFT_Trade.helpingClasses
{
    public class Auction
    {
        // Auction myDeserializedAuction = JsonConvert.DeserializeObject<Auction>(myJsonResponse); 

        public List<AssetEvent> asset_events { get; set; }

        public class AssetContract
        {
            public string address { get; set; }
            public string asset_contract_type { get; set; }
            public DateTime created_date { get; set; }
            public string name { get; set; }
            public string nft_version { get; set; }
            public string opensea_version { get; set; }
            public int? owner { get; set; }
            public string schema_name { get; set; }
            public string symbol { get; set; }
            public string total_supply { get; set; }
            public string description { get; set; }
            public string external_link { get; set; }
            public string image_url { get; set; }
            public bool default_to_fiat { get; set; }
            public int dev_buyer_fee_basis_points { get; set; }
            public int dev_seller_fee_basis_points { get; set; }
            public bool only_proxied_transfers { get; set; }
            public int opensea_buyer_fee_basis_points { get; set; }
            public int opensea_seller_fee_basis_points { get; set; }
            public int buyer_fee_basis_points { get; set; }
            public int seller_fee_basis_points { get; set; }
            public string payout_address { get; set; }
        }

        public class DisplayData
        {
            public string card_display_style { get; set; }
        }

        public class Collection
        {
            public string banner_image_url { get; set; }
            public object chat_url { get; set; }
            public DateTime created_date { get; set; }
            public bool default_to_fiat { get; set; }
            public string description { get; set; }
            public string dev_buyer_fee_basis_points { get; set; }
            public string dev_seller_fee_basis_points { get; set; }
            public string discord_url { get; set; }
            public DisplayData display_data { get; set; }
            public string external_url { get; set; }
            public bool featured { get; set; }
            public string featured_image_url { get; set; }
            public bool hidden { get; set; }
            public string safelist_request_status { get; set; }
            public string image_url { get; set; }
            public bool is_subject_to_whitelist { get; set; }
            public string large_image_url { get; set; }
            public string medium_username { get; set; }
            public string name { get; set; }
            public bool only_proxied_transfers { get; set; }
            public string opensea_buyer_fee_basis_points { get; set; }
            public string opensea_seller_fee_basis_points { get; set; }
            public string payout_address { get; set; }
            public bool require_email { get; set; }
            public object short_description { get; set; }
            public string slug { get; set; }
            public string telegram_url { get; set; }
            public string twitter_username { get; set; }
            public string instagram_username { get; set; }
            public object wiki_url { get; set; }
        }

        public class User
        {
            public string username { get; set; }
        }

        public class Owner
        {
            public User user { get; set; }
            public string profile_img_url { get; set; }
            public string address { get; set; }
            public string config { get; set; }
        }

        public class Asset
        {
            public int id { get; set; }
            public string token_id { get; set; }
            public int num_sales { get; set; }
            public object background_color { get; set; }
            public string image_url { get; set; }
            public string image_preview_url { get; set; }
            public string image_thumbnail_url { get; set; }
            public string image_original_url { get; set; }
            public object animation_url { get; set; }
            public object animation_original_url { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string external_link { get; set; }
            public AssetContract asset_contract { get; set; }
            public string permalink { get; set; }
            public Collection collection { get; set; }
            public int? decimals { get; set; }
            public string token_metadata { get; set; }
            public Owner owner { get; set; }
        }

        public class FromAccount
        {
            public User user { get; set; }
            public string profile_img_url { get; set; }
            public string address { get; set; }
            public string config { get; set; }
        }

        public class PaymentToken
        {
            public int id { get; set; }
            public string symbol { get; set; }
            public string address { get; set; }
            public string image_url { get; set; }
            public string name { get; set; }
            public int decimals { get; set; }
            public string eth_price { get; set; }
            public string usd_price { get; set; }
        }

        public class Seller
        {
            public User user { get; set; }
            public string profile_img_url { get; set; }
            public string address { get; set; }
            public string config { get; set; }
        }

        public class AssetEvent
        {
            public object approved_account { get; set; }
            public Asset asset { get; set; }
            public object asset_bundle { get; set; }
            public string auction_type { get; set; }
            public string bid_amount { get; set; }
            public string collection_slug { get; set; }
            public string contract_address { get; set; }
            public DateTime created_date { get; set; }
            public object custom_event_name { get; set; }
            public object dev_fee_payment_event { get; set; }
            public string duration { get; set; }
            public string ending_price { get; set; }
            public string event_type { get; set; }
            public FromAccount from_account { get; set; }
            public int id { get; set; }
            public bool? is_private { get; set; }
            public object owner_account { get; set; }
            public PaymentToken payment_token { get; set; }
            public string quantity { get; set; }
            public Seller seller { get; set; }
            public string starting_price { get; set; }
            public object to_account { get; set; }
            public object total_price { get; set; }
            public object transaction { get; set; }
            public object winner_account { get; set; }
        }
    }
}