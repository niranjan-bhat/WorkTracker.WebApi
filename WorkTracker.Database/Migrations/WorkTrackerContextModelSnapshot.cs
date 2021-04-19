﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkTracker.Database;

namespace WorkTracker.Database.Migrations
{
    [DbContext(typeof(WorkTrackerContext))]
    partial class WorkTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("AssignmentJob", b =>
                {
                    b.Property<int>("AssignmentsId")
                        .HasColumnType("int");

                    b.Property<int>("JobsId")
                        .HasColumnType("int");

                    b.HasKey("AssignmentsId", "JobsId");

                    b.HasIndex("JobsId");

                    b.ToTable("AssignmentJob");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("Wage")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("AssignedDate", "WorkerId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AddeTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerComment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name", "OwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EncryptedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name", "OwnerId");

                    b.HasIndex("Mobile")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Worker");
                });

            modelBuilder.Entity("AssignmentJob", b =>
                {
                    b.HasOne("WorkTracker.Database.Models.Assignment", null)
                        .WithMany()
                        .HasForeignKey("AssignmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkTracker.Database.Models.Job", null)
                        .WithMany()
                        .HasForeignKey("JobsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Assignment", b =>
                {
                    b.HasOne("WorkTracker.Database.Models.Owner", "Owner")
                        .WithMany("Assignment")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkTracker.Database.Models.Worker", "Worker")
                        .WithMany("Assignments")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Comment", b =>
                {
                    b.HasOne("WorkTracker.Database.Models.Assignment", "Assignment")
                        .WithMany("Comments")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Job", b =>
                {
                    b.HasOne("WorkTracker.Database.Models.Owner", "Owner")
                        .WithMany("Jobs")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Worker", b =>
                {
                    b.HasOne("WorkTracker.Database.Models.Owner", "Owner")
                        .WithMany("Workers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Assignment", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Owner", b =>
                {
                    b.Navigation("Assignment");

                    b.Navigation("Jobs");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("WorkTracker.Database.Models.Worker", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
