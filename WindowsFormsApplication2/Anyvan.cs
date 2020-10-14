using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Anyvan
{
    public class ListingPickupAddress
    {
        public string town { get; set; }
        public string region { get; set; }
        public string postcode { get; set; }
    }

    public class ListingDeliveryAddress
    {
        public string town { get; set; }
        public string region { get; set; }
        public string postcode { get; set; }
    }

    public class Render
    {
        public int id { get; set; }
        public string listing_label { get; set; }
        public string listing_created_at { get; set; }
        public ListingPickupAddress listing_pickup_address { get; set; }
        public ListingDeliveryAddress listing_delivery_address { get; set; }
        public object listing_bid_count { get; set; }
        public object listing_lowest_bid { get; set; }
        public object listing_tp_lowest_bid { get; set; }
        public string book_now_price { get; set; }
        public string listing_distance { get; set; }
        public string listing_status { get; set; }
        public string listing_pickup_date { get; set; }
        public string date_mode { get; set; }
        public string listing_expires { get; set; }
        public string listing_locale { get; set; }
        public string pickup_lat { get; set; }
        public string pickup_lng { get; set; }
        public string delivery_lat { get; set; }
        public string delivery_lng { get; set; }
        public int is_featured { get; set; }
        public bool virtual_listing_visited { get; set; }
        public bool virtual_listing_watching { get; set; }
        public bool virtual_listing_bidding { get; set; }
        public bool virtual_buy_it_now { get; set; }
        public bool virtual_visible { get; set; }
        public string virtual_thumbnail_url { get; set; }
        public bool virtual_has_negotiation { get; set; }
        public string virtual_negotiation_is_public { get; set; }
        public string category_term { get; set; }
        public bool online { get; set; }
        public List<object> customer_is { get; set; }
        public int customer_called { get; set; }
        public string listing_details_transport_method { get; set; }
        public bool virtual_exclusive { get; set; }
        public string listing_lowest_bid_with_currency { get; set; }
        public string listing_details_volume { get; set; }
        public string listing_details_volume_units { get; set; }
        public string listing_details_restricted_to { get; set; }
        public string listing_details_tp_lowest_bid { get; set; }
        public string listing_details_cancel_date { get; set; }
        public string listing_details_customer_category { get; set; }
        public string listing_details_overall_weight { get; set; }
        public string listing_details_overall_weight_units { get; set; }
        public string listing_details_origin_type_of_property { get; set; }
        public string listing_details_destination_type_of_property { get; set; }
        public string listing_details_vehicle_type { get; set; }
        public string listing_details_auto_bids { get; set; }
        public string listing_details_group_bid_emails_outstanding { get; set; }
        public string listing_details_first_bid_amount { get; set; }
        public string listing_details_volume_concat { get; set; }
    }

    public class SavedSearch
    {
        public bool success { get; set; }
    }

    public class Order
    {
        public string listing_id { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public List<Render> render { get; set; }
        public SavedSearch saved_search { get; set; }
        public int page { get; set; }
        public int row_count { get; set; }
        public Order order { get; set; }
    }


    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
