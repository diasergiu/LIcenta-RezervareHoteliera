using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Licenta.Entityes
{
    public partial class DBRezervareHotelieraContext : DbContext
    {
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employes> Employes { get; set; }
        public virtual DbSet<Facilities> Facilities { get; set; }
        public virtual DbSet<FacilitiesHotel> FacilitiesHotel { get; set; }
        public virtual DbSet<Hotels> Hotels { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }

        public DBRezervareHotelieraContext(DbContextOptions<DBRezervareHotelieraContext> options) : base(options)
        {

        }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=DESKTOP-TPPITPA\SQLEXPRESS;Database=DBRezervareHoteliera;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.IdCustomer);

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employes>(entity =>
            {
                entity.HasKey(e => e.Idemploye);

                entity.Property(e => e.Idemploye).HasColumnName("IDEmploye");

                entity.Property(e => e.EmployType)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Employes)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("FK__Employes__IdHote__160F4887");
            });

            modelBuilder.Entity<Facilities>(entity =>
            {
                entity.HasKey(e => e.IdFacilities);

                entity.Property(e => e.IdFacilities).HasColumnName("idFacilities");

                entity.Property(e => e.FacilitiesName)
                    .HasColumnName("facilitiesName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FacilitiesHotel>(entity =>
            {
                entity.HasKey(e => new { e.IdHotel, e.IdFacilities });

                entity.Property(e => e.IdHotel).HasColumnName("idHotel");

                entity.Property(e => e.IdFacilities).HasColumnName("idFacilities");

                entity.Property(e => e.Quantiy).HasColumnName("quantiy");

                entity.HasOne(d => d.IdFacilitiesNavigation)
                    .WithMany(p => p.FacilitiesHotel)
                    .HasForeignKey(d => d.IdFacilities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Facilitie__idFac__628FA481");

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.FacilitiesHotel)
                    .HasForeignKey(d => d.IdHotel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Facilitie__idHot__619B8048");
            });

            modelBuilder.Entity<Hotels>(entity =>
            {
                entity.HasKey(e => e.IdHotel);

                entity.Property(e => e.IdHotel).HasColumnName("idHotel");

                entity.Property(e => e.DescriptionTable)
                    .HasColumnName("descriptionTable")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.HotelName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.IdLocation);

                entity.Property(e => e.IdLocation).HasColumnName("idLocation");

                entity.Property(e => e.IdHotel).HasColumnName("idHotel");

                entity.Property(e => e.NrStreat).HasColumnName("nrStreat");

                entity.Property(e => e.RegionName)
                    .HasColumnName("regionName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreatName)
                    .HasColumnName("streatName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("FK__Location__idHote__5165187F");
            });

            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.HasKey(e => e.IdReservations);

                entity.Property(e => e.IdReservations).HasColumnName("idReservations");

                entity.Property(e => e.CheckIn)
                    .HasColumnName("check_in")
                    .HasColumnType("date");

                entity.Property(e => e.CheckOut)
                    .HasColumnName("check_out")
                    .HasColumnType("date");

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.IdRoom).HasColumnName("idRoom");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK__Reservati__idCus__5AEE82B9");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("FK__Reservati__idRoo__5441852A");
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.IdRoom);

                entity.Property(e => e.IdRoom).HasColumnName("idRoom");

                entity.Property(e => e.IdHotel).HasColumnName("idHotel");

                entity.Property(e => e.Reserved).HasColumnName("reserved");

                entity.Property(e => e.RoomNumber).HasColumnName("roomNumber");

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("FK__Rooms__idHotel__4E88ABD4");
            });
        }
    }
}
