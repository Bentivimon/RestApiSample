﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RestApiTest.Data.Context;
using System;

namespace RestApiTest.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    partial class UniversityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("RestApiTest.Data.Models.GroupEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountOfStudents");

                    b.Property<long>("FacultyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("RestApiTest.Data.Models.StudentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("FirstName");

                    b.Property<long>("GroupId");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("RestApiTest.Data.Models.StudentEntity", b =>
                {
                    b.HasOne("RestApiTest.Data.Models.GroupEntity", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
