﻿// <auto-generated />
using System;
using LessonService.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LessonService.Infrastructure.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lessonsrv")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LessonService.Core.Base.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("Duration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(60);

                    b.Property<int>("LessonStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<int>("LessonType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<int>("MaxStudents")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("TrainerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<int>("TrainingLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("Lessons", "lessonsrv");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e7944d6d-6a6e-4323-af4f-a12b792282a5"),
                            DateFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Description 1",
                            Duration = 0,
                            LessonStatus = 0,
                            LessonType = 0,
                            MaxStudents = 0,
                            Name = "Lesson 1",
                            TrainerId = new Guid("00000000-0000-0000-0000-000000000000"),
                            TrainingLevel = 0
                        },
                        new
                        {
                            Id = new Guid("591f2f13-a49a-462e-afc1-2a94f8f24389"),
                            DateFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Description 2",
                            Duration = 0,
                            LessonStatus = 0,
                            LessonType = 0,
                            MaxStudents = 0,
                            Name = "Lesson 2",
                            TrainerId = new Guid("00000000-0000-0000-0000-000000000000"),
                            TrainingLevel = 0
                        });
                });

            modelBuilder.Entity("LessonService.Core.Base.LessonGroup", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uuid");

                    b.HasKey("StudentId", "LessonId");

                    b.HasIndex("LessonId");

                    b.ToTable("LessonGroups", "lessonsrv");
                });

            modelBuilder.Entity("LessonService.Core.Base.LessonGroup", b =>
                {
                    b.HasOne("LessonService.Core.Base.Lesson", "Lesson")
                        .WithMany("LessonGroups")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LessonService.Core.Base.Lesson", b =>
                {
                    b.Navigation("LessonGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
