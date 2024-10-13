﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectBookShop.Models;

#nullable disable

namespace ProjectBookShop.Migrations
{
    [DbContext(typeof(BookShopContext))]
    [Migration("20230707155224_Add-Book_TranslatorTB")]
    partial class AddBook_TranslatorTB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectBookShop.Models.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorID"));

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorID");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Author_Book", b =>
                {
                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<int>("AuthorID")
                        .HasColumnType("int");

                    b.HasKey("BookID", "AuthorID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Author_Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"));

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<int>("LanguagID")
                        .HasColumnType("int");

                    b.Property<int?>("LanguageID")
                        .HasColumnType("int");

                    b.Property<int>("NumOfPages")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("PublisherID")
                        .HasColumnType("int");

                    b.Property<int>("SCategoryID")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Summery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("weight")
                        .HasColumnType("smallint");

                    b.HasKey("BookID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("PublisherID");

                    b.HasIndex("SCategoryID");

                    b.ToTable("BookInfo", (string)null);
                });

            modelBuilder.Entity("ProjectBookShop.Models.Book_Translator", b =>
                {
                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<int>("TranslatorID")
                        .HasColumnType("int");

                    b.HasKey("BookID", "TranslatorID");

                    b.HasIndex("TranslatorID");

                    b.ToTable("Book_Translator");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProjectBookShop.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProviceID")
                        .HasColumnType("int");

                    b.HasKey("CityID");

                    b.HasIndex("ProviceID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Customer", b =>
                {
                    b.Property<string>("CustomserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CityID1")
                        .HasColumnType("int");

                    b.Property<int>("CityID2")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarChar(20)")
                        .HasColumnName("FName");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LName");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomserID");

                    b.HasIndex("CityID1");

                    b.HasIndex("CityID2");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Discount", b =>
                {
                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Percent")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BookID");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Language", b =>
                {
                    b.Property<int>("LanguageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageID"));

                    b.Property<string>("LanguageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageID");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Order", b =>
                {
                    b.Property<string>("OrderID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("AmountPaid")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("BuyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerCustomserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisPachtNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderStatusID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerCustomserID");

                    b.HasIndex("OrderStatusID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProjectBookShop.Models.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderStatusID"));

                    b.Property<string>("OrderStatusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderStatusID");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Order_Book", b =>
                {
                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<string>("OrderID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BookID", "OrderID");

                    b.HasIndex("OrderID");

                    b.ToTable("Order_Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Provice", b =>
                {
                    b.Property<int>("ProviceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProviceID"));

                    b.Property<string>("ProviceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProviceID");

                    b.ToTable("Provices");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Publisher", b =>
                {
                    b.Property<int>("PublisherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PublisherID"));

                    b.Property<string>("PublisherName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PublisherID");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("ProjectBookShop.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryID"));

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Translator", b =>
                {
                    b.Property<int>("TranslatorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TranslatorID"));

                    b.Property<string>("Family")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TranslatorID");

                    b.ToTable("Translator");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Author_Book", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Author", "Author")
                        .WithMany("Author_Books")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBookShop.Models.Book", "Book")
                        .WithMany("Author_Books")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Book", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Language", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageID");

                    b.HasOne("ProjectBookShop.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherID");

                    b.HasOne("ProjectBookShop.Models.SubCategory", "SubCategory")
                        .WithMany("Books")
                        .HasForeignKey("SCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Publisher");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Book_Translator", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Book", "Book")
                        .WithMany("book_Tranlators")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBookShop.Models.Translator", "Translator")
                        .WithMany("book_Tranlators")
                        .HasForeignKey("TranslatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Translator");
                });

            modelBuilder.Entity("ProjectBookShop.Models.City", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Provice", "Provice")
                        .WithMany("Cities")
                        .HasForeignKey("ProviceID");

                    b.Navigation("Provice");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Customer", b =>
                {
                    b.HasOne("ProjectBookShop.Models.City", "city1")
                        .WithMany("Customers1")
                        .HasForeignKey("CityID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBookShop.Models.City", "city2")
                        .WithMany("Customers2")
                        .HasForeignKey("CityID2")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("city1");

                    b.Navigation("city2");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Discount", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Book", "Book")
                        .WithOne("Discount")
                        .HasForeignKey("ProjectBookShop.Models.Discount", "BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Order", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerCustomserID");

                    b.HasOne("ProjectBookShop.Models.OrderStatus", "orderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusID");

                    b.Navigation("Customer");

                    b.Navigation("orderStatus");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Order_Book", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Book", "Book")
                        .WithMany("order_Books")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBookShop.Models.Order", "Order")
                        .WithMany("order_Books")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ProjectBookShop.Models.SubCategory", b =>
                {
                    b.HasOne("ProjectBookShop.Models.Category", "Category")
                        .WithMany("subCategoy")
                        .HasForeignKey("CategoryID");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Author", b =>
                {
                    b.Navigation("Author_Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Book", b =>
                {
                    b.Navigation("Author_Books");

                    b.Navigation("Discount");

                    b.Navigation("book_Tranlators");

                    b.Navigation("order_Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Category", b =>
                {
                    b.Navigation("subCategoy");
                });

            modelBuilder.Entity("ProjectBookShop.Models.City", b =>
                {
                    b.Navigation("Customers1");

                    b.Navigation("Customers2");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Language", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Order", b =>
                {
                    b.Navigation("order_Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Provice", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.SubCategory", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ProjectBookShop.Models.Translator", b =>
                {
                    b.Navigation("book_Tranlators");
                });
#pragma warning restore 612, 618
        }
    }
}
