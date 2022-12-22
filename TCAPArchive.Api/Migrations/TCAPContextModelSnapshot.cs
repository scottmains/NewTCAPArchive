﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TCAPArchive.Api.Models;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    [DbContext(typeof(TCAPContext))]
    partial class TCAPContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TCAPArchive.Shared.Domain.ChatLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("SenderHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChatSessionId");

                    b.ToTable("ChatLines");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.ChatSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ChatLength")
                        .HasColumnType("int");

                    b.Property<Guid>("DecoyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PredatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DecoyId");

                    b.HasIndex("PredatorId");

                    b.ToTable("ChatSessions");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.Decoy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Handle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PredatorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Decoys");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.Predator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Handle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StingLocation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Predators");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.ChatLine", b =>
                {
                    b.HasOne("TCAPArchive.Shared.Domain.ChatSession", "ChatSession")
                        .WithMany("ChatLines")
                        .HasForeignKey("ChatSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatSession");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.ChatSession", b =>
                {
                    b.HasOne("TCAPArchive.Shared.Domain.Decoy", "Decoy")
                        .WithMany("ChatSessions")
                        .HasForeignKey("DecoyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TCAPArchive.Shared.Domain.Predator", "Predator")
                        .WithMany("ChatSessions")
                        .HasForeignKey("PredatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Decoy");

                    b.Navigation("Predator");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.ChatSession", b =>
                {
                    b.Navigation("ChatLines");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.Decoy", b =>
                {
                    b.Navigation("ChatSessions");
                });

            modelBuilder.Entity("TCAPArchive.Shared.Domain.Predator", b =>
                {
                    b.Navigation("ChatSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
