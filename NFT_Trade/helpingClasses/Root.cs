using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFT_Trade.helpingClasses
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
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
        public List<object> images { get; set; }
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
        public string twitter_username { get; set; }
        public string instagram_username { get; set; }
        public object wiki_url { get; set; }
    }

    public class Users
    {
        public string username { get; set; }
    }

    public class Owner
    {
        public Users user { get; set; }
        public string profile_img_url { get; set; }
        public string address { get; set; }
        public string config { get; set; }
    }

    public class Creator
    {
        public Users user { get; set; }
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
        public object image_original_url { get; set; }
        public object animation_url { get; set; }
        public object animation_original_url { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object external_link { get; set; }
        public AssetContract asset_contract { get; set; }
        public string permalink { get; set; }
        public Collection collection { get; set; }
        public object decimals { get; set; }
        public object token_metadata { get; set; }
        public Owner owner { get; set; }
        public object sell_orders { get; set; }
        public Creator creator { get; set; }
        public List<object> traits { get; set; }
        public object last_sale { get; set; }
        public object top_bid { get; set; }
        public object listing_date { get; set; }
        public bool is_presale { get; set; }
        public object transfer_fee_payment_token { get; set; }
        public object transfer_fee { get; set; }
    }

    public class Root
    {
        public List<Asset> assets { get; set; }
    }


}