﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyStudentApi.Data;

#nullable disable

namespace MyStudentApi.Migrations
{
    [DbContext(typeof(TendancyDbContext))]
    partial class TendancyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyStudentApi.Models.AttendanceViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Course")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRegistered")
                        .HasColumnType("bit");

                    b.Property<int?>("SchoolClassId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StopTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolClassId");

                    b.HasIndex("StudentId");

                    b.ToTable("AttendanceViewModel");
                });

            modelBuilder.Entity("MyStudentApi.Models.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClasssName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseCode")
                        .HasColumnType("int");

                    b.Property<int?>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StopTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeacherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitLoad")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SchoolClasses");
                });

            modelBuilder.Entity("MyStudentApi.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MyStudentApi.Models.StudentSchoolClass", b =>
                {
                    b.Property<int>("SchoolClassId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("SchoolClassId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentSchoolClass", (string)null);
                });

            modelBuilder.Entity("MyStudentApi.Models.AttendanceViewModel", b =>
                {
                    b.HasOne("MyStudentApi.Models.SchoolClass", "SchoolClass")
                        .WithMany("AttendanceViewModel")
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyStudentApi.Models.Student", "Student")
                        .WithMany("AttendanceViewModel")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolClass");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MyStudentApi.Models.StudentSchoolClass", b =>
                {
                    b.HasOne("MyStudentApi.Models.SchoolClass", "SchoolClass")
                        .WithMany()
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyStudentApi.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolClass");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MyStudentApi.Models.SchoolClass", b =>
                {
                    b.Navigation("AttendanceViewModel");
                });

            modelBuilder.Entity("MyStudentApi.Models.Student", b =>
                {
                    b.Navigation("AttendanceViewModel");
                });
#pragma warning restore 612, 618
        }
    }
}
