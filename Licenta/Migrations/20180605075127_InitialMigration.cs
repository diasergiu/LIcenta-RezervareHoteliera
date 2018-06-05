using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Licenta.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    idCustomer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    firstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    lastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    username = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.idCustomer);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    idFacilities = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    facilitiesName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IsChecked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.idFacilities);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    idLocation = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    nrStreat = table.Column<int>(nullable: true),
                    regionName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    streatName = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.idLocation);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    idCard = table.Column<int>(nullable: false),
                    cardExpireDate = table.Column<DateTime>(type: "date", nullable: true),
                    cardNumber = table.Column<string>(unicode: false, maxLength: 16, nullable: true),
                    CVC = table.Column<int>(nullable: true),
                    idClient = table.Column<int>(nullable: true),
                    MoneyInTheCard = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.idCard);
                    table.ForeignKey(
                        name: "FK__CreditCar__idCli__05D8E0BE",
                        column: x => x.idClient,
                        principalTable: "Customers",
                        principalColumn: "idCustomer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    idHotel = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descriptionTable = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    HotelName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IdLocation = table.Column<int>(nullable: true),
                    Stars = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.idHotel);
                    table.ForeignKey(
                        name: "FK__Hotels__IdLocati__3A4CA8FD",
                        column: x => x.IdLocation,
                        principalTable: "Location",
                        principalColumn: "idLocation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employes",
                columns: table => new
                {
                    IDEmploye = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployType = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    FirstName = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    IdHotel = table.Column<int>(nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Password = table.Column<string>(unicode: false, maxLength: 55, nullable: true),
                    Username = table.Column<string>(unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employes", x => x.IDEmploye);
                    table.ForeignKey(
                        name: "FK__Employes__IdHote__160F4887",
                        column: x => x.IdHotel,
                        principalTable: "Hotels",
                        principalColumn: "idHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilitiesHotel",
                columns: table => new
                {
                    idHotel = table.Column<int>(nullable: false),
                    idFacilities = table.Column<int>(nullable: false),
                    quantiy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilitiesHotel", x => new { x.idHotel, x.idFacilities });
                    table.ForeignKey(
                        name: "FK__Facilitie__idFac__628FA481",
                        column: x => x.idFacilities,
                        principalTable: "Facilities",
                        principalColumn: "idFacilities",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Facilitie__idHot__619B8048",
                        column: x => x.idHotel,
                        principalTable: "Hotels",
                        principalColumn: "idHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    idImageHotel = table.Column<int>(nullable: false),
                    idHotel = table.Column<int>(nullable: true),
                    ImageHotel = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.idImageHotel);
                    table.ForeignKey(
                        name: "FK__HotelImag__idHot__02FC7413",
                        column: x => x.idHotel,
                        principalTable: "Hotels",
                        principalColumn: "idHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    idRoom = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bath = table.Column<bool>(nullable: true),
                    Beds = table.Column<int>(nullable: true),
                    idHotel = table.Column<int>(nullable: true),
                    reserved = table.Column<bool>(nullable: true),
                    roomNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.idRoom);
                    table.ForeignKey(
                        name: "FK__Rooms__idHotel__4E88ABD4",
                        column: x => x.idHotel,
                        principalTable: "Hotels",
                        principalColumn: "idHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    idReservations = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    check_in = table.Column<DateTime>(type: "date", nullable: false),
                    check_out = table.Column<DateTime>(type: "date", nullable: false),
                    idCustomer = table.Column<int>(nullable: true),
                    idRoom = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.idReservations);
                    table.ForeignKey(
                        name: "FK__Reservati__idCus__5AEE82B9",
                        column: x => x.idCustomer,
                        principalTable: "Customers",
                        principalColumn: "idCustomer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Reservati__idRoo__5441852A",
                        column: x => x.idRoom,
                        principalTable: "Rooms",
                        principalColumn: "idRoom",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_idClient",
                table: "CreditCard",
                column: "idClient");

            migrationBuilder.CreateIndex(
                name: "IX_Employes_IdHotel",
                table: "Employes",
                column: "IdHotel");

            migrationBuilder.CreateIndex(
                name: "IX_FacilitiesHotel_idFacilities",
                table: "FacilitiesHotel",
                column: "idFacilities");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_idHotel",
                table: "HotelImages",
                column: "idHotel");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_IdLocation",
                table: "Hotels",
                column: "IdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_idCustomer",
                table: "Reservations",
                column: "idCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_idRoom",
                table: "Reservations",
                column: "idRoom");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_idHotel",
                table: "Rooms",
                column: "idHotel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "Employes");

            migrationBuilder.DropTable(
                name: "FacilitiesHotel");

            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
