﻿// <auto-generated />
using Licenta.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Licenta.Migrations
{
    [DbContext(typeof(DBRezervareHotelieraContext))]
    partial class DBRezervareHotelieraContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Licenta.Entityes.CreditCard", b =>
                {
                    b.Property<int>("IdCard")
                        .HasColumnName("idCard");

                    b.Property<DateTime?>("CardExpireDate")
                        .HasColumnName("cardExpireDate")
                        .HasColumnType("date");

                    b.Property<string>("CardNumber")
                        .HasColumnName("cardNumber")
                        .HasMaxLength(16)
                        .IsUnicode(false);

                    b.Property<int?>("Cvc")
                        .HasColumnName("CVC");

                    b.Property<int?>("IdClient")
                        .HasColumnName("idClient");

                    b.Property<long?>("MoneyInTheCard");

                    b.HasKey("IdCard");

                    b.HasIndex("IdClient");

                    b.ToTable("CreditCard");
                });

            modelBuilder.Entity("Licenta.Entityes.Customers", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idCustomer");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .HasColumnName("firstName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .HasColumnName("lastName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("IdCustomer");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Licenta.Entityes.Employes", b =>
                {
                    b.Property<int>("Idemploye")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDEmploye");

                    b.Property<string>("EmployType")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<int?>("IdHotel");

                    b.Property<string>("LastName")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .HasMaxLength(55)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .HasMaxLength(55)
                        .IsUnicode(false);

                    b.HasKey("Idemploye");

                    b.HasIndex("IdHotel");

                    b.ToTable("Employes");
                });

            modelBuilder.Entity("Licenta.Entityes.Facilities", b =>
                {
                    b.Property<int>("IdFacilities")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idFacilities");

                    b.Property<string>("FacilitiesName")
                        .HasColumnName("facilitiesName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<bool>("IsChecked")
                        .HasColumnName("IsChecked");

                    b.HasKey("IdFacilities");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("Licenta.Entityes.FacilitiesHotel", b =>
                {
                    b.Property<int>("IdHotel")
                        .HasColumnName("idHotel");

                    b.Property<int>("IdFacilities")
                        .HasColumnName("idFacilities");

                    b.Property<int?>("Quantiy")
                        .HasColumnName("quantiy");

                    b.HasKey("IdHotel", "IdFacilities");

                    b.HasIndex("IdFacilities");

                    b.ToTable("FacilitiesHotel");
                });

            modelBuilder.Entity("Licenta.Entityes.HotelImages", b =>
                {
                    b.Property<int>("IdImageHotel")
                        .HasColumnName("idImageHotel");

                    b.Property<int?>("IdHotel")
                        .HasColumnName("idHotel");

                    b.Property<byte[]>("ImageHotel")
                        .HasColumnType("image");

                    b.HasKey("IdImageHotel");

                    b.HasIndex("IdHotel");

                    b.ToTable("HotelImages");
                });

            modelBuilder.Entity("Licenta.Entityes.Hotels", b =>
                {
                    b.Property<int>("IdHotel")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idHotel");

                    b.Property<string>("DescriptionTable")
                        .HasColumnName("descriptionTable")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("HotelName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("IdLocation");

                    b.Property<int?>("Stars");

                    b.HasKey("IdHotel");

                    b.HasIndex("IdLocation");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Licenta.Entityes.Location", b =>
                {
                    b.Property<int>("IdLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idLocation");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("NrStreat")
                        .HasColumnName("nrStreat");

                    b.Property<string>("RegionName")
                        .HasColumnName("regionName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("StreatName")
                        .HasColumnName("streatName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("IdLocation");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Licenta.Entityes.Reservations", b =>
                {
                    b.Property<int>("IdReservations")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idReservations");

                    b.Property<DateTime?>("CheckIn")
                        .IsRequired()
                        .HasColumnName("check_in")
                        .HasColumnType("date");

                    b.Property<DateTime?>("CheckOut")
                        .IsRequired()
                        .HasColumnName("check_out")
                        .HasColumnType("date");

                    b.Property<int?>("IdCustomer")
                        .HasColumnName("idCustomer");

                    b.Property<int?>("IdRoom")
                        .HasColumnName("idRoom");

                    b.HasKey("IdReservations");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdRoom");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Licenta.Entityes.Rooms", b =>
                {
                    b.Property<int>("IdRoom")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idRoom");

                    b.Property<bool?>("Bath");

                    b.Property<int?>("Beds");

                    b.Property<int?>("IdHotel")
                        .HasColumnName("idHotel");

                    b.Property<bool?>("Reserved")
                        .HasColumnName("reserved");

                    b.Property<int?>("RoomNumber")
                        .HasColumnName("roomNumber");

                    b.HasKey("IdRoom");

                    b.HasIndex("IdHotel");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Licenta.Entityes.CreditCard", b =>
                {
                    b.HasOne("Licenta.Entityes.Customers", "IdClientNavigation")
                        .WithMany("CreditCard")
                        .HasForeignKey("IdClient")
                        .HasConstraintName("FK__CreditCar__idCli__05D8E0BE");
                });

            modelBuilder.Entity("Licenta.Entityes.Employes", b =>
                {
                    b.HasOne("Licenta.Entityes.Hotels", "IdHotelNavigation")
                        .WithMany("Employes")
                        .HasForeignKey("IdHotel")
                        .HasConstraintName("FK__Employes__IdHote__160F4887");
                });

            modelBuilder.Entity("Licenta.Entityes.FacilitiesHotel", b =>
                {
                    b.HasOne("Licenta.Entityes.Facilities", "IdFacilitiesNavigation")
                        .WithMany("FacilitiesHotel")
                        .HasForeignKey("IdFacilities")
                        .HasConstraintName("FK__Facilitie__idFac__628FA481");

                    b.HasOne("Licenta.Entityes.Hotels", "IdHotelNavigation")
                        .WithMany("FacilitiesHotel")
                        .HasForeignKey("IdHotel")
                        .HasConstraintName("FK__Facilitie__idHot__619B8048");
                });

            modelBuilder.Entity("Licenta.Entityes.HotelImages", b =>
                {
                    b.HasOne("Licenta.Entityes.Hotels", "IdHotelNavigation")
                        .WithMany("HotelImages")
                        .HasForeignKey("IdHotel")
                        .HasConstraintName("FK__HotelImag__idHot__02FC7413");
                });

            modelBuilder.Entity("Licenta.Entityes.Hotels", b =>
                {
                    b.HasOne("Licenta.Entityes.Location", "IdLocationNavigation")
                        .WithMany("Hotels")
                        .HasForeignKey("IdLocation")
                        .HasConstraintName("FK__Hotels__IdLocati__3A4CA8FD");
                });

            modelBuilder.Entity("Licenta.Entityes.Reservations", b =>
                {
                    b.HasOne("Licenta.Entityes.Customers", "IdCustomerNavigation")
                        .WithMany("Reservations")
                        .HasForeignKey("IdCustomer")
                        .HasConstraintName("FK__Reservati__idCus__5AEE82B9");

                    b.HasOne("Licenta.Entityes.Rooms", "IdRoomNavigation")
                        .WithMany("Reservations")
                        .HasForeignKey("IdRoom")
                        .HasConstraintName("FK__Reservati__idRoo__5441852A");
                });

            modelBuilder.Entity("Licenta.Entityes.Rooms", b =>
                {
                    b.HasOne("Licenta.Entityes.Hotels", "IdHotelNavigation")
                        .WithMany("Rooms")
                        .HasForeignKey("IdHotel")
                        .HasConstraintName("FK__Rooms__idHotel__4E88ABD4");
                });
#pragma warning restore 612, 618
        }
    }
}
