using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rvezy.Models
{
      public class Listing
    {
        public int Id { get; set; }
        public string ListingUrl { get; set; }
        public string ScrapeId { get; set; }
        public string LastScraped { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Space { get; set; }
        public string Description { get; set; }
        public string ExperiencesOffered { get; set; }
        public string NeighborhoodOverview { get; set; }
        public string Notes { get; set; }
        public string Transit { get; set; }
        public string ThumbnailUrl { get; set; }
        public string medium_url { get; set; }
        public string picture_url { get; set; }
        public string xl_picture_url { get; set; }
        public string host_id { get; set; }
        public string host_url { get; set; }
        public string host_since { get; set; }
        public string host_location { get; set; }
        public string host_about { get; set; }
        public string host_response_time { get; set; }
        public string host_response_rate { get; set; }
        public string host_acceptance_rate { get; set; }
        public string host_is_superhost { get; set; }
        public string host_thumbnail_url { get; set; }
        public string host_picture_url { get; set; }
        public string host_neighbourhood { get; set; }
        public string host_listings_count { get; set; }
        public string host_total_listings_count { get; set; }
        public string host_verifications { get; set; }
        public string host_has_profile_pic { get; set; }
        public string host_identity_verified { get; set; }
        public string street { get; set; }
        public string neighbourhood { get; set; }
        public string neighbourhood_cleansed { get; set; }
        public string neighbourhood_group_cleansed { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string market { get; set; }
        public string smart_location { get; set; }
        public string country_code { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Boolean is_location_exact { get; set; }
        public string property_type { get; set; }
        public string room_type { get; set; }
        public string accommodates { get; set; }
        public string bathrooms { get; set; }
        public string bedrooms { get; set; }
        public string beds { get; set; }
        public string bed_type { get; set; }
        public string amenities { get; set; }
        public string square_feet { get; set; }
        public string price { get; set; }
        public string weekly_price { get; set; }
        public string monthly_price { get; set; }
        public string security_deposit { get; set; }
        public string cleaning_fee { get; set; }
        public string guests_included { get; set; }
        public string extra_people { get; set; }
        public string minimum_nights { get; set; }
        public string maximum_nights { get; set; }
        public string calendar_updated { get; set; }
        public string has_availability { get; set; }
        public string availability_30 { get; set; }
        public string availability_60 { get; set; }
        public string availability_90 { get; set; }
        public string availability_365 { get; set; }
        public string calendar_last_scraped { get; set; }
        public string number_of_reviews { get; set; }
        public string first_review { get; set; }
        public string last_review { get; set; }
        public string review_scores_rating { get; set; }
        public string review_scores_accuracy { get; set; }
        public string review_scores_cleanliness { get; set; }
        public string review_scores_checkin { get; set; }
        public string review_scores_communication { get; set; }
        public string review_scores_location { get; set; }
        public string review_scores_value { get; set; }
        public string requires_license { get; set; }
        public string license { get; set; }
        public string jurisdiction_names { get; set; }
        public string instant_bookable { get; set; }
        public string cancellation_policy { get; set; }
        public string require_guest_profile_picture { get; set; }
        public string require_guest_phone_verification { get; set; }
        public string calculated_host_listings_count { get; set; }
        public string reviews_per_month { get; set; }

    }
}
