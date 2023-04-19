﻿// <auto-generated />
using System;
using FRDZSchool.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FRDZSchool.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230419154909_editDateTimeErrorMgration")]
    partial class editDateTimeErrorMgration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FRDZSchool.Models.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AcademYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Litera")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("FRDZSchool.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("HomeTask")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchoolObjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolObjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("FRDZSchool.Models.Lesson_Student", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Mark")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Visiting")
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("LessonId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Lesson_Student");
                });

            modelBuilder.Entity("FRDZSchool.Models.School_Object", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("School_Object");
                });

            modelBuilder.Entity("FRDZSchool.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fathername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("FRDZSchool.Models.Student_Grade", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("GradeId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "GradeId");

                    b.HasIndex("GradeId");

                    b.ToTable("Student_Grade");
                });

            modelBuilder.Entity("FRDZSchool.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<string>("Fathername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Post")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("FRDZSchool.Models.Lesson", b =>
                {
                    b.HasOne("FRDZSchool.Models.School_Object", "SchoolObject")
                        .WithMany()
                        .HasForeignKey("SchoolObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FRDZSchool.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolObject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("FRDZSchool.Models.Lesson_Student", b =>
                {
                    b.HasOne("FRDZSchool.Models.Lesson", "Lesson")
                        .WithMany("Lesson_Student")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FRDZSchool.Models.Student", "Student")
                        .WithMany("Lesson_Student")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("FRDZSchool.Models.Student_Grade", b =>
                {
                    b.HasOne("FRDZSchool.Models.Grade", "Grade")
                        .WithMany("Student_Grades")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FRDZSchool.Models.Student", "Student")
                        .WithMany("Student_Grade")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("FRDZSchool.Models.Grade", b =>
                {
                    b.Navigation("Student_Grades");
                });

            modelBuilder.Entity("FRDZSchool.Models.Lesson", b =>
                {
                    b.Navigation("Lesson_Student");
                });

            modelBuilder.Entity("FRDZSchool.Models.Student", b =>
                {
                    b.Navigation("Lesson_Student");

                    b.Navigation("Student_Grade");
                });
#pragma warning restore 612, 618
        }
    }
}
