using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFT_Trade.helpingClasses
{
    // SearchValue myDeserializedClass = JsonConvert.DeserializeObject<SearchValue>(myJsonResponse); 
    public class SearchValue
    {
        public List<Assets> assets { get; set; }
    }
    public class AssetContractSV
    {
        public string address { get; set; }
        public string asset_contract_type { get; set; }
        public DateTime created_date { get; set; }
        public string name { get; set; }
        public object nft_version { get; set; }
        public string opensea_version { get; set; }
        public int? owner { get; set; }
        public string schema_name { get; set; }
        public string symbol { get; set; }
        public object total_supply { get; set; }
        public string description { get; set; }
        public object external_link { get; set; }
        public object image_url { get; set; }
        public bool default_to_fiat { get; set; }
        public int? dev_buyer_fee_basis_points { get; set; }
        public int? dev_seller_fee_basis_points { get; set; }
        public bool only_proxied_transfers { get; set; }
        public int? opensea_buyer_fee_basis_points { get; set; }
        public int? opensea_seller_fee_basis_points { get; set; }
        public int? buyer_fee_basis_points { get; set; }
        public int? seller_fee_basis_points { get; set; }
        public object payout_address { get; set; }
    }

    public class DisplayDataSV
    {
        public string card_display_style { get; set; }
        public List<object> images { get; set; }
    }

    public class CollectionSV
    {
        public string banner_image_url { get; set; }
        public object chat_url { get; set; }
        public DateTime created_date { get; set; }
        public bool default_to_fiat { get; set; }
        public string description { get; set; }
        public string dev_buyer_fee_basis_points { get; set; }
        public string dev_seller_fee_basis_points { get; set; }
        public string discord_url { get; set; }
        public DisplayDataSV display_data { get; set; }
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

    public class UserSV
    {
        public string username { get; set; }
    }

    public class OwnerSV
    {
        public UserSV user { get; set; }
        public string profile_img_url { get; set; }
        public string address { get; set; }
        public string config { get; set; }
    }

    public class Asset2
    {
        public string id { get; set; }
        public string address { get; set; }
        public string quantity { get; set; }
    }

    public class Metadata
    {
        public Asset asset { get; set; }
        public string schema { get; set; }
    }

    public class Maker
    {
        public int? user { get; set; }
        public string profile_img_url { get; set; }
        public string address { get; set; }
        public string config { get; set; }
    }

    public class Taker
    {
        public int? user { get; set; }
        public string profile_img_url { get; set; }
        public string address { get; set; }
        public string config { get; set; }
    }

    public class FeeRecipient
    {
        public int? user { get; set; }
        public string profile_img_url { get; set; }
        public string address { get; set; }
        public string config { get; set; }
    }

    public class PaymentTokenContract
    {
        public int? id { get; set; }
        public string symbol { get; set; }
        public string address { get; set; }
        public string image_url { get; set; }
        public string name { get; set; }
        public int? decimals { get; set; }
        public string eth_price { get; set; }
        public string usd_price { get; set; }
    }

    public class SellOrder
    {
        public DateTime created_date { get; set; }
        public DateTime closing_date { get; set; }
        public bool closing_extendable { get; set; }
        public int? expiration_time { get; set; }
        public int? listing_time { get; set; }
        public string order_hash { get; set; }
        public Metadata metadata { get; set; }
        public string exchange { get; set; }
        public Maker maker { get; set; }
        public Taker taker { get; set; }
        public string current_price { get; set; }
        public string current_bounty { get; set; }
        public string bounty_multiple { get; set; }
        public string maker_relayer_fee { get; set; }
        public string taker_relayer_fee { get; set; }
        public string maker_protocol_fee { get; set; }
        public string taker_protocol_fee { get; set; }
        public string maker_referrer_fee { get; set; }
        public FeeRecipient fee_recipient { get; set; }
        public int? fee_method { get; set; }
        public int? side { get; set; }
        public int? sale_kind { get; set; }
        public string target { get; set; }
        public int? how_to_call { get; set; }
        public string calldata { get; set; }
        public string replacement_pattern { get; set; }
        public string static_target { get; set; }
        public string static_extradata { get; set; }
        public string payment_token { get; set; }
        public PaymentTokenContract payment_token_contract { get; set; }
        public string base_price { get; set; }
        public string extra { get; set; }
        public string quantity { get; set; }
        public string salt { get; set; }
        public int? v { get; set; }
        public string r { get; set; }
        public string s { get; set; }
        public bool approved_on_chain { get; set; }
        public bool cancelled { get; set; }
        public bool finalized { get; set; }
        public bool marked_invalid { get; set; }
        public string prefixed_hash { get; set; }
    }

    public class CreatorSV
    {
        public UserSV user { get; set; }
        public string profile_img_url { get; set; }
        public string address { get; set; }
        public string config { get; set; }
    }

    public class Trait
    {
        public string trait_type { get; set; }
        public object value { get; set; }
        public string display_type { get; set; }
        public string max_value { get; set; }
        public int? trait_count { get; set; }
        public object order { get; set; }
    }

    public class Assets
    {
        public int? id { get; set; }
        public string token_id { get; set; }
        public int? num_sales { get; set; }
        public object background_color { get; set; }
        public string image_url { get; set; }
        public string image_preview_url { get; set; }
        public string image_thumbnail_url { get; set; }
        public object image_original_url { get; set; }
        public string animation_url { get; set; }
        public string animation_original_url { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string external_link { get; set; }
        public AssetContractSV asset_contract { get; set; }
        public string permalink { get; set; }
        public CollectionSV collection { get; set; }
        public object decimals { get; set; }
        public object token_metadata { get; set; }
        public OwnerSV owner { get; set; }
        public List<SellOrder> sell_orders { get; set; }
        public CreatorSV creator { get; set; }
        public List<Trait> traits { get; set; }
        public object last_sale { get; set; }
        public object top_bid { get; set; }
        public object listing_date { get; set; }
        public bool is_presale { get; set; }
        public object transfer_fee_payment_token { get; set; }
        public object transfer_fee { get; set; }
    }
}