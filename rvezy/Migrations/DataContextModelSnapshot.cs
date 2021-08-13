﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rvezy.Data;

namespace rvezy.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("rvezy.Models.Calendar", b =>
                {
                    b.Property<int>("listing_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("available")
                        .HasColumnType("bit");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.HasKey("listing_id");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("rvezy.Models.Listing", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("accommodates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amenities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("availability_30")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("availability_365")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("availability_60")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("availability_90")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bathrooms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bed_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bedrooms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("beds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("calculated_host_listings_count")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("calendar_last_scraped")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("calendar_updated")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cancellation_policy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cleaning_fee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("experiences_offered")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("extra_people")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("guests_included")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("has_availability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_about")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_acceptance_rate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_has_profile_pic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_identity_verified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_is_superhost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_listings_count")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_neighbourhood")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_picture_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_response_rate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_response_time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_since")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_thumbnail_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_total_listings_count")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("host_verifications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instant_bookable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("is_location_exact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("jurisdiction_names")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_scraped")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("license")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("listing_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("market")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("maximum_nights")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("medium_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("minimum_nights")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("monthly_price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("neighborhood_overview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("neighbourhood")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("neighbourhood_cleansed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("neighbourhood_group_cleansed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("number_of_reviews")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("picture_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("property_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("require_guest_phone_verification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("require_guest_profile_picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requires_license")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("review_scores_accuracy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("review_scores_checkin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("review_scores_cleanliness")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("review_scores_communication")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("review_scores_location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("review_scores_rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("review_scores_value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reviews_per_month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("room_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("scrape_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("security_deposit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("smart_location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("space")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("square_feet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("thumbnail_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("weekly_price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("xl_picture_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zipcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("rvezy.Models.Reviews", b =>
                {
                    b.Property<int>("listing_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reviewer_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reviewer_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("listing_id");

                    b.ToTable("Reviewss");
                });
#pragma warning restore 612, 618
        }
    }
}