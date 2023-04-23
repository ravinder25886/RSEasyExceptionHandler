﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RS_EasyExceptionHandling7.Persistence;

#nullable disable

namespace RS_EasyExceptionHandling7.Migrations
{
    [DbContext(typeof(RS_App_DbContext))]
    partial class RS_App_DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("RS_EasyExceptionHandling7.Models.RS_AppErrorLogs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ErrorCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ErrorDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorDetail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorSource")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorTitle")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsMailSent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LogType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MailSentDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("tblAppErrorLogs");
                });
#pragma warning restore 612, 618
        }
    }
}