using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StockAlert.Models;

namespace addingefcoreonosx.Migrations
{
    [DbContext(typeof(ArticleContext))]
    [Migration("20170110171855_InitialMigrationv")]
    partial class InitialMigrationv
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("StockAlert.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Body");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("StockAlert.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("StockAlert.Models.Ticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrentPrice");

                    b.Property<string>("Name");

                    b.Property<int>("YesterdayPrice");

                    b.HasKey("Id");

                    b.ToTable("Tickers");
                });

            modelBuilder.Entity("StockAlert.Models.Article", b =>
                {
                    b.HasOne("StockAlert.Models.Author", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
