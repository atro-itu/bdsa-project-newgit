﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NEWgIT.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NEWgIT.Infrastructure.Migrations
{
    [DbContext(typeof(GitContext))]
    [Migration("20221111123211_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NEWgIT.Infrastructure.Analysis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LatestCommitHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RepoIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RepoIdentifier")
                        .IsUnique();

                    b.ToTable("Analysis");
                });

            modelBuilder.Entity("NEWgIT.Infrastructure.CommitInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnalysisId")
                        .HasColumnType("integer");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Id");

                    b.HasIndex("AnalysisId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Commits");
                });

            modelBuilder.Entity("NEWgIT.Infrastructure.CommitInfo", b =>
                {
                    b.HasOne("NEWgIT.Infrastructure.Analysis", "Analysis")
                        .WithMany("Commits")
                        .HasForeignKey("AnalysisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Analysis");
                });

            modelBuilder.Entity("NEWgIT.Infrastructure.Analysis", b =>
                {
                    b.Navigation("Commits");
                });
#pragma warning restore 612, 618
        }
    }
}
