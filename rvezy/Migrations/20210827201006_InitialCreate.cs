using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rvezy.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Listing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    listing_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    scrape_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_scraped = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    space = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    experiences_offered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    neighborhood_overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medium_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    picture_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    xl_picture_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_since = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_about = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_response_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_response_rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_acceptance_rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_is_superhost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_picture_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_neighbourhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_listings_count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_total_listings_count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_verifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_has_profile_pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    host_identity_verified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    neighbourhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    neighbourhood_cleansed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    neighbourhood_group_cleansed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    market = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    smart_location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_location_exact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    property_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    room_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accommodates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bathrooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bedrooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    beds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bed_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amenities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    square_feet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weekly_price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    monthly_price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    security_deposit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cleaning_fee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    guests_included = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    extra_people = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    minimum_nights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    maximum_nights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calendar_updated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    has_availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    availability_30 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    availability_60 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    availability_90 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    availability_365 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calendar_last_scraped = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    number_of_reviews = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    first_review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review_scores_rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review_scores_accuracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review_scores_cleanliness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review_scores_checkin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review_scores_communication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review_scores_location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review_scores_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requires_license = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    license = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jurisdiction_names = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    instant_bookable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cancellation_policy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    require_guest_profile_picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    require_guest_phone_verification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calculated_host_listings_count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reviews_per_month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "Listing");

            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
