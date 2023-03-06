﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuggestionPanel.Application.Data;

#nullable disable

namespace SuggestionPanel.Application.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SuggestionPanel.Domain.Models.Cost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("SuggestionPanel.Domain.Models.HumanResources", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("HumanResources");
                });

            modelBuilder.Entity("SuggestionPanel.Domain.Models.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfSubmission")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ImplementationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImplementationDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCardAnomaly")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Money")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PropositionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SignedToId")
                        .HasColumnType("int");

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SubmissionOwnerId")
                        .HasColumnType("int");

                    b.Property<bool?>("ToCommittee")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CostId");

                    b.HasIndex("SignedToId");

                    b.HasIndex("SubmissionOwnerId");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("SuggestionPanel.Domain.Models.ValueStream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ValueStreams");
                });

            modelBuilder.Entity("SuggestionPanel.Domain.Models.ValueStreamResponsibility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ValueStreamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ValueStreamId");

                    b.ToTable("ValueStreamResponsibilities");
                });

            modelBuilder.Entity("SuggestionPanel.Domain.Models.Suggestion", b =>
                {
                    b.HasOne("SuggestionPanel.Domain.Models.Cost", "Cost")
                        .WithMany()
                        .HasForeignKey("CostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuggestionPanel.Domain.Models.ValueStreamResponsibility", "SignedTo")
                        .WithMany()
                        .HasForeignKey("SignedToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuggestionPanel.Domain.Models.HumanResources", "SubmissionOwner")
                        .WithMany()
                        .HasForeignKey("SubmissionOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cost");

                    b.Navigation("SignedTo");

                    b.Navigation("SubmissionOwner");
                });

            modelBuilder.Entity("SuggestionPanel.Domain.Models.ValueStreamResponsibility", b =>
                {
                    b.HasOne("SuggestionPanel.Domain.Models.ValueStream", "ValueStream")
                        .WithMany()
                        .HasForeignKey("ValueStreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ValueStream");
                });
#pragma warning restore 612, 618
        }
    }
}
