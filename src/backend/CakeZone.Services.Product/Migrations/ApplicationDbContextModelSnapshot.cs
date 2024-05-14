﻿// <auto-generated />
using System;
using CakeZone.Services.Product.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CakeZone.Services.Product.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CakeZone.Services.Product.Model.Attribute", b =>
                {
                    b.Property<Guid>("AttributeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AttributeDefinition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttributeGroups")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttributeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DataGovernanceFlags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InheritanceRules")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NavigationSequence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitsOfMeasure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("AttributeId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("CakeZone.Services.Product.Model.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CakeZone.Services.Product.Model.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId")
                        .HasDefaultValueSql("newid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductId")
                        .HasName("PK_Products_5793275AF5B5B6D7");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("CakeZone.Services.Product.Model.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AltText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("ProductAttributes", b =>
                {
                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AttributeId");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Value");

                    b.HasKey("AttributeId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductAttributes", (string)null);
                });

            modelBuilder.Entity("ProductCategories", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CategoryId");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories", (string)null);
                });

            modelBuilder.Entity("CakeZone.Services.Product.Model.Category", b =>
                {
                    b.HasOne("CakeZone.Services.Product.Model.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("CakeZone.Services.Product.Model.ProductImage", b =>
                {
                    b.HasOne("CakeZone.Services.Product.Model.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductAttributes", b =>
                {
                    b.HasOne("CakeZone.Services.Product.Model.Attribute", null)
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .IsRequired()
                        .HasConstraintName("FK_Attributes_5683275AF5B5B6D7");

                    b.HasOne("CakeZone.Services.Product.Model.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Products_5793275AF5B5B69F");
                });

            modelBuilder.Entity("ProductCategories", b =>
                {
                    b.HasOne("CakeZone.Services.Product.Model.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Category_G798FB98899322");

                    b.HasOne("CakeZone.Services.Product.Model.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Products_5793275AF5B5B6D7");
                });

            modelBuilder.Entity("CakeZone.Services.Product.Model.Product", b =>
                {
                    b.Navigation("ProductImages");
                });
#pragma warning restore 612, 618
        }
    }
}
